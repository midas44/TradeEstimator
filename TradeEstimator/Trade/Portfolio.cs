using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeEstimator.Conf;
using TradeEstimator.Data;
using TradeEstimator.Log;

namespace TradeEstimator.Trade
{
    public class Portfolio
    {
        //params

        public Config config;

        public Logger logger;

        public TradeProcess[] trProcesses;


        //runtime

        public List<DateTime> timeLine;

        public List<double> profitLine;

        public List<double> drawdownLine;

        public List<double> exposureLine;



        public Portfolio(Config config, Logger logger, TradeProcess[] trProcesses)
        {
            this.config = config;
            this.logger = logger;
            this.trProcesses = trProcesses;

            logger.logTitle("Portfolio", 1);

            prepareData();
        }


        private void prepareData()
        {
            timeLine = new();

            profitLine = new();

            drawdownLine = new();

            exposureLine = new();
        }


        public void update()
        {
            double profit = 0;
            double drawdown = 0;
            double exposure = 0;

            foreach (var trProcess in trProcesses)
            {
                int n = trProcess.timeLine.Count - 1;
                if (n > 0)
                {
                    timeLine.Add(trProcess.timeLine[n]);
                    profit += trProcess.profitLine[n];
                    drawdown += trProcess.drawdownLine[n];
                    exposure += Math.Abs(trProcess.exposureLine[n]);
                }
            }

            recordMetrics(profit, drawdown, exposure);
        }


        private void recordMetrics(double profit, double drawdown, double exposure)
        {
            profitLine.Add(profit);

            exposureLine.Add(exposure);

            drawdownLine.Add(drawdown);
        }

    }
}