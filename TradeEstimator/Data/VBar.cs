using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeEstimator.Data
{
    public class VBar
    {
        //params

        DBar bar0;

        DBar bar1;


        //runtime

        public DateTime time;

        public double open;

        public double high;

        public double low;

        public double close;


        public VBar(DBar bar0, DBar bar1)
        {
            this.bar0 = bar0;
            this.bar1 = bar1;

            time = bar1.time;
            open = bar0.close; //!
            high = bar1.high;
            low = bar1.low;
            close = bar1.close;

        }


    }
}
