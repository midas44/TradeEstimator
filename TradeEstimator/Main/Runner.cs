using TradeEstimator.Charts;
using TradeEstimator.Conf;
using TradeEstimator.Data;
using TradeEstimator.Log;
using TradeEstimator.Trade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeEstimator.Main
{

    public partial class Runner
    {
        public Config config;

        public Logger logger;

        RunnerBase runnerBase;

        public TradeModel trModel;

        string trModelName;

        string mlModelName;

        string anModelName;

        Form1 f1;

        public Tsets tset;

        public bool isBusy;

        bool chart1_rescale_key;



        public Runner(RunnerBase runnerBase)
        {
            isBusy = true;

            this.runnerBase = runnerBase;
            config = runnerBase.config;
            logger = runnerBase.logger;

            createModels();

            f1 = Form1.form1;

            f1.setConfig(config);

            setupUI();

            run();
        }


        private void createModels()
        {
            createTrModel();
        }


        public TradeModel createTrModel()
        {
            trModel = new(config.chosen_tr_model);
            return trModel;
        }


        public void run()
        {
            if (f1.noRun) { return; }


            chart1_rescale_key = true;


            f1.reset_chosen_timepoint();
            getUIstate();

            switch (config.ui_mode)
            {
                case 0:

                    break;

                case 1:
                    getInstrData();
                    break;
            }

            tradesRun();
        }


        public void tradesRun()
        {
            if (f1.noRun) { return; }

            getUIstate();


            switch (config.ui_mode)
            {
                case 0:

                    break;

                case 1:
                    createTrades_withUI();
                    createChart1();
                    outputData1();
                    outputTrades1();
                    finalize1();
                    break;
            }

            //end of exe sequence!
            isBusy = false;
        }


        private void finalize1()
        {
            chart1.setMark(f1.getChart1MarkX(),0); //debug disabled

            f1.showMark2();
            chart1.mark(); 

            if (chart1_rescale_key) //only on run event
            {
                f1.chart1YRange = 3.0;

               // f1.set_chart1_scale_xy();

                chart1_rescale_key = false;
            }

            chart1.finalize();
        }


        private void setupUI()
        {
            f1.noRun = true;

            setupTset();
            
            switch (config.ui_mode)
            {
                case 0:
                    f1.setupMode_0();
                    break;

                case 1:
                    f1.setupMode_1();
                    setTimingUI();
                    break;
            }
           
            f1.noRun = false;
        }


        public void setupTset()
        {
            string name = config.tset_name;
            logger.log_("Tset", 2);
            tset = new(config, name);
            f1.setupTsetUI(tset.astroNumPoints);
        }


        private void getUIstate()
        {
            Application.DoEvents();

            instrName = f1.get_combobox_instrument_text();

            trModelName = f1.get_combobox_tr_model_text();
 

            getTimingUI();

            //getIterUI();

            Application.DoEvents();
        }


        private int[] getChosenTimepoint() //use for manual analysis
        {
            int timepoint_i = f1.get_chosen_timepoint();

            int[] single_timepoint = { timepoint_i };

            return single_timepoint;
        }


        private void getAnIterUiState()
        {

        }

        public void createTrades_withUI() //only for UI modes (1,2)!
        {
            int[] timepoints = getChosenTimepoint();



        }



    }


}
