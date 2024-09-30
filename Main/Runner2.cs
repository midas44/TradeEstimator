using TradeEstimator.Charts;
using TradeEstimator.Conf;
using OpenTK.Audio.OpenAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeEstimator.Main
{
    public partial class Runner
    {

        //Runner2


        Chart2 chart2;

        int planetI;
        int figureI;
        int aspectI;
        int mirrorI;

        int planetN;
        int figureN;
        int aspectN;
        int mirrorN;


        public void createChart2()
        {
            chart2 = new(config, logger);

            f1.set_chart2(chart2);
        }


        public void outputData2()
        {
            chart2.displayInstrDataOHLC(instrConfig, daysQuotes);

            chart2.display_grids(daysQuotes, instrGrids.gridSets, displayDate1, displayDate2);

            //chart2.finalize();
        }


        public void outputIndicators2()
        {
            getAnIterUiState();

        }




        public void setIterRnd()
        {

        }

        
        public void setIterUI()
        {

        }


        public void getIterUI()
        {

        }



        public void setIterPrev()
        {

        }


        public void setIterNext()
        {

        }


        public void set_prev_planet()
        {

        }


        public void set_next_planet()
        {

        }




/*

        private AstronumModel setAspects(AstronumModel initial_model)
        {
            AstronumModel changed_model = initial_model;



            List<string> aspects_list = new List<string>();
            List<int> aspect_angles_list = new List<int>();


            foreach (string aspect in initial_model.aspects)
            {
                if (isShowAspect(aspect))
                {

                    if (aspect == "CNJ")
                    {
                        aspects_list.Add(aspect);
                        aspect_angles_list.Add(0);
                    }

                    if (aspect == "OPP")
                    {
                        aspects_list.Add(aspect);
                        aspect_angles_list.Add(180);
                    }

                    if (aspect == "SQR")
                    {
                        aspects_list.Add(aspect);
                        aspects_list.Add(aspect);
                        aspect_angles_list.Add(90);
                        aspect_angles_list.Add(270);
                    }

                    if (aspect == "TRI")
                    {
                        aspects_list.Add(aspect);
                        aspects_list.Add(aspect);
                        aspect_angles_list.Add(120);
                        aspect_angles_list.Add(240);
                    }

                    if (aspect == "SEX")
                    {
                        aspects_list.Add(aspect);
                        aspects_list.Add(aspect);
                        aspect_angles_list.Add(60);
                        aspect_angles_list.Add(300);
                    }

                    if (aspect == "QNS")
                    {
                        aspects_list.Add(aspect);
                        aspects_list.Add(aspect);
                        aspect_angles_list.Add(150);
                        aspect_angles_list.Add(210);
                    }

                }
            }

            string[] aspects = aspects_list.ToArray();
            int[] aspect_angles = aspect_angles_list.ToArray();

            changed_model.aspects = aspects;
            changed_model.aspect_angles = aspect_angles;

            return changed_model;

        }





        private bool isShowAspect(string asp)
        {
            if (f1.isCNJ() && asp == "CNJ")
            { return true; }


            if (f1.isOPP() && asp == "OPP")
            { return true; }


            if (f1.isSQRTRI() && (asp == "SQR" || asp == "TRI"))
            { return true; }


            if (f1.isSEXQNS() && (asp == "SEX" || asp == "QNS"))
            { return true; }


            return false;
        }

*/


    }
}
