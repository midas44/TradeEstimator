using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeEstimator.Data;

namespace TradeEstimator.Trade
{
    public class Order
    {
        //params
        public string instr; // instr name
        public string dir; //BUY, SELL
        public int size;   //lots 
        public double triggerPrice;
        public string timestamp;

        //runtime
        public bool isActive;


        public Order(string instr, string dir, int size, double triggerPrice, string timestamp)
        {
            this.instr = instr;
            this.dir = dir;
            this.size = size;
            this.triggerPrice = triggerPrice;
            this.timestamp = timestamp;

            isActive = true;
        }


        public void deactivate()
        {
            isActive = false;
        }

    }


}
