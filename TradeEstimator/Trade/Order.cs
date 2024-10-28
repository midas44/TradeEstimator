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
        public string type; //limit, stop, market    //TO DO: some hybrids?
        public double triggerPrice;

        public DateTime startTime;

        //runtime
        public bool isActive;
        public DateTime endTime;
        public bool isTriggered;
        public double entryPrice;


        public Order(string instr, int size, string type, double triggerPrice, DateTime startTime)
        {
            this.instr = instr;
            this.size = size;
            this.triggerPrice = triggerPrice;
            this.startTime = startTime;
            
            isActive = true;
            isTriggered = false;

            entryPrice = -1;

            dir = "";

            if (size > 0)
            {
                dir = "BUY";
            }

            if (size < 0)
            {
                dir = "SELL";
            }

            if(size == 0)
            {
                isActive = false;
            }
        }


        public void deactivate(DateTime time)
        {
            isActive = false;
            endTime = time;
        }

    }


}
