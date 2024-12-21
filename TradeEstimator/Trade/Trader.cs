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
        public List<string> instruments;
        //int[] lastBar;
        int instrN;
        InputTimes inputTimes; 
        public TradeProcess[] trProcesses;
        public Portfolio portfolio;

        //output
        public List <Output> outputs; //pass to chart



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

            instrDaysQuotes = new();

            foreach (InstrConfig instrConfig in instrConfigs)
            {
                Quotes instrQuotes = all_data.getInstrQuotes(instrConfig.instr_name);
                DaysQuotes daysQuotes = instrQuotes.get_days_quotes_no_adr(trModel, time1, time2);
                //logger.log(" time1 = " + time1.ToString(config.full_datetime_format), 2);
                //logger.log(" time2 = " + time2.ToString(config.full_datetime_format), 2);
                instrDaysQuotes.Add(daysQuotes);
                instruments.Add(instrConfig.instr_name);
            }

            instrN = instruments.Count;

            //lastBar = new int[instrN];

            for(int i = 0; i < instrN; i++)
            {
                logger.log(instruments[i], 2);
                logger.log("instrDaysQuotes[i].dBars.Count = " + instrDaysQuotes[i].dBars.Count.ToString(), 2);
                logger.log("instrDaysQuotes[i].Timeline.Length = " + instrDaysQuotes[i].Timeline.Length.ToString(), 2);
                //lastBar[i] = instrDaysQuotes[i].dBars.Count - 1; // max index, not length!
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
            logger.log("Run", 2);

            DateTime timeI = time1;
            
            MessageBox.Show("START  tf=" + config.tf.ToString() + "m" +  "  time1 = " + time1.ToString() + "  time2 = " + time2.ToString() + "  timeI = " + timeI.ToString());

            int barN = instrDaysQuotes[0].dBars.Count; //must be equal count for all instruments!

            for (int barI = 0; barI < barN; barI++) // bar cycle : : : : : : : : : : : : : : : : : : : : : : :
            {

                for (int instrI = 0; instrI < instrN; instrI++) // instr cycle - - - - - - - - -
                {



                    //TODO: rewrite on getDbar by time!!

                    //NEW

                    /*

                    VBar bar = null;

                    DBar bar0 = null;
                    DBar bar1 = instrDaysQuotes[instrI].getDBar(timeI);

                    int barI = instrDaysQuotes[instrI].getBarI();

                    
                    if (barI > 0 && barI< instrDaysQuotes[instrI].dBars.Count)
                    {

                        
                        int i = instrDaysQuotes[instrI].dBars.Count;

                        if (barI >= i)
                        {
                            MessageBox.Show("dBars.Count = " + i.ToString() + "  barI = " + barI.ToString());
                        }

                            


                        bar0 = instrDaysQuotes[instrI].dBars[barI];
                        bar = new(bar0, bar1); //virtual bar!
                    }

                    if (barI == 0)
                    {
                       // int i = instrDaysQuotes[instrI].dBars.Count;

                        //MessageBox.Show(i.ToString());


                        bar0 = instrDaysQuotes[instrI].dBars[barI];   //BUG
                        bar = new(bar1, bar1); //virtual bar!   //here!
                    }

                    */

                    /*
                    if (barI == 0)
                    {
                        //First bar

                        //bar1 = bar0;

                        

                        logger.log("First bar", 2);

                        logger.log("process first bar: " + bar1.time.ToString(config.full_datetime_format), 2);

                        trProcesses[instrI].clearOrders(bar1.time);

                        trProcesses[instrI].processFirstBar(bar1);
                       
                    }

                    else
                    {

                        if (barI == instrDaysQuotes[instrI].dBars.Count -1)
                        {
                            //Last bar
                            logger.log("Last bar", 2);

                            logger.log("process last bar: " + bar.time.ToString(config.full_datetime_format), 2);

                            trProcesses[instrI].clearOrders(bar0.time);

                            trProcesses[instrI].processLastBar(bar);

                            createOutput(trProcesses[instrI]);
                        }
                        else
                        {
                            //DEBUG disabled
                            // if (DateTime.Compare(bar.time, timeI) == 0) // never set inputs on first bar! 
                            // {
                            Input? input = getInput(instruments[instrI], bar);


                            if (input != null)
                            {

                                logger.log("process bar: " + bar.time.ToString(config.full_datetime_format), 2);

                                logger.log("Input is here!", 2);

                                logger.log("n = " + input.n.ToString(), 2);

                                for (int i = 0; i < input.n; i++)
                                {
                                    logger.log("Input: " + i.ToString() + " " +
                                    input.instrument + " " + input.size[i].ToString() + " " + input.time.ToString(config.full_datetime_format) + " " +
                                    input.price[i].ToString(), 2);
                                }

                                var newOrders = createOrders(input, bar);

                                trProcesses[instrI].clearOrders(bar0.time);

                                trProcesses[instrI].addOrders(newOrders);

                                trProcesses[instrI].processBar(bar);
                            }


                            // }
                        }

                    }

                    */


                } // instr cycle - - - - - - - - - - - - - - - - - - - - - - - - - - 

               portfolio.update();

               timeI.AddMinutes(config.tf);

            } // bar cycle : : : : : : : : : : : : : : : : : : : : : : : : : : : : : :

            MessageBox.Show("END  tf=" + config.tf.ToString() + "m" + "  time1 = " + time1.ToString() + "  time2 = " + time2.ToString() + "  timeI = " + timeI.ToString());

            //createPortfolioOutput(portfolio); //DEBUG disabled!!!
        }



        private Input getInput(string instr, VBar bar) 
        {
            foreach(DateTime inputTime in inputTimes.itimes)
            {
                if(DateTime.Compare(inputTime, bar.time) == 0)
                {
                    return new Input(config, logger, trModel, tradeId, bar.time, instr);
                }
            }

            return null;
        }


        private List<Order> createOrders(Input input, VBar bar)
        {
            List<Order> orders = new();

            if (input != null && input.n>0)
            {
                for (int i = 0; i < input.n; i++) 
                {
                    if(input.size[i] != 0)
                    {
                        var order = createOrder(input, i, bar);
                        orders.Add(order);
                    }
                }
            }
            return orders;
        }


        private Order createOrder(Input input, int index, VBar bar)
        {

            string orderType = "market";

            if(input.size[index] > 0)
            {
                if(bar.close < input.price[index])
                {
                    orderType = "limit";
                }

                if (bar.close < input.price[index])
                {
                    orderType = "stop";
                }
            }
            else
            {
                if (bar.close < input.price[index])
                {
                    orderType = "stop";
                }

                if (bar.close < input.price[index])
                {
                    orderType = "limit";
                }
            }

            logger.log("createOrder: " + 
                input.instrument + " " + input.size[index] + " " + orderType + " " + input.price[index] + " " + input.time.ToString(config.full_datetime_format), 2);

            Order order = new(input.instrument, input.size[index], orderType, input.price[index], input.time);

            return order;
        }


        private void createOutput(TradeProcess trProcess) //single instrument
        {
            int barI = trProcess.timeLine.Count - 1;

            double profit = trProcess.equityLine[barI];
            double drawdown = trProcess.drawdownLine[barI];
            double exposure = trProcess.exposureLine[barI];
            string timestamp = getTimestamp(trProcess.timeLine[barI]);

            var output = new Output(config, logger, trModel, tradeId, timestamp, trProcess.instrConfig.instr_name, profit, drawdown, exposure, trProcess.inactiveOrders);
            outputs.Add(output);
        }


        public void createPortfolioOutput(Portfolio portfolio)
        {
            int barI = portfolio.timeLine.Count - 1; //problem here

            double profit = portfolio.profitLine[barI];
            double drawdown = portfolio.drawdownLine[barI];
            double exposure = portfolio.exposureLine[barI];
            string timestamp = getTimestamp(portfolio.timeLine[barI]);

            var output = new Output(config, logger, trModel, tradeId, timestamp, "Portfolio", profit, drawdown, exposure, null);
            outputs.Add(output);
        }


        private string getTimestamp(DateTime time) //TO DO: in one place (now in trader and ion tradeProcess)
        {
            return time.ToString("yyyyMMdd HHmmss");
        }


        private void saveEvent(string instr, DBar bar)
        {


        }

    }
}
