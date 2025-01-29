using ScottPlot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeEstimator.Conf;
using TradeEstimator.Data;


namespace TradeEstimator.Trade
{
    public class Position
    {
        //params
        public InstrConfig instrConfig;

        public TradeModel trModel;


        //runtime

        public int size;   //lots

        public double lastEquity; 

        public double maxProfit; //all time max

        public double maxLoss; //all time min

        public double drawdown; //loss from maxProfit

        public double lastPrice;

        /*
        public DateTime entryTime;

        public DateTime exitTime;

        public double entryPrice;

        public double exitPrice;
        */


        public Position(InstrConfig instrConfig, TradeModel trModel) 
        {
            this.instrConfig = instrConfig;
            this.trModel = trModel;

            size = 0; 
            
            lastEquity = 0;
        }


        public void enter(double price)
        {
            //entryPrice = price;
            //entryTime = time;

            lastPrice = price;
        }


        public void update(double price) //on close!
        {
            lastEquity += calcEquityChange(price);

            lastPrice = price;

            calcMetrics();
        }


        private double calcEquityChange(double price)
        {
            return size * instrConfig.leverage * instrConfig.lot  * (price - lastPrice) / instrConfig.instr_tick;
        }


        public void change(int deltaSize) //on order trigger
        {
            size += deltaSize;
        }


        private void calcMetrics()
        {
            if(maxProfit < lastEquity) 
            { 
                maxProfit = lastEquity;
            }

            if(maxLoss > lastEquity)
            {
                maxLoss = lastEquity;
            }

            if(maxProfit > lastEquity)
            {
                drawdown = maxProfit - lastEquity;
            }
            else
            {
                drawdown = 0;
            }            
        }

    }

}
