﻿using TradeEstimator.Charts;
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
using System.Globalization;
using System.Numerics;

namespace TradeEstimator.Main
{
    public class RunnerBase
    {

        public Config config;
        public Logger logger;
        public QuotesLoader allData;

        public List<InstrConfig> instrConfigs;


        public RunnerBase(Config config, Logger logger)
        {
            this.config = config;
            this.logger = logger;

            logger.logTitle("Trade Estimator", 1);
            logger.log_("Runner", 1);

            loadInstrData();

            loadInstrConfigs();
        }


        public void loadInstrData()
        {
            allData = new(config, logger);
        }

        public void loadInstrConfigs()
        {
            instrConfigs = new();

            foreach (string instrument in config.instruments)
            {
                var instrConfig = new InstrConfig(instrument);
                instrConfigs.Add(instrConfig);
            }
        }

    }
}
