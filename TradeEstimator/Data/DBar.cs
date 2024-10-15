using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeEstimator.Data
{
    public class DBar
    {
        public DateTime time;

        public double open;

        public double high;

        public double low;

        public double close;


        public DBar(DateTime time, double open, double high, double low, double close)
        {
            this.time = time;

            this.open = open;
            this.high = high;
            this.low = low;
            this.close = close;
        }


    }
}
