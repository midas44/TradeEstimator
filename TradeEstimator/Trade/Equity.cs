using TradeEstimator.Conf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeEstimator.Trade
{
    public class Equity
    {
        public string instr;

        public List<double> equityLine;

        public List<DateTime> timeLine;

        public double finalProfit;

        public double maxDrawdown;

        double maxProfit;
        double minProfit;


        public Equity(InstrConfig instrConfig)
        {
            instr = instrConfig.instr_name;

            equityLine = new List<double>();

            timeLine = new List<DateTime>();

            finalProfit = 0;
            maxDrawdown = 0;

            maxProfit = 0;
            minProfit = 0;
        }


        public void addEquityPoint(DateTime timePoint, double equityPoint)
        {
            timeLine.Add(timePoint);
            equityLine.Add(equityPoint);

            if (equityPoint > maxProfit)
            {
                maxProfit = equityPoint;
            }

            if (equityPoint < minProfit)
            {
                minProfit = equityPoint;
            }

            maxDrawdown = maxProfit - minProfit;
        }


    }
}