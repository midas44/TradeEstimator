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

        public int size;   //lots

        public List<double> equityLine;
        public List<DateTime> timeLine;

        public double equity;

        public double lastPrice;

        
        public Position(InstrConfig instrConfig) 
        {
            this.instrConfig = instrConfig;

            equityLine = new();
            timeLine = new();   

            equity = 0;
        }


        public void update(double price, DateTime time)
        {
            double deltaEquity = size * instrConfig.leverage * instrConfig.lot  * (price - lastPrice) / instrConfig.instr_tick;

            equity += deltaEquity;

            equityLine.Add(equity);
            timeLine.Add(time);

            lastPrice = price;
        }


        public void change(int deltaSize, double price, DateTime time)
        {
            update(price, time);
            size += deltaSize;
        }


    }
}
