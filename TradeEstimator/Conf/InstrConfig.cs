using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeEstimator.Conf

{
    public class InstrConfig
    {

        public string instr_name;

        public int scales_n;

        //Instr

        public int instr_number;

        public double instr_tick;

        public string export_price_format;

        public string better_price_format;

        //Num

        public Double[] gann_scale;

        public Double[] wh_scale;

        //Ind

        public int min_half_range;

        //Trade

        public int leverage;
        public double lot;
        public double spread;
        public double commission;


        public InstrConfig(string instr_name)
        {

            this.instr_name = instr_name;

            IniFile INI = new IniFile("configuration/instruments/" + instr_name + ".ini");

            scales_n = 9;


            //Instr

            instr_number = int.Parse(INI.Read("instr_number", "Instr").Trim(), System.Globalization.CultureInfo.InvariantCulture);

            instr_tick = double.Parse(INI.Read("instr_tick", "Instr").Trim(), System.Globalization.CultureInfo.InvariantCulture);

            switch (instr_tick)
            {
                case 0.1:
                    export_price_format = "F1";
                    better_price_format = "F2";
                    break;
                case 0.01:
                    export_price_format = "F2";
                    better_price_format = "F3";
                    break;
                case 0.001:
                    export_price_format = "F3";
                    better_price_format = "F4";
                    break;
                case 0.0001:
                    export_price_format = "F4";
                    better_price_format = "F5";
                    break;
                case 0.00001:
                    export_price_format = "F5";
                    better_price_format = "F6";
                    break;
                case 1:
                    export_price_format = "D";
                    better_price_format = "F1";
                    break;
                default:
                    export_price_format = "F4";
                    better_price_format = "F5";
                    break;
            }

            //Num

            gann_scale = new Double[scales_n];
            wh_scale = new Double[scales_n];

            for (int i = 0; i < scales_n; i++)
            {
                gann_scale[i] = double.Parse(INI.Read("gann_scale" + i.ToString(), "Num").Trim(), System.Globalization.CultureInfo.InvariantCulture);
                wh_scale[i] = double.Parse(INI.Read("wh_scale" + i.ToString(), "Num").Trim(), System.Globalization.CultureInfo.InvariantCulture);
            }

            //Ind
            min_half_range = int.Parse(INI.Read("min_half_range", "Ind").Trim(), System.Globalization.CultureInfo.InvariantCulture);


            //Trade

            leverage = int.Parse(INI.Read("leverage", "Trade").Trim(), System.Globalization.CultureInfo.InvariantCulture);

            lot = double.Parse(INI.Read("lot", "Trade").Trim(), System.Globalization.CultureInfo.InvariantCulture);

            spread = double.Parse(INI.Read("spread", "Trade").Trim(), System.Globalization.CultureInfo.InvariantCulture);

            commission = double.Parse(INI.Read("commission", "Trade").Trim(), System.Globalization.CultureInfo.InvariantCulture);
        }


    }
}
