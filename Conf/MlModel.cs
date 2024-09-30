using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeEstimator.Conf
{
    public class MlModel

    {
        public string ml_model_name;


        //Data

        public string ml_data_ext;

        public string import_filename;

        public string export_filename;

        public string empty_data_placeholder;

        public double half_range_adr;

        public int half_range_points;

        public string norm_value_format;

        public int max_levels_number;

        public bool randomize_angle;

        public bool normalize_price;

        public bool normalize_angle;


        //NEW
        public int indicatorPointsNumber;

        public double indScale;

        public double zonesScale;

        //Filter1

        public bool filter1;

        public int proximity_zone;


        //Filter2

        public bool filter2;

        public int min_seed_level;


        //Filter3

        public bool filter3;

        public bool mp_seeds;

        public bool qt_seeds;



        //Filter4

        public bool filter4;



        public MlModel(string ml_model_name)
        {
            this.ml_model_name = ml_model_name;

            IniFile INI = new IniFile("configuration/models/ml/" + ml_model_name + ".ini");


            //Data

            ml_data_ext = INI.Read("ml_data_ext", "Data").Trim();

            import_filename = INI.Read("import_filename", "Data").Trim();

            export_filename = INI.Read("export_filename", "Data").Trim();


            empty_data_placeholder = INI.Read("empty_data_placeholder", "Data").Trim();

            half_range_adr = double.Parse(INI.Read("half_range_adr", "Data").Trim(), System.Globalization.CultureInfo.InvariantCulture);

            half_range_points = int.Parse(INI.Read("half_range_points", "Data").Trim(), System.Globalization.CultureInfo.InvariantCulture);

            norm_value_format = INI.Read("norm_value_format", "Data").Trim();

            max_levels_number = int.Parse(INI.Read("max_levels_number", "Data").Trim(), System.Globalization.CultureInfo.InvariantCulture);

            randomize_angle = bool.Parse(INI.Read("randomize_angle", "Data").Trim());

            normalize_price = bool.Parse(INI.Read("normalize_price", "Data").Trim());

            normalize_angle = bool.Parse(INI.Read("normalize_angle", "Data").Trim());

            //NEW
            indicatorPointsNumber = int.Parse(INI.Read("indicatorPointsNumber", "Data").Trim(), System.Globalization.CultureInfo.InvariantCulture);


            indScale = double.Parse(INI.Read("indScale", "Data").Trim(), System.Globalization.CultureInfo.InvariantCulture);

            zonesScale = double.Parse(INI.Read("zonesScale", "Data").Trim(), System.Globalization.CultureInfo.InvariantCulture);


            //Filter1

            filter1 = bool.Parse(INI.Read("filter1", "Filter1").Trim());

            proximity_zone = int.Parse(INI.Read("proximity_zone", "Filter1").Trim(), System.Globalization.CultureInfo.InvariantCulture);


            //Filter2

            filter2 = bool.Parse(INI.Read("filter2", "Filter2").Trim());

            min_seed_level = int.Parse(INI.Read("min_seed_level", "Filter2").Trim(), System.Globalization.CultureInfo.InvariantCulture);


            //Filter3

            filter3 = bool.Parse(INI.Read("filter3", "Filter3").Trim());

            mp_seeds = bool.Parse(INI.Read("mp_seeds", "Filter3").Trim());

            qt_seeds = bool.Parse(INI.Read("qt_seeds", "Filter3").Trim());


            //Filter4

            filter4 = bool.Parse(INI.Read("filter4", "Filter4").Trim());

        }
    }
}
