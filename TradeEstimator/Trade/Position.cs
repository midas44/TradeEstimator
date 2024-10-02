using TradeEstimator.Conf;
using TradeEstimator.Data;
using TradeEstimator.Log;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeEstimator.Trade
{
    public class Position
    {
        public string instr;
        public double tick;
        public double lot;
        public double spread;
        public double ispread;

        public double slippageFactor;
        public DateTime timeA;
        public DateTime timeB;
        public DateTime timeC;

        public string dir;
        int idir;
        public double size;

        public double entryPrice;
        public double exitPrice;
        public double takeProfitPrice;
        public double stopLossPrice;
        public double breakEvenPrice; //meaning: sl to trigger price or?

        public DateTime entryTime;
        public DateTime exitTime;

        public double profit;

        public bool isOpen;


        public Position(InstrConfig instrConfig, TradeModel trModel, Order order, Bar bar)
        {
            instr = instrConfig.instr_name;
            tick = instrConfig.instr_tick;
            lot = instrConfig.lot;
            spread = instrConfig.spread;

            ispread = instrConfig.instr_tick * instrConfig.spread;

            slippageFactor = trModel.slippageFactor;

            DateTime theDay = bar.time.Date; //midnight

            timeA = theDay.Add(trModel.timeA);
            timeB = theDay.Add(trModel.timeB);
            timeC = theDay.Add(trModel.timeC);

            dir = "";
            idir = 0;
            size = 0;

            profit = 0;

            isOpen = false;

            enterPos(order, bar);
        }


        private void enterPos(Order order, Bar bar)
        {
            if (DateTime.Compare(bar.time, timeB) >= 0 || DateTime.Compare(bar.time, timeA) < 0)
            {
                return;
            }

            entryPrice = order.triggerPrice + calculateSlippage(bar, order.triggerPrice);
            entryTime = bar.time;

            size = order.size;

            takeProfitPrice = order.tpPrice;
            stopLossPrice = order.slPrice;
            breakEvenPrice = order.bePrice;

            isOpen = true;
        }


        private double calculateSlippage(Bar bar, double price)
        {
            if (!isOpen) return 0;

            double slippage;


            //Gaps support - - -
            if (price > bar.high)
            {
                price = bar.high;
            }

            if (price < bar.low)
            {
                price = bar.low;
            }
            // - - - - - - - - -


            double buy_slippage = (bar.high - price) * slippageFactor;

            double sell_slippage = (price - bar.low) * slippageFactor;

            Random random = new();

            if (random.NextDouble() >= 0.5)
            {
                slippage = buy_slippage;
            }
            else
            {
                slippage = sell_slippage;
            }

            slippage *= random.NextDouble();

            return slippage;
        }


        public void processBar(Bar bar)
        {
            if (!isOpen) return;

            profit = calculateProfit(bar, bar.close);

            if (DateTime.Compare(bar.time, timeC) >= 0)
            {
                exitPos(bar, bar.open);
            }

            if (idir == 1)
            {
                // support gaps!
                if (stopLossPrice >= bar.low) stopLoss(bar); //BEFORE TP!!!
                if (takeProfitPrice <= bar.high) takeProfit(bar);
                if (breakEvenPrice <= bar.high) breakEven(bar);
            }

            if (idir == -1)
            {
                // support gaps!
                if (stopLossPrice <= bar.high) stopLoss(bar); //BEFORE TP!!!
                if (takeProfitPrice >= bar.low) takeProfit(bar);
                if (breakEvenPrice >= bar.low) breakEven(bar);
            }
        }


        private void stopLoss(Bar bar)
        {
            if (!isOpen) return;

            exitPos(bar, stopLossPrice);
        }


        private void takeProfit(Bar bar)
        {
            if (!isOpen) return;

            exitPos(bar, takeProfitPrice);
        }


        private void exitPos(Bar bar, double price)
        {
            if (!isOpen) return;

            exitPrice = price;
            exitTime = bar.time;

            profit = calculateExitProfit(bar, exitPrice);

            isOpen = false;
        }


        private void breakEven(Bar bar)
        {
            if (!isOpen) return;

            stopLossPrice = entryPrice;
        }


        private double calculateProfit(Bar bar, double price)
        {
            if (!isOpen) return profit;

            return size * (price - entryPrice) * idir / tick;

            //ATTENTION: result in POINTS, no conversion to account currency (yet)
        }


        private double calculateExitProfit(Bar bar, double price)
        {
            if (!isOpen) return profit;

            double slippage = calculateSlippage(bar, price);

            return size * ((price - entryPrice) * idir - ispread + slippage) / tick;

            //ATTENTION: result in POINTS, no conversion to account currency (yet)
        }

    }

}