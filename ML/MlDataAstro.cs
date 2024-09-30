using TradeEstimator.Conf;
using TradeEstimator.Data;
using OpenTK.Audio.OpenAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TradeEstimator.ML
{
    public partial class MlData  //NOT READY - calc disabled
    {
        /*
        public void add_astro_params()
        {

            int planet_i = 0;
            foreach (string planet_name in an_model.planets)
            {
                //PLANET

                int aspect_i = 0;
                foreach (string aspect in an_model.aspects)
                {
                    //ASPECT

                    double aspect_angle = an_model.aspect_angles[aspect_i];

                    int mirror_i = 0;
                    foreach (bool mirror_state in an_model.mirror_state) // false / true
                    {
                        //MIRROR

                        string name = "astro_" + planet_name.ToLower() + "_" + aspect.ToLower() + aspect_angle.ToString() + "_" + an_model.mirror[mirror_i];

                        string short_mirror = an_model.mirror[mirror_i].Substring(0,1);

                        add_param("double", name);  //ps, ph, pw
                            
                        mirror_i++;
                        //MIRROR
                    }

                    aspect_i++;
                    //ASPECT
                }

                planet_i++;
                //PLANET
            }

        }
        */

    }
}