using TradeEstimator.Charts;
using TradeEstimator.Conf;
using TradeEstimator.Data;
using TradeEstimator.Log;
using ScottPlot;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeEstimator.Trade
{
    public class Grids
    {
        Config config;
        TradeModel tr_model;
        Logger logger;

        double half_range; //in real price
        double th; //in real price
        double tp; //in real price

        int minduration1; //in bars
        int minduration2; //in bars

        TimeSpan time1;
        TimeSpan time2;

        int index1; //day start index
        int index2; //day end index

        DateTime active_date;
        DateTime next_date;

        int rev_last_extremum_index;
        int rev_new_bottom_index;
        int rev_new_top_index;

        DaysQuotes Bars;

        int min_duration1_;
        int min_duration2_;

        int tf;
        int half_range_adrp;
        int th_adrp;
        int tp_adrp;


        List<Gridset> Grids_list;
        public Gridset[] gridSets;


        public Grids(Config config, Logger logger, TradeModel tr_model, DaysQuotes days_quotes, DateTime date1, DateTime date2)
        {
            this.config = config;
            this.tr_model = tr_model;
            this.logger = logger;
            Bars = days_quotes;

            logger.log_("Grids: " + days_quotes.instrument + " " + date1.ToString(config.date_format) + " - " + date2.ToString(config.date_format), 1);

            Grids_list = new List<Gridset>();

            time1 = tr_model.time1;
            time2 = tr_model.time2;

            min_duration1_ = tr_model.min_duration1;
            min_duration2_ = tr_model.min_duration2;

            tf = config.tf;

            half_range_adrp = tr_model.half_range_adrp;

            th_adrp = tr_model.th_adrp;

            tp_adrp = tr_model.tp_adrp;

            minduration1 = (int)Math.Ceiling((double)min_duration1_ / tf);
            minduration2 = (int)Math.Ceiling((double)min_duration2_ / tf);

            index1 = -1;
            index2 = -1;

            rev_last_extremum_index = 0;
            rev_new_bottom_index = 0; ;
            rev_new_top_index = 0;

            for (int i = 0; i < Bars.Timeline.Length; i++)
            {
                calculate_grids_only(i);
            }

            gridSets = Grids_list.ToArray();
        }


        public void calculate_grids_only(int index)
        {
            DateTime bar_date = Bars.Timeline[index].Date;

            TimeSpan bar_time = Bars.Timeline[index].TimeOfDay;

            DayOfWeek bar_wday = Bars.Timeline[index].DayOfWeek;

            if (index1 < 0 && (bar_wday != DayOfWeek.Saturday && bar_wday != DayOfWeek.Friday))
            {
                if (TimeSpan.Compare(bar_time, time1) >= 0)
                {
                    index1 = index;
                    index2 = -1;
                    active_date = bar_date;
                    next_date = active_date.AddDays(1).Date;
                }
            }
            else
            {
                if (index2 < 0 && DateTime.Compare(next_date, bar_date) == 0)
                {
                    if (TimeSpan.Compare(bar_time, time2) >= 0)
                    {
                        index2 = index;

                        double adr = Bars.ADR[index];

                        if (adr > 0)
                        {
                            half_range = half_range_adrp * adr / 100;
                            th = th_adrp * adr / 100;
                            tp = tp_adrp * adr / 100;

                            Gridset gridset = new("trade_grid", index1, index2, Bars, half_range);
                            Grids_list.Add(gridset);
                            Application.DoEvents();
                        }
                        index1 = -1;
                    }
                }
            }
        }
    }
}
