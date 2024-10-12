using TradeEstimator.Charts;
using TradeEstimator.Conf;
using TradeEstimator.Data;
using TradeEstimator.Log;
using ScottPlot.Palettes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeEstimator.Trade1
{
    public class Robot
    {

        Config config;

        Logger logger;

        TradeModel trModel;

        public InstrConfig instrConfig; //portfolio!

        public List<Order> orders;

        public List<Position> positions;

        public Equity equity;

        int timeInc; //min


        public Robot(Config config, Logger logger, TradeModel trModel, InstrConfig instrConfig) // Single instrument robot
        {
            this.config = config;
            this.logger = logger;

            this.trModel = trModel;
            this.instrConfig = instrConfig;

            orders = orders;

            logger.log_("Robot " + instrConfig.instr_name, 1);

            orders = new List<Order>();
            positions = new List<Position>();
            equity = new Equity(instrConfig);

            timeInc = 1;

            switch (config.data_timeframe.ToLower())
            {
                case "m1":
                    timeInc = 1;
                    break;

                case "m2":
                    timeInc = 2;
                    break;

                case "m3":
                    timeInc = 3;
                    break;

                case "m5":
                    timeInc = 5;
                    break;

                case "m10":
                    timeInc = 10;
                    break;

                case "m15":
                    timeInc = 15;
                    break;

                case "m30":
                    timeInc = 30;
                    break;

                case "h1":
                    timeInc = 60;
                    break;

                case "h2":
                    timeInc = 120;
                    break;

                case "h4":
                    timeInc = 240;
                    break;

                case "h6":
                    timeInc = 360;
                    break;

                case "h12":
                    timeInc = 720;
                    break;

                default:
                    timeInc = 1;
                    break;
            }
        }



        public void trade(InputSet inputSet, DateTime theDay, List<DaysQuotes> daysQuotesList) //call from Tester
        {
            /*
            public TimeSpan timeA; //trade window (pos opening) start
            public TimeSpan timeB; //trade window (pos opening) end
            public TimeSpan timeC; //trade window (pos closing) end
             */

            theDay = theDay.Date;

            DateTime time1 = theDay.Add(trModel.timeA);
            DateTime time2 = theDay.Add(trModel.timeB);
            DateTime time3 = theDay.Add(trModel.timeC);
            DateTime time4 = time3.AddMinutes(timeInc);

            createOrders(inputSet, time1, time2);

            bool key = true;

            DateTime time = time1;

            while (key)
            {

                foreach (var quotes in daysQuotesList)
                {
                    //need  prev bar close!
                    Bar bar = quotes.getBar(time);
                    double prevHigh = quotes.prevHigh;
                    double prevLow = quotes.prevLow;

                    foreach (Order order in orders)
                    {
                        order.checkExpiration(bar);

                        if (order.isActive)
                        {

                            if (order.dir == "buy")
                            {




                            }

                            if (order.dir == "sell")
                            {




                            }



                            if (
                                bar.high >= order.triggerPrice && bar.low <= order.triggerPrice
                                ||
                                bar.low >= order.triggerPrice && order.dir == "buy" //??  //prev bar and current bar
                                ||
                                bar.high <= order.triggerPrice && order.dir == "sell" && order.type == "limit" //???
                                )
                            {
                                order.diactivate();
                            }
                        }
                    }

                }

                time = time.AddMinutes(timeInc);

                if (DateTime.Compare(time4, time) > 0)
                {
                    key = false;
                }
            }


        }


        private void createOrders(InputSet inputSet, DateTime time1, DateTime time2)
        {
            foreach (var set in inputSet.inputList)
            {
                string id = set.type + " " + set.dir + " " + set.instr + " " + set.size.ToString() + " tr:" + set.triggerPrice.ToString() + " tp:" + set.tpPrice.ToString() + " sl:" + set.slPrice.ToString();
                Order order = new(set.instr, set.dir, set.size, set.type, set.triggerPrice, set.tpPrice, set.slPrice, set.bePrice, time1, time2, id);
                orders.Add(order);
            }
        }





    }
}