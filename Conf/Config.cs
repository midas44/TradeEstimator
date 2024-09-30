using TradeEstimator.Data;
using ScottPlot.TickGenerators.TimeUnits;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TradeEstimator.Conf
{
    public class Config
    {
        //Path

        public string data_path;

        public string quotes_path;

        public string ml_path;

        public string log_path;

        public string tsets_path;


        //Data

        public string data_timeframe;

        public string data_ext;


        //Chart

        public int chart_time_margins;

        //public int points_per_price_point;

        public double half_range_adr_exp;


        //UI
        public int form0_x;
        public int form0_y;

        public int form0_w;
        public int form0_h;

        public int form1_x;
        public int form1_y;

        public int form1_w;
        public int form1_h;


        public int ui_mode;

        public int timerange_index;

        public string tset_name;

        public DateTime chosen_date;

        public string chosen_instrument;


        //Log

        public int log_level;

        public string datetime_format;
        public string full_datetime_format;
        public string date_format;
        public string time_format;


        //Models

        public List<string> astronum_models;

        public List<string> trade_models;

        public List<string> ml_models;


        //Period

        public DateTime date1;

        public DateTime date2;


        //Instruments

        public List<string> instruments;


        //ML

        public string chosen_tr_model;
        public string chosen_ml_model;


        //Runtime
        //(not in ini file) * * * * * * * * * * * * *

        public int tf;

        public string export_value_format;
        public string export_angle_format;

        public bool ex_gauss_display;
        public bool mp_gauss_display;
        public bool qt_gauss_display;

        public bool ex_marker_display;
        public bool mp_marker_display;
        public bool qt_marker_display;

        public bool cos_display;
        //public bool hcircle_display; //New disabled - need not???

        public int begin_time_shift_days;



        public Config()
        {
            //CultureInfo System.Globalization.CultureInfo.InvariantCulture = CultureInfo.InvariantCulture;

            IniFile INI = new IniFile("configuration/config.ini");


            //Path

            data_path = INI.Read("data_path", "Path").Trim();
            quotes_path = INI.Read("quotes_path", "Path").Trim().Replace("[data_path]", data_path);
            ml_path = INI.Read("ml_path", "Path").Trim().Replace("[data_path]", data_path);
            log_path = INI.Read("log_path", "Path").Trim().Replace("[data_path]", data_path);
            tsets_path = INI.Read("tsets_path", "Path").Trim().Replace("[data_path]", data_path);

            //debug
            /*
            MessageBox.Show(quotes_path);
            MessageBox.Show(export_path);
            MessageBox.Show(log_path);
            */


            //Data

            data_timeframe = INI.Read("data_timeframe", "Data").Trim();

            data_ext = INI.Read("data_ext", "Data").Trim();


            //Chart

            chart_time_margins = int.Parse(INI.Read("chart_time_margins", "Chart").Trim(), System.Globalization.CultureInfo.InvariantCulture);

            //points_per_price_point = int.Parse(INI.Read("points_per_price_point", "Chart").Trim(), System.Globalization.CultureInfo.InvariantCulture);

            half_range_adr_exp = double.Parse(INI.Read("half_range_adr_exp", "Chart").Trim(), System.Globalization.CultureInfo.InvariantCulture);


            //UI

            form0_x = int.Parse(INI.Read("form0_x", "UI").Trim(), System.Globalization.CultureInfo.InvariantCulture);
            form0_y = int.Parse(INI.Read("form0_y", "UI").Trim(), System.Globalization.CultureInfo.InvariantCulture);

            form0_w = int.Parse(INI.Read("form0_w", "UI").Trim(), System.Globalization.CultureInfo.InvariantCulture);
            form0_h = int.Parse(INI.Read("form0_h", "UI").Trim(), System.Globalization.CultureInfo.InvariantCulture);

            form1_x = int.Parse(INI.Read("form1_x", "UI").Trim(), System.Globalization.CultureInfo.InvariantCulture);
            form1_y = int.Parse(INI.Read("form1_y", "UI").Trim(), System.Globalization.CultureInfo.InvariantCulture);

            form1_w = int.Parse(INI.Read("form1_w", "UI").Trim(), System.Globalization.CultureInfo.InvariantCulture);
            form1_h = int.Parse(INI.Read("form1_h", "UI").Trim(), System.Globalization.CultureInfo.InvariantCulture);


            ui_mode = int.Parse(INI.Read("ui_mode", "UI").Trim(), System.Globalization.CultureInfo.InvariantCulture);

            timerange_index = int.Parse(INI.Read("timerange_index", "UI").Trim(), System.Globalization.CultureInfo.InvariantCulture);

            tset_name = INI.Read("tset_name", "UI").Trim();

            chosen_date = DateTime.ParseExact(INI.Read("chosen_date", "UI").Trim(), "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);

            chosen_instrument = INI.Read("chosen_instrument", "UI").Trim();


            //Log

            log_level = int.Parse(INI.Read("log_level", "Log").Trim(), System.Globalization.CultureInfo.InvariantCulture);

            datetime_format = INI.Read("datetime_format", "Log").Trim();
            full_datetime_format = INI.Read("full_datetime_format", "Log").Trim();
            date_format = INI.Read("date_format", "Log").Trim();
            time_format = INI.Read("time_format", "Log").Trim();



            //return;


            //Models

            astronum_models = new List<string>();

            string line1 = INI.Read("astronum_models", "Models").Trim(' ', ',');
            string[] s1 = line1.Split(',');

            foreach (string name in s1)
            {
                astronum_models.Add(name.Trim());
            }


            trade_models = new List<string>();

            string line2 = INI.Read("trade_models", "Models").Trim(' ', ',');
            string[] s2 = line2.Split(',');

            foreach (string name in s2)
            {
                trade_models.Add(name.Trim());
            }


            ml_models = new List<string>();

            string line3 = INI.Read("ml_models", "Models").Trim(' ', ',');
            string[] s3 = line3.Split(',');

            foreach (string name in s3)
            {
                ml_models.Add(name.Trim());
            }


            //Period

            date1 = DateTime.ParseExact(INI.Read("date1", "Period").Trim(), "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);

            date2 = DateTime.ParseExact(INI.Read("date2", "Period").Trim(), "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);


            //Instruments

            instruments = new List<string>();

            string line4 = INI.Read("instruments", "Instruments").Trim(' ', ',');
            string[] s4 = line4.Split(',');

            foreach (string name in s4)
            {
                instruments.Add(name.Trim());
            }


            //ML

            chosen_tr_model = INI.Read("chosen_tr_model", "ML").Trim();

            chosen_ml_model = INI.Read("chosen_ml_model", "ML").Trim();



            //Runtime
            //(not in ini file) * * * * * * * * * * * * *

            switch (data_timeframe)
            {
                case "m1":
                    tf = 1;
                    break;

                case "m":
                    tf = 1;
                    break;

                case "m2":
                    tf = 2;
                    break;

                case "m3":
                    tf = 3;
                    break;

                case "m4":
                    tf = 4;
                    break;

                case "m5":
                    tf = 5;
                    break;

                case "m10":
                    tf = 10;
                    break;

                case "m15":
                    tf = 15;
                    break;

                case "m30":
                    tf = 30;
                    break;

                case "h1":
                    tf = 60;
                    break;

                case "h":
                    tf = 60;
                    break;

                case "h2":
                    tf = 120;
                    break;

                case "h4":
                    tf = 240;
                    break;

                default:
                    tf = 1;
                    break;
            }

            //TODO: read from INI?
            //TODO: remove drem here, new in ml_model
            export_value_format = "F4";
            export_angle_format = "F4";

            ex_marker_display = true;
            mp_marker_display = true;
            qt_marker_display = true;

            cos_display = true;
            //hcircle_display = false; //New disabled - need not???

            begin_time_shift_days = 60;

            //tests = new();
        }

    }
}
