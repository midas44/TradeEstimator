using TradeEstimator.Conf;
using TradeEstimator.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeEstimator.Data
{
    public class QuotesLoader
    {

        Config config;
        Logger logger;

        public Quotes[] instrQuotes;

       
        public QuotesLoader(Config config, Logger logger) 
        {
            this.config = config;
            this.logger = logger;

            logger.log_("QuotesLoader", 1);

            int n = config.instruments.Count;

            instrQuotes = new Quotes[n];

            int i = 0;  

            foreach (string instrument in config.instruments)
            {
                instrQuotes[i] = new Quotes(config, logger, instrument);
                Application.DoEvents();
                i++;
            }
        }


        public Quotes getInstrQuotes(string instrument)
        {
            int index = config.instruments.IndexOf(instrument);
            return instrQuotes[index];
        }

    }
}
