using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeEstimator.Trade;


namespace TradeEstimator.Data
{
    public class DaysQuotes
    {

        public string instrument;

        public DateTime day1;
        public DateTime day2;

        public DateTime[] Timeline;
        public Double[] Open;
        public Double[] High;
        public Double[] Low;
        public Double[] Close;
        public Double[] Volume;
        public Double[] DR;
        public Double[] ADR;

        int n;

        int lastIndex;

        public double prevHigh;
        public double prevLow;


        public DaysQuotes(string instrument, DateTime day1, DateTime day2, DateTime[] Timeline, Double[] Open, Double[] High, Double[] Low, Double[] Close, Double[] Volume, Double[] DR, Double[] ADR) 
        {
            this.instrument = instrument;
            this.day1 = day1;
            this.day2 = day2;
            this.Timeline = Timeline;
            this.Open = Open;
            this.High = High;
            this.Low = Low;
            this.Close = Close;
            this.Volume = Volume;
            this.DR = DR;
            this.ADR = ADR;

            n = Timeline.Length - 1;

            lastIndex = 0;

            prevHigh = -1;
            prevLow = -1;
        }


        public Bar getBar(DateTime time)
        {
            Bar bar;

            int i = lastIndex;

            while (true)
            {
                i++; //i always > 0 !!!

                if (i >= n)
                {
                    bar = new(instrument, Timeline[n], Open[n], High[n], Low[n], Close[n], Volume[n], DR[n], ADR[n]);
                    prevHigh = High[n - 1];
                    prevLow = Low[n - 1];
                    break;
                }

                if (DateTime.Compare(Timeline[i], time) >= 0)
                {                    
                    bar = new(instrument, Timeline[i], Open[i], High[i], Low[i], Close[i], Volume[i], DR[i], ADR[i]);
                    prevHigh = High[i - 1];
                    prevLow = Low[i - 1];
                    break;
                }                
            }

            lastIndex = i;
        
            return bar;
        }


    }
}
