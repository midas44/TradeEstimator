using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeEstimator.Data
{
    public partial class Quotes
    {

        private double find_daily_range(int i1, int i2)
        {
            if (i1 < 0 || i2<=0) { return -1; }

            DateTime the_day = Timeline[i1].Date;

            if (the_day.DayOfWeek == DayOfWeek.Saturday || the_day.DayOfWeek == DayOfWeek.Sunday) { return -1; }

            double day_low = 999999;

            double day_high = 0;

            for(int i = i1; i<= i2; i++)
            {
                if (day_low > Low[i])
                {
                    day_low = Low[i];
                }

                if (day_high < High[i])
                {
                    day_high = High[i];
                }               
            }

            if (day_low < day_high)
            {
                return day_high - day_low;
            }
            else
            {
                return -1;
            }
        }


        private double find_average_daily_range(int i1, int i2)
        {
            double result = 0; 


            return result;
        }


        /*
            public double find_adr(int period)
        {
            if (period > DR.Length)
            {
                return -1;
            }

            int i = DR.Length - 1;
            int k = 0;
            int j = 0;

            double sum = 0;

            while (k < period && j <= i)
            {
                double range_value = DR[i - j];

                if (range_value > 0)
                {
                    sum += range_value;
                    k++;
                } 
                j++;    
            }
            return sum/period;
        }
        */

    }
}
