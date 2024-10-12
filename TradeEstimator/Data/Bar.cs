using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeEstimator.Data
{
    public class Bar
    {

        public string instr;

        public DateTime time;

        public double open;

        public double high;

        public double low;

        public double close;

        public double volume;

        public double dr;

        public double adr;


        public Bar(string instr, DateTime time, double open, double high, double low, double close, double volume, double dr, double adr)
        {
            this.instr = instr;

            this.time = time;

            this.open = open;
            this.high = high;
            this.low = low;
            this.close = close;

            this.volume = volume;

            this.dr = dr;
            this.adr = adr;
        }


    }
}
