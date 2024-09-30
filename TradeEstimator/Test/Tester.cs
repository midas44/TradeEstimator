using TradeEstimator.Charts;
using TradeEstimator.Conf;
using TradeEstimator.Data;
using TradeEstimator.Log;
using TradeEstimator.Trade;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeEstimator.Test
{
    public class Tester
    {

        Config config;

        Logger logger;

        QuotesLoader all_data;

        TradeModel tr_model;

        List<string> instruments;

        List<InstrConfig> instr_config_list;

        List<Quotes> instr_quotes_list;

       // List<List<double>> eq_list;

        //List<Equity> equity_list;

        //Chart1 chart1;


        public Tester(Config config, Logger logger, QuotesLoader all_data, List<string> instruments, TradeModel tr_model)
        {
            this.config = config;
            this.logger = logger;
            this.all_data = all_data;
            this.tr_model = tr_model;
            this.instruments = instruments;


            logger.logTitle("Tester", 1);




            instr_config_list = new List<InstrConfig>();

            instr_quotes_list = new List<Quotes>();

            //equity_list = new List<Equity>();

            //eq_list = new List<List<double>>();

            foreach (String instr in instruments)
            {
                InstrConfig instr_config = new(instr);

                instr_config_list.Add(instr_config);

                Quotes instr_quotes = all_data.getInstrQuotes(instr);

                instr_quotes_list.Add(instr_quotes);

                //List<double> eq = new();

                //eq_list.Add(eq);
            }
        }





    }
}
