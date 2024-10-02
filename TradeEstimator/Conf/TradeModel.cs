using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeEstimator.Conf
{
    public class TradeModel

    {
        public string tr_model_name;


        //Time

        public List<string> times;

        public List<TimeOnly> timepoints;


        //Trade

        public double slippageFactor;


        //Targets

        //percent of ADR
        public int half_range_adrp;

        //percent of ADR
        public int th_adrp;

        //percent of ADR
        public int tp_adrp;

        //in minutes
        public int min_duration1;
        public int min_duration2;

        public TimeSpan time1; //targets indicator related: start time
        public TimeSpan time2; //targets indicator related: end time 

        public TimeSpan timeA; //trade window (pos opening) start
        public TimeSpan timeB; //trade window (pos opening) end
        public TimeSpan timeC; //trade window (pos closing) end



        public TradeModel(string tr_model_name)
        {
            this.tr_model_name = tr_model_name;

            IniFile INI = new IniFile("configuration/models/trade/" + tr_model_name + ".ini");


            //Time

            string[] sss = INI.Read("times", "Time").Trim(' ', ',').Split(',');

            //DateTime dt = DateTime.ParseExact(s[0], "yyyyMMdd HHmmss", CultureInfo.InvariantCulture);



            //return;


            times = new List<string>();
            timepoints = new List<TimeOnly>();

            foreach (string s_time in sss)
            {
                string t = s_time.Trim();
                times.Add(t);
                timepoints.Add(TimeOnly.ParseExact(t, "HHmm", CultureInfo.InvariantCulture));
            }


            //Trade

            slippageFactor = double.Parse(INI.Read("slippage_factor", "Trade").Trim(), System.Globalization.CultureInfo.InvariantCulture);

            timeA = DateTime.ParseExact(INI.Read("time_a", "Trade").Trim(), "HHmm", System.Globalization.CultureInfo.InvariantCulture).TimeOfDay;
            timeB = DateTime.ParseExact(INI.Read("time_b", "Trade").Trim(), "HHmm", System.Globalization.CultureInfo.InvariantCulture).TimeOfDay;
            timeC = DateTime.ParseExact(INI.Read("time_c", "Trade").Trim(), "HHmm", System.Globalization.CultureInfo.InvariantCulture).TimeOfDay;


            //Targets

            half_range_adrp = int.Parse(INI.Read("half_range_adrp", "Targets").Trim(), System.Globalization.CultureInfo.InvariantCulture);

            th_adrp = int.Parse(INI.Read("th_adrp", "Targets").Trim(), System.Globalization.CultureInfo.InvariantCulture);

            tp_adrp = int.Parse(INI.Read("tp_adrp", "Targets").Trim(), System.Globalization.CultureInfo.InvariantCulture);

            min_duration1 = int.Parse(INI.Read("min_duration1", "Targets").Trim(), System.Globalization.CultureInfo.InvariantCulture);
            min_duration2 = int.Parse(INI.Read("min_duration2", "Targets").Trim(), System.Globalization.CultureInfo.InvariantCulture);
           
            time1 = DateTime.ParseExact(INI.Read("time1", "Targets").Trim(), "HHmm", System.Globalization.CultureInfo.InvariantCulture).TimeOfDay;
            time2 = DateTime.ParseExact(INI.Read("time2", "Targets").Trim(), "HHmm", System.Globalization.CultureInfo.InvariantCulture).TimeOfDay;

            //debug
            /*
            MessageBox.Show(time1.ToString());
            MessageBox.Show(time2.ToString());
            */


        }

    }
}
