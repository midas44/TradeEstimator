using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeEstimator.Test
{

    public class InputSubSet
    {

        public string instr; // instr name

        public string dir; //BUY, SELL
        public int size;   //lots   
        public string type; //"limit", "stop", "market" //not in use yet

        public double triggerPrice;
        public double tpPrice;
        public double slPrice;
        public double bePrice; //meaning: sl to trigger price or?


        public InputSubSet(string instr, string dir, int size, string type, double triggerPrice, double tpPrice, double slPrice, double bePrice)
        {
            this.instr = instr;
            this.dir = dir;
            this.size = size;
            this.type = type;
            this.triggerPrice = triggerPrice;
            this.tpPrice = tpPrice;
            this.slPrice = slPrice;
            this.bePrice = bePrice;

        }


    }
}
