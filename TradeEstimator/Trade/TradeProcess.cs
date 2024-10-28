using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeEstimator.Conf;
using TradeEstimator.Data;
using TradeEstimator.Log;
using TradeEstimator.Main;

namespace TradeEstimator.Trade
{
    public class TradeProcess
    {
        //params

        public Config config;

        public Logger logger;

        public TradeModel trModel;

        public InstrConfig instrConfig;


        //runtime

        public List<Order> activeOrders;

        public List<Order> inactiveOrders;

        public Position position;

        public List<DateTime> timeLine;

        public List<double> equityLine;

        public List<double> drawdownLine;

        public List<double> lossLine;

        public List<double> exposureLine;


        public TradeProcess(Config config, Logger logger, InstrConfig instrConfig, TradeModel trModel) 
        {
            this.config = config;
            this.logger = logger;
            this.instrConfig = instrConfig;
            this.trModel = trModel;

            logger.logTitle("TradeProcess", 1);

            prepareData();
        }


        private void prepareData()
        {
            activeOrders = new();

            inactiveOrders = new();

            position = new(instrConfig, trModel);

            timeLine = new();

            equityLine = new();

            drawdownLine = new();

            lossLine = new();

            exposureLine = new();
        }


        public void processFirstBar(DBar bar) //Dbar (close)
        {
            position.enter(bar.close);
        }


        public void clearOrders(DateTime time)
        {
            foreach(var order in activeOrders)
            {
                deactivateOrder(order, time);
            }
        }


        private void deactivateOrder(Order order, DateTime time)
        {
            order.deactivate(time);
            inactiveOrders.Add(order);
            activeOrders.Remove(order);
        }


        public void addOrders(List<Order> newOrders)
        {
            activeOrders.AddRange(newOrders);
        }


        private void trade(Order order, VBar bar)
        {
            double entryPrice = calcEntryPrice(order, bar);

            order.isTriggered = true;
            order.entryPrice = entryPrice;
            deactivateOrder(order, bar.time);

            position.change(order.size);

            position.update(entryPrice);
        }


        public void processBar(VBar bar)
        {
            var orderSequence = findOrderSequence(bar);

            foreach (Order order in orderSequence) 
            {
                trade(order, bar);               
            }

            recordMetrics();
        }


        public void processLastBar(VBar bar)
        {
            Order exitOrder = new(instrConfig.instr_name, -position.size, "market", bar.open, bar.time); //market order for position exit

            activeOrders.Add(exitOrder);

            processBar(bar);
        }


        private void recordMetrics() // on bar close
        {
            equityLine.Add(position.lastEquity);

            lossLine.Add(position.maxLoss);

            drawdownLine.Add(position.drawdown);

            exposureLine.Add(Math.Abs(position.size));
        }


        private string getTimestamp(DateTime time)
        {
            return time.ToString("yyyyMMdd HHmmss");
        }


        private double calcEntryPrice(Order order , VBar bar)
        {
            //slippage calc

            int deltaSize = order.size;

            double triggerPrice = order.triggerPrice;

            double entryPrice = triggerPrice;

            if (deltaSize > 0)
            {
                double delta = bar.high - triggerPrice;
                entryPrice = triggerPrice + trModel.slippageFactor * delta;
            }

            if (deltaSize < 0)
            {
                double delta = triggerPrice - bar.low;
                entryPrice = triggerPrice - trModel.slippageFactor * delta;
            }

            return entryPrice;
        }


        private List<Order> findOrderSequence(VBar bar) //virtual bar
        {
            List<Order> foundOrders = new();

            List<Order> ordersInBar = new();

            foreach (var order in activeOrders)
            {
                if(order.triggerPrice >= bar.low && order.triggerPrice <= bar.high) 
                {
                    ordersInBar.Add(order);
                }
            }

            while (ordersInBar.Count > 0)
            {
                int index = -1;

                double delta = 999999;

                int i = 0;

                foreach (var order in ordersInBar)
                {
                    double d = Math.Abs(bar.open - order.triggerPrice);

                    if (delta > d)
                    {
                        delta = d;
                        index = i;
                    }

                    i++;
                }

                foundOrders.Add(ordersInBar[index]);
                ordersInBar.RemoveAt(index);
            }

            return foundOrders;
        }




    }
}
