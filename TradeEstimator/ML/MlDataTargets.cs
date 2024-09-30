using TradeEstimator.Conf;
using TradeEstimator.Trade;
using OpenTK.Audio.OpenAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TradeEstimator.ML
{
    public partial class MlData
    {
        public void add_targets_params()
        {
            string name;
            string id;

            for(int i=0; i<ml_model.max_levels_number; i++)
            {
                name = "level_entry--" + i.ToString();
                add_param("double", name);

                name = "level_exit--" + i.ToString();
                add_param("double", name);
            }
        }

        /*
        public void create_targets_data(InstrConfig instr_config, DateTime timepoint, double open_price, double adr, Targets instr_targets)
        {
            double price_min = open_price - adr * ml_model.half_range_adr;
            double price_max = open_price + adr * ml_model.half_range_adr;

            int i = 0;
            foreach (Levelset level_set in instr_targets.Levels)
            {

                if (timepoint.Date == level_set.timeline_entry[0].Date || timepoint.Date == level_set.timeline_entry[1].Date ||
                    timepoint.Date == level_set.timeline_exit[0].Date || timepoint.Date == level_set.timeline_exit[1].Date)
                {
                    if (i < ml_model.max_levels_number)
                    {
                        double level_entry_price = level_set.price_entry[0];
                        double norm_level_entry = normalize_price(level_entry_price, price_min, price_max);

                        if (norm_level_entry > 1)
                        {
                            add_value("level_entry--" + i.ToString(), ml_model.empty_data_placeholder);
                            logger.log_("level > 1", 1);
                        }
                        else if (norm_level_entry >= 0)
                        {
                            add_value("level_entry--" + i.ToString(), norm_level_entry.ToString(ml_model.norm_value_format));
                        }
                        else
                        {
                            add_value("level_entry--" + i.ToString(), ml_model.empty_data_placeholder);
                            logger.log_("level < 0", 1);
                        }

                        double level_exit_price = level_set.price_exit[0];
                        double norm_level_exit = normalize_price(level_exit_price, price_min, price_max);


                        if (norm_level_exit > 1)
                        {
                            add_value("level_exit--" + i.ToString(), ml_model.empty_data_placeholder);
                            logger.log_("level > 1", 1);
                        }
                        else if (norm_level_exit >= 0)
                        {
                            add_value("level_exit--" + i.ToString(), norm_level_exit.ToString(ml_model.norm_value_format));
                        }
                        else
                        {
                            add_value("level_exit--" + i.ToString(), ml_model.empty_data_placeholder);
                            logger.log_("level < 0", 1);
                        }
                        i++;
                    }
                }
            }


            if (i < ml_model.max_levels_number - 1)
            {
                for (int j = i; j < ml_model.max_levels_number; j++)
                {
                    add_value("level_entry--" + j.ToString(), ml_model.empty_data_placeholder);
                    add_value("level_exit--" + j.ToString(), ml_model.empty_data_placeholder);
                }
            }
        }
        */
    }

    
}
