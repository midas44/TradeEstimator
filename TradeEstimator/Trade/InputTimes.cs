using Microsoft.VisualBasic.Logging;
using ScottPlot;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeEstimator.Conf;
using TradeEstimator.Log;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;

namespace TradeEstimator.Trade
{
    public class InputTimes
    {
        //params

        Config config;
        Logger logger;
        TradeModel trModel;
        string tradeId;

        //runtime
        public List<DateTime> itimes;
        public int n = 0;  


        public InputTimes(Config config, Logger logger, TradeModel trModel, string tradeId)
        {
            this.config = config;
            this.logger = logger;
            this.trModel = trModel;
            this.tradeId = tradeId;

            string filePath = config.inputs_path + "/"
                + trModel.trModelName + "/"
                + tradeId + "/"
                + "itimes" + "_"
                + config.data_timeframe + "."
                + config.data_ext;

            logger.log_("Input Times: " + filePath, 1);

            n = 0;

            itimes = new();

            if (File.Exists(filePath))
            {
                load(filePath);
                n = itimes.Count;
            } 
        }


        private void load(string path)
        {
            string[] dataset0 = System.IO.File.ReadAllLines(path);

            string[] dataset = dataset0.Where(item => item != string.Empty).ToArray();

            foreach (string line in dataset)
            {
                DateTime t = DateTime.ParseExact(line.Trim(), "yyyyMMdd HHmmss", CultureInfo.InvariantCulture);
                itimes.Add(t);
            }
        }


    }
}
