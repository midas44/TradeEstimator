using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeEstimator.Conf;
using TradeEstimator.Data;
using TradeEstimator.Log;

namespace TradeEstimator.Trade
{
    public class TradeProcessor
    {
        //params

        public Config config;

        public Logger logger;

        QuotesLoader all_data;

        List<string> instruments;

        public TradeModel trModel;

        public DateTime time1;

        public DateTime time2;


        //runtime

        List<InstrConfig> instrConfig_list;

        List<Quotes> instrQuotes_list;

        public Input input;

        public Output output;

        public List<Event> events;



        public TradeProcessor(Config config, Logger logger, QuotesLoader all_data, List<string> instruments, TradeModel trModel, DateTime time1, DateTime time2) //create from runner or some controller
        {
            this.config = config;
            this.logger = logger;
            this.all_data = all_data;
            this.instruments = instruments;
            this.trModel = trModel;
            this.time1 = time1;
            this.time2 = time2;

            logger.logTitle("TradeProcessor", 1);

            events = new();

            prepareData();

            getInputs();

            process();

            createOutputs();
        }



        private void prepareData()
        {
            foreach (string instr in instruments)
            {
                InstrConfig instrConfig = new(instr);

                instrConfig_list.Add(instrConfig);

                Quotes instrQuotes = all_data.getInstrQuotes(instr); // in time range!!!!!

                instrQuotes_list.Add(instrQuotes);
            }
        }



        public void getInputs() 
        { 
        
        }


        public void process() 
        { 
        
        
        }


        public void saveEvent() 
        { 
        
        
        }



        public void createOutputs() 
        { 
        

        }



    }
}
