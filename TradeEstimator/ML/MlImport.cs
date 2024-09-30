using TradeEstimator.Conf;
using TradeEstimator.Log;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeEstimator.ML
{
    public class MlImport
    {
        Config config;
        Logger logger;
        MlModel ml_model;

        String path;
        String path_input;
        String path_output;


        public MlImport(Config config, Logger logger, MlModel ml_model, string timestamp)
        {
            this.config = config;
            this.logger = logger;
            this.ml_model = ml_model;

            logger.logTitle("Import", 1);

            path = config.ml_path + "/" + timestamp;

            path_input = path + "/input";
            path_output = path + "/output";
        }


        public List<string[]> load_col_data(string col_prefix, bool denorm_price, bool denorm_angle)
        {
            //string source = path_input + "/" + ml_model.import_filename + "." + ml_model.ml_data_ext;

            //TEMP
            string source = path_output + "/" + ml_model.export_filename + "." + ml_model.ml_data_ext;
            //TEMP

            string[] lines = File.ReadAllLines(source);

            string[] header = lines[1].Split(',');

            List<int> col_indexes = new();

            int col_i = 0;
            foreach (string s in header)
            {                 
                if (s.StartsWith(col_prefix))
                {
                    col_indexes.Add(col_i);
                }
                col_i++;
            }

            List<string[]> dataset = new();

            int n = lines.Length;

            foreach (int index in col_indexes)
            {
                string[] col = new string[n];

                int i = 0;
                foreach (string line in lines)
                {
                    string[] cols = line.Split(",");

                    string s = cols[index];

                    if (s != ml_model.empty_data_placeholder && i>1)
                    {
                        if (denorm_angle)
                        {
                            double v = double.Parse(s, System.Globalization.CultureInfo.InvariantCulture);
                            v = denormalize_angle(v);
                            s = v.ToString("F4"); 
                        }
                        else if (denorm_price)
                        {
                            //NOT READY


                            //TODO: get real range max, min
                            double range_min = 10;
                            double range_max = 30;

                            double v = double.Parse(s, System.Globalization.CultureInfo.InvariantCulture);
                            v = denormalize_price(v, range_min, range_max);
                            s = v.ToString("F5"); //TO DO: get instr better price format
                        }
                    }

                    col[i] = s;

                    i++;
                }

                dataset.Add(col);   
            }

            return dataset;
        }


        private double denormalize_angle(double norm_angle_value)
        {
            return norm_angle_value * 360;
        }


        private double denormalize_price(double norm_price_value, double min, double max)
        {
            return (norm_price_value * (max - min)) + min;
        }


    }


}
