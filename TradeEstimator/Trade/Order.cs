using TradeEstimator.Conf;
using TradeEstimator.Data;
using TradeEstimator.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeEstimator.Trade
{
    public class Order
    {
        //params
        public string instr; // instr name

        public string dir; //BUY, SELL
        public int size;   //lots   
        public string type; //"limit", "stop", "market" //not in use yet

        public double triggerPrice;
        public double tpPrice;
        public double slPrice;
        public double bePrice; //meaning: sl to trigger price or?

        public DateTime creationTime;
        public DateTime expirationTime;

        public string id;

        //runtime
        public bool isActive;


        public Order(string instr,
            string dir, int size, string type,
            double triggerPrice, double tpPrice, double slPrice, double bePrice,
            DateTime creationTime, DateTime expirationTime,
            string id)
        {
            this.instr = instr;
            this.dir = dir;
            this.size = size;
            this.type = type;

            this.triggerPrice = triggerPrice;
            this.tpPrice = tpPrice;
            this.slPrice = slPrice;
            this.bePrice = bePrice;

            this.creationTime = creationTime;
            this.expirationTime = expirationTime;

            this.id = id;

            isActive = true;
        }


        public void checkExpiration(Bar bar) //cycle all orders every bar!
        {
            if (DateTime.Compare(bar.time, expirationTime) >= 0)
            {
                isActive = false;
            }
        }


        public void diactivate()
        {
            isActive = false;
        }




    }

}