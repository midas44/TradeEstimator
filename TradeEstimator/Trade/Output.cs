using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using TradeEstimator.Conf;
using TradeEstimator.Log;

namespace TradeEstimator.Trade
{
    public class Output
    {
        //params
        Config config;
        Logger logger;
        TradeModel trModel;
        string tradeId;
        string timestamp;
        string instrument;
        public double profit; //percent or abs???
        public double drawdown; //percent  or abs???
        public double exposure; //units?
        public List<Order> orders;


        public Output(Config config, Logger logger, TradeModel trModel, string tradeId, string timestamp, string instrument, double profit, double drawdown, double exposure, List<Order> orders)
        {
            this.config = config;
            this.logger = logger;
            this.trModel = trModel;
            this.tradeId = tradeId;
            this.timestamp = timestamp;
            this.instrument = instrument;        
            this.profit = profit;
            this.drawdown = drawdown;
            this.exposure = exposure;
            this.orders = orders;

            //20101017 195200; 1.3977; 1.3977; 1.3977; 1.3977; 500000
            //DateTime dt = DateTime.ParseExact(s[0], "yyyyMMdd HHmmss", CultureInfo.InvariantCulture);

            timestamp = timestamp.Replace(" ", "_");
            string year = timestamp.Substring(0, 3);
            string month = timestamp.Substring(4, 5);
            string day = timestamp.Substring(6, 7);

            string filePath = config.outputs_path + "/"
                + trModel.trModelName + "/"
                + tradeId + "/"
                + year + "/"
                + month + "/"
                + day + "/"
                + timestamp + "/"
                + instrument + "_"
                + config.data_timeframe + "."
                + config.data_ext;

            logger.log_("Output: " + filePath, 1);

            save(filePath);
        }


        public void save(string path)
        {
            string line = profit.ToString(trModel.valueOutputFormat) + "; " + drawdown.ToString(trModel.valueOutputFormat) + "; " + exposure.ToString(trModel.valueOutputFormat);

            File.AppendAllText(path, line);
            Application.DoEvents();
        }


    }
}
