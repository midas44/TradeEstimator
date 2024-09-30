using TradeEstimator.Conf;
using TradeEstimator.Data;
using TradeEstimator.Log;
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
        Config config;

        Logger logger;

        MlModel ml_model;

        TradeModel tr_model;

        List<List<string>> dataset;

        int random_angle;

        bool[] angles;


        public MlData(Config config, Logger logger, MlModel ml_model, TradeModel tr_model)  //instr name, adr, day of week? , fx session
        {
            this.config = config;
            this.logger = logger;
            this.ml_model = ml_model;
            this.tr_model = tr_model;

            logger.logTitle("MlData", 1);

            dataset = new List<List<string>>();

            add_trade_params();
            add_price_params();

            add_targets_params();

            random_angle = 0;
            angles = new bool[360];
            set_angles();
        }


        private double apply_random_angle(double ini_angle)
        {
            double angle = ini_angle + random_angle;

            if (angle >= 360)
            {
                angle -= 360;
            }
            return angle;   
        }


        private void set_angles()
        {
            logger.log("set_angles",1);

            for (int i = 0; i < 360; i++) 
            {
                angles[i] = true;       
            }
        }


        public void set_random_angle()
        {
            if (!ml_model.randomize_angle)
            {
                random_angle = 0;
            }
            else
            {
                random_angle = -1;
                var random = new Random();
                int i = random.Next(360); // creates a number between 0 and 359

                if (angles[i])
                {
                    angles[i] = false;
                    random_angle = i;
                }
                else
                {
                    int l = 0;
                    for (int j = 1; j < 360; j++)
                    {
                        int m = l + i + j;
                        if (m > 359)
                        {
                            l = -360;
                            m = l + i + j;
                        }
                        if (angles[m])
                        {
                            angles[m] = false;
                            random_angle = m;
                            return;
                        }
                    }
                    set_angles();
                    random_angle = i;
                }                
            }
        }


        public void add_param(String type, String name)
        {
            List<string> subset = new();

            subset.Add(type);
            subset.Add(name);

            dataset.Add(subset);
        }


        public void add_value(String name, String value)
        {
            foreach(var subset in dataset) 
            {
                if (subset[1] == name)
                {
                    subset.Add(value);
                }
            }
        }


        private double normalize_angle(double angle_value)
        {
            if (!ml_model.normalize_angle) { return angle_value; }
            return angle_value / 360;
        }


        private double denormalize_angle(double norm_angle_value)
        {
            return norm_angle_value * 360;
        }


        private double normalize_price(double price_value, double min, double max)
        {
            if (!ml_model.normalize_price) {  return price_value; }
            return (price_value - min) / (max - min);
        }


        private double denormalize_price(double norm_price_value, double min, double max)
        {
            return (norm_price_value * (max - min)) + min;
        }



        public List<string[]> get_dataset()
        {
            List<string[]> ds = new();

            foreach (var subset in dataset)
            {
                ds.Add(subset.ToArray());
            }
                return ds;
        }


    }
}
