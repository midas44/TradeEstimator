using TradeEstimator.Charts;
using TradeEstimator.Conf;
using TradeEstimator.Data;
using TradeEstimator.Log;
using TradeEstimator.Trade;
using ScottPlot.Colormaps;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TradeEstimator.Test;
using System.Globalization;
using System.Numerics;

namespace TradeEstimator.Main
{
    public class RunnerBase
    {

        public Config config;
        public Logger logger;
        public QuotesLoader allData;


        public RunnerBase(Config config, Logger logger)
        {
            this.config = config;
            this.logger = logger;

            logger.logTitle("New Merlin", 1);
            logger.log_("Runner", 1);

            loadData();
        }


        public void loadData()
        {
            allData = new(config, logger);
        }

    }
}
