using TradeEstimator.Conf;
using TradeEstimator.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static OpenTK.Graphics.OpenGL.GL;

namespace TradeEstimator.ML
{
    public partial class MlData
    {
        public void add_trade_params()
        {
            add_param("date", "date");
            add_param("time", "time");
            add_param("string", "day_of_week");
            add_param("string", "instrument");
            add_param("string", "fxsession"); 
            add_param("int", "random_angle");
        }


        public void create_trade_data(InstrConfig instr_config, DateTime timepoint)
        {
            add_value("date", timepoint.Date.ToString("yyyy.MM.dd"));

            add_value("time", timepoint.Date.ToString("HH:mm"));

            add_value("day_of_week", timepoint.Date.ToString("ddd"));

            add_value("instrument", instr_config.instr_name);

            add_value("fxsession", get_fx_session(timepoint)); // NOT READY

            add_value("random_angle", random_angle.ToString());
        }


        private string get_fx_session(DateTime time) //TODO: add correct hours.add DST for years
        {
            int h = time.TimeOfDay.Hours;



            if (h >= 0 && h < 8)
            {
                return "a"; //asia 
            }
            else if (h < 13)
            {
                return "e"; //europe
            }
            else if (h < 20)
            {
                return "m"; //america
            }
            else
            {
                return "n"; //no trade
            }
        }



    }
}
