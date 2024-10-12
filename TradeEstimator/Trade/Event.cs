using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeEstimator.Trade
{
    public class Event
    {

        public Order order;


        public Event(Order order)
        {
            this.order = order;

        }


    }
}
