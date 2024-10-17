using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeEstimator.Conf;
using TradeEstimator.Data;
using TradeEstimator.Log;
using TradeEstimator.Main;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Menu;

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
        Portfolio portfolio;
        List <Output> outputs;



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

            run();
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

            portfolio = new(config, logger, trProcesses);

            inputTimes = new(config, logger, trModel, tradeId);

            outputs = new();
        }


        private void run()
        {
            int maxLastBar = 0; 

            for (int i = 0; i < instrN; i++)
            {
                if (lastBar[i] > maxLastBar)
                {
                    maxLastBar = lastBar[i];
                }
            }

            DateTime timeI = time1;

            for (int barI = 0; barI <= maxLastBar; barI++) // bar cycle : : : : : : : : : : : : : : : : : : : : : : :
            {
                for (int instrI = 0; instrI < instrN; instrI++) // instr cycle - - - - - - - - -
                {
                    DBar bar = instrDaysQuotes[instrI].dBars[barI];

                    if (barI == lastBar[instrI])
                    {
                        //last bar

                        //TO DO

                        //cancel orders, close positions, save final output

                        trProcesses[instrI].processLastBar(bar);

                        createOutput(trProcesses[instrI]);
                    }
                    else
                    {
                        //not last bar

                        if (DateTime.Compare(bar.time, timeI) == 0)
                        {
                            Input? input = getInput(instruments[instrI], bar);

                            var orders = createOrders(input);
                          
                            trProcesses[instrI].processBar(orders, bar);
                        }
                    }
                } // instr cycle - - - - - - - - - - - - - - - - - - - - - - - - - - 

                portfolio.update();

                timeI.AddMinutes(config.tf);

            } // bar cycle : : : : : : : : : : : : : : : : : : : : : : : : : : : : : :

            createPortfolioOutput(portfolio);
        }


        private Input getInput(string instr, DBar bar) 
        {
            foreach(DateTime inputTime in inputTimes.itimes)
            {
                if(DateTime.Compare(inputTime, bar.time) == 0)
                {
                    return new Input(config, logger, trModel, tradeId, getTimestamp(bar.time), instr);
                }
            }

            return null;
        }


        private List<Order> createOrders(Input input)
        {
            List<Order> orders = new();

            if (input != null && input.n>0)
            {
                for (int i = 0; i < input.n; i++) 
                {
                    string dir = "";

                    if (input.size[i] > 0)
                    {
                        dir = "BUY";
                    }

                    if (input.size[i] < 0)
                    {
                        dir = "SELL";
                    }

                    if(dir != "")
                    {
                        Order order = new(input.instrument, dir, input.size[i], input.price[i], input.timestamp);
                        orders.Add(order);
                    }
                }
            }

            return orders;
        }


        private void createOutput(TradeProcess trProcess) //single instrument
        {
            int barI = trProcess.timeLine.Count - 1;

            double profit = trProcess.profitLine[barI];
            double drawdown = trProcess.drawdownLine[barI];
            double exposure = trProcess.exposureLine[barI];
            string timestamp = getTimestamp(trProcess.timeLine[barI]);

            var output = new Output(config, logger, trModel, tradeId, timestamp, trProcess.instrConfig.instr_name, profit, drawdown, exposure);
            outputs.Add(output);
        }


        public void createPortfolioOutput(Portfolio portfolio)
        {
            int barI = portfolio.timeLine.Count - 1;

            double profit = portfolio.profitLine[barI];
            double drawdown = portfolio.drawdownLine[barI];
            double exposure = portfolio.exposureLine[barI];
            string timestamp = getTimestamp(portfolio.timeLine[barI]);

            var output = new Output(config, logger, trModel, tradeId, timestamp, "Portfolio", profit, drawdown, exposure);
            outputs.Add(output);
        }


        private string getTimestamp(DateTime time)
        {
            return time.ToString("yyyyMMdd HHmmss");
        }


        private void saveEvent(string instr, DBar bar)
        {


        }

    }
}
