using TradeEstimator.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeEstimator.Trade
{



    public class Gridset
    {
        public string type;

        public DateTime[] timeline_daybreak;

        public double[] price_daybreak;


        public Gridset(string type, int trade_start_index, int trade_end_index, DaysQuotes Bars, double half_range)
        {
            this.type = type;

            timeline_daybreak = new DateTime[2];

            price_daybreak = new double[2];

            int daybreak_index = trade_start_index;
            DateTime day = Bars.Timeline[daybreak_index].Date;
            while (DateTime.Compare(day, Bars.Timeline[daybreak_index].Date) == 0 && daybreak_index < trade_end_index)
            {
                daybreak_index++;
            }

            timeline_daybreak[0] = Bars.Timeline[daybreak_index];
            timeline_daybreak[1] = Bars.Timeline[daybreak_index];

            price_daybreak[0] = Bars.Open[daybreak_index] - 3 * half_range;
            price_daybreak[1] = Bars.Open[daybreak_index] + 3 * half_range;
        }
    }

/*
    public class GridsetOLD
    {
        public string type;

        public DateTime[] timeline_trade_start;
        public DateTime[] timeline_trade_end;
        public DateTime[] timeline_daybreak;

        public double[] price_trade_start;
        public double[] price_trade_end;
        public double[] price_daybreak;


        public GridsetOLD(string type, int trade_start_index, int trade_end_index, DaysQuotes Bars, double half_range)
        {
            this.type = type;

            timeline_trade_start = new DateTime[2];
            timeline_trade_end = new DateTime[2];
            timeline_daybreak = new DateTime[2];

            price_trade_start = new double[2];
            price_trade_end = new double[2];
            price_daybreak = new double[2];

            timeline_trade_start[0] = Bars.Timeline[trade_start_index];
            timeline_trade_start[1] = Bars.Timeline[trade_start_index];

            timeline_trade_end[0] = Bars.Timeline[trade_end_index];
            timeline_trade_end[1] = Bars.Timeline[trade_end_index];

            price_trade_start[0] = Bars.Open[trade_start_index] - half_range;
            price_trade_start[1] = Bars.Open[trade_start_index] + half_range;

            price_trade_end[0] = Bars.Open[trade_end_index] - half_range;
            price_trade_end[1] = Bars.Open[trade_end_index] + half_range;

            int daybreak_index = trade_start_index;
            DateTime day = Bars.Timeline[daybreak_index].Date;
            while(DateTime.Compare(day, Bars.Timeline[daybreak_index].Date)==0 && daybreak_index < trade_end_index)
            {
                daybreak_index++;
            }

            timeline_daybreak[0] = Bars.Timeline[daybreak_index];
            timeline_daybreak[1] = Bars.Timeline[daybreak_index];

            price_daybreak[0] = Bars.Open[daybreak_index] - 3 * half_range; 
            price_daybreak[1] = Bars.Open[daybreak_index] + 3 * half_range;
        }
    }
*/
}