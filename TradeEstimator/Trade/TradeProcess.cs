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

        public List<Order> orders;

        public Position position;

        public List<Event> events;

        public List<DateTime> timeLine;

        public List<double> profitLine;

        public List<double> drawdownLine;

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
            orders = new();

            position = new(instrConfig);

            events = new();

            timeLine = new();

            profitLine = new();

            drawdownLine = new();

            exposureLine = new();
        }



        public void processBar(List<Order> newOrders, DBar bar)
        {
            foreach (Order order in orders) 
            {
            
            
            }

            //TO DO: position update and change

            recordMetrics();
        }


        public void processLastBar(DBar bar)
        {
            foreach (Order order in orders)
            {
                order.deactivate(); //need not???
            }

            //TO DO: position update and close

            recordMetrics();
        }


        private void recordMetrics()
        {
            profitLine.Add(position.profit);

            exposureLine.Add(Math.Abs(position.size));

            drawdownLine.Add(position.drawdown);
        }




    }
}
