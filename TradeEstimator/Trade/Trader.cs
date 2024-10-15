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
    public class Trader
    {
        //params
        public Config config;
        public Logger logger;
        QuotesLoader all_data;
        public TradeModel trModel;
        public string tradeId;
        public DateTime time1;
        public DateTime time2;

        //runtime
        List<InstrConfig> instrConfigs;
        List<DaysQuotes> instrDaysQuotes;
        List<string> instruments;
        int[] lastBar;
        int instrN;
        InputTimes inputTimes; 
        TradeProcess[] trProcesses;
        Input input;
        Output output;



        public Trader(Config config, Logger logger, QuotesLoader all_data, List<InstrConfig> instrConfigs, TradeModel trModel, string tradeId, DateTime time1, DateTime time2) //create from runner
        {
            this.config = config;
            this.logger = logger;
            this.all_data = all_data;
            this.instrConfigs = instrConfigs;
            this.trModel = trModel;
            this.tradeId = tradeId;
            this.time1 = time1;
            this.time2 = time2;

            logger.logTitle("Trader", 1);

            initialize();
        }


        private void initialize()
        {
            instruments = new();

            foreach (InstrConfig instrConfig in instrConfigs)
            {
                Quotes instrQuotes = all_data.getInstrQuotes(instrConfig.instr_name);
                DaysQuotes daysQuotes = instrQuotes.get_days_quotes_no_adr(trModel, time1, time2);
                instrDaysQuotes.Add(daysQuotes);
                instruments.Add(instrConfig.instr_name);
            }

            instrN = instruments.Count;

            lastBar = new int[instrN];

            for(int i = 0; i < instrN; i++)
            {
                lastBar[i] = instrDaysQuotes[i].Timeline.Length - 1; // max index, not length!
            }

            trProcesses = new TradeProcess[instrN];

            for(int i = 0; i < instrN; i++)
            {
                trProcesses[i] = new(config, logger, instrConfigs[i], trModel);
            }

            inputTimes = new(config, logger, trModel, tradeId);
        }



        public void run()
        {
            DateTime timeI = time1;

            int maxLastBar = 0; 

            for (int i = 0; i < instrN; i++)
            {
                if (lastBar[i] > maxLastBar)
                {
                    maxLastBar = lastBar[i];
                }
            }


            for(int barI = 0; barI <= maxLastBar; barI++) // bar cycle : : : : : : : : : : : : : : : : : : : : : : :
            {
                for (int instrI = 0; instrI < instrN; instrI++) // instr cycle - - - - - - - - -
                {
                    DBar bar = instrDaysQuotes[instrI].dBars[barI];

                    if (barI == lastBar[instrI])
                    {
                        //last bar

                        //TO DO



                    }
                    else
                    {
                        //not last bar

                        if (DateTime.Compare(bar.time, timeI) == 0)
                        {
                            Input? input = getInput(instruments[instrI], bar);

                            List<Order> orders = new();

                            if (input != null)
                            {
                                orders = createOrders(input);
                            }

                            trProcesses[instrI].processBar(orders, bar); //TODO orders on bar or not???


                            double profit = 0; //TODO
                            double drawdown = 0; //TODO

                            createOutput(instruments[instrI], bar, profit, drawdown);
                        }
                    }
                } // instr cycle - - - - - - - - - - - - - - - - - - - - - - - - - - 

                timeI.AddMinutes(config.tf);

            } // bar cycle : : : : : : : : : : : : : : : : : : : : : : : : : : : : : :
        }



        private Input getInput(string instr, DBar bar) 
        {
            foreach(DateTime inputTime in inputTimes)
            {
                if(DateTime.Compare(inputTime, bar.time) == 0)
                {
                    string timestamp = bar.time.ToString("yyyyMMdd HHmmss");

                    return new Input(config, logger, trModel, timestamp, instr);
                }

            }
            return null;
        }


        private List<Order> createOrders(Input input)
        {
            List<Order> orders = new();





            return orders;
        }



        private void createOutput(string instr, DBar bar, double profit, double drawdown) 
        {
            string timestamp = bar.time.ToString("yyyyMMdd HHmmss");

            Output output = new Output(config, logger, trModel, timestamp, instr, profit, drawdown);
        }



        private void saveEvent(string instr, DBar bar)
        {


        }



    }
}
