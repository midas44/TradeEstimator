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
        public DateTime time;
        public string instrument;
        
        //runtime
        public List<int> size;
        public List<double> price;
        public int n;
        


        public Input(Config config, Logger logger, TradeModel trModel, string tradeId, DateTime time, string instrument)
        {
            this.config = config;
            this.logger = logger;
            this.trModel = trModel;
            this.time = time;
            this.instrument = instrument;
            this.tradeId = tradeId;


            string timestamp = getTimestamp(time);
            timestamp = timestamp.Replace(" ", "_");
            string year = timestamp.Substring(0, 4);
            string month = timestamp.Substring(4, 2);
            string day = timestamp.Substring(6, 2);

            string filePath = config.inputs_path + "/" 
                + trModel.trModelName + "/"
                + tradeId + "/"
                + year + "/"
                + month + "/"
                + day + "/"
                + timestamp + "/" //bug here!
                + instrument + "_"
                + config.data_timeframe + "." 
                + config.data_ext;

            logger.log_("Input: " + filePath, 1);

            size = new();
            price = new();

            n = 0;

            if (File.Exists(filePath))
            {
                logger.log("file exists", 2);
                load(filePath);
                n = price.Count;

            }

        }


        private void load(string path)
        {
            string[] dataset = System.IO.File.ReadAllLines(path);
            //string[] dataset = dataset0.Where(item => item != string.Empty).ToArray();

            foreach (string line in dataset)
            {
                if(line.Trim().Length > 0)
                {
                    //size; price;

                    string[] s = line.Split(';');

                    size.Add(int.Parse(s[0].Trim(), CultureInfo.InvariantCulture));
                    price.Add(double.Parse(s[1].Trim(), CultureInfo.InvariantCulture));
                }

            }
        }


        private string getTimestamp(DateTime time) //TO DO: in one place (now in trader and ion tradeProcess)
        {
            return time.ToString("yyyyMMdd HHmmss");
        }


    }
}
