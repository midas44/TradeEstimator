using TradeEstimator.Conf;
using TradeEstimator.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeEstimator.ML
{
    public class MlExport
    {
        Config config;
        Logger logger;
        MlModel ml_model;

        String path;
        String path_input;
        String path_output;
        String path_hyper;

        String app_path;

        String config_path;
        String models_astronum_path;
        String models_tarde_path;
        String models_ml_path;
        String config_instruments_path;


        public MlExport(Config config, Logger logger, MlModel ml_model, string timestamp)
        {
            this.config = config;
            this.logger = logger;
            this.ml_model = ml_model;

            logger.logTitle("Export", 1);

            path = config.ml_path + "/" + timestamp;

            create_dir(path);

            path_input = path + "/input";
            path_output = path + "/output";
            path_hyper = path + "/hyper";

            create_dir(path_input);
            create_dir(path_output);
            create_dir(path_hyper);

            app_path = Application.StartupPath;

            config_path = app_path + "/" + "configuration";
            models_astronum_path = app_path + "/" + "configuration/models/astronum";
            models_tarde_path = app_path + "/" + "configuration/models/trade";
            models_ml_path = app_path + "/" + "configuration/models/ml";
            config_instruments_path = app_path + "/" + "configuration/instruments";

            copy_configs();
        }


        private void copy_configs()
        {
            copy_file(config_path, path_hyper, "config", "ini");

            copy_file(models_tarde_path, path_hyper, config.chosen_tr_model, "ini");
            copy_file(models_ml_path, path_hyper, config.chosen_ml_model, "ini");

            foreach(string instr in config.instruments)
            {
                copy_file(config_instruments_path, path_hyper, instr, "ini");
            }
        }


        private void create_dir(string dir_path)
        {
            if (!Directory.Exists(dir_path))
            {
                Directory.CreateDirectory(dir_path);
            }
        }


        public String get_path()
        {
            return path;
        }


        public void save_dataset(List<string[]> dataset)
        {           
            save_dataset_to_file(dataset, ml_model.export_filename);
        }


        public void save_dataset_to_file(List<string[]> dataset, string filename)
        {
            string target = path_output + "/" + filename + "." + ml_model.ml_data_ext;

            logger.log("dataset.Count = " + dataset.Count.ToString(), 1);

            if (dataset.Count > 0)
            {
                List<string> csv = new();

                int n = dataset[0].Length;

                for (int i = 0; i < n; i++)
                {
                    string line = "";

                    foreach (string[] sub_set in dataset)
                    {
                        string s = sub_set[i];
                        line += s + ",";
                    }

                    csv.Add(line);
                }
                File.WriteAllLines(target, csv);
            }
        }


        private void copy_file(string source_path, string target_path, string filename, string ext)
        {
            string source = source_path + "/" + filename + "." + ext;
            string target = target_path + "/" + filename + "." + ext;

            File.Copy(source, target);
        }

    }
}
