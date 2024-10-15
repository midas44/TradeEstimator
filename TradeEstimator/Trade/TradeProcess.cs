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

        InstrConfig instrConfig;


        //runtime

        Position position;

        public List<Event> events;

        public List<DateTime> timeLine;

        public List<double> equityLine;

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
            position = new(instrConfig);

            events = new();

            timeLine = new();

            equityLine = new();

            drawdownLine = new();

            exposureLine = new();
        }



        public void processBar(List<Order> orders, DBar bar)
        {




        }




    }
}
