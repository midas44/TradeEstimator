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
    public class Input
    {
        //params
        Config config;
        Logger logger;
        TradeModel trModel;
        string tradeId;
        public string timestamp;
        public string instrument;
        
        //runtime
        public List<int> size;
        public List<double> price;
        public int n;


        public Input(Config config, Logger logger, TradeModel trModel, string tradeId, string timestamp, string instrument)
        {
            this.config = config;
            this.logger = logger;
            this.trModel = trModel;
            this.timestamp = timestamp;
            this.instrument = instrument;
            this.tradeId = tradeId;

            timestamp = timestamp.Replace(" ", "_");
            string year = timestamp.Substring(0, 3);
            string month = timestamp.Substring(4, 5);
            string day = timestamp.Substring(6, 7);

            string filePath = config.inputs_path + "/" 
                + trModel.trModelName + "/"
                + tradeId + "/"
                + year + "/"
                + month + "/"
                + day + "/"
                + timestamp + "/"
                + instrument + "_"
                + config.data_timeframe + "." 
                + config.data_ext;

            logger.log_("Input: " + filePath, 1);

            size = new();
            price = new();

            n = 0;

            if (File.Exists(filePath))
            {
                load(filePath);
                n = price.Count;
            }

        }


        private void load(string path)
        {
            string[] dataset0 = System.IO.File.ReadAllLines(path);
            string[] dataset = dataset0.Where(item => item != string.Empty).ToArray();

            foreach (string line in dataset)
            {
                //size; price;

                string[] s = line.Split(';');

                size.Add(int.Parse(s[0].Trim(), CultureInfo.InvariantCulture));
                price.Add(double.Parse(s[1].Trim(), CultureInfo.InvariantCulture));
            }
        }


    }
}
