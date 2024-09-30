using TradeEstimator.Conf;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeEstimator.ML
{
    public partial class MlData  //NOT READY! Clac disabled!
    {
        public void add_price_params()
        {
            add_param("int", "adr");
            add_param("double", "range_min");
            add_param("double", "range_maх");

            int p_n = ml_model.half_range_points * 2;

            for (int i = 0; i < p_n; i++)
            {
                //PRICELINE 

                string price_name = "price_point--" + i.ToString();
                add_param("double", price_name);  //ps, ph, pw

                //PRICELINE 
            }

            /*
            int figure_i = 0;
            foreach (string figure in an_model.figures)
            {
                //FIGURE

                int abs_ground;

                if (figure_i < 2) //TODO: make a method
                {
                    abs_ground = an_model.sq_hx_abs_ground;
                }
                else
                {
                    abs_ground = an_model.wh_abs_ground;
                }


                for(int j = 8; j>= abs_ground; j--)
                {
                
                    for (int i = 0; i < p_n; i++)
                    {
                        //PRICELINE
                        string num_name = "num_" + figure.ToLower() + j.ToString() + "_point--" + i.ToString();
                        add_param("double", num_name);  //ps, ph, pw
                        //PRICELINE 
                    }
                }

                figure_i++;
                //FIGURE
            */
            

        }


    }

}
