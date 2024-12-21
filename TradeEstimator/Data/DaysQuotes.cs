using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;


namespace TradeEstimator.Data
{
    public class DaysQuotes
    {

        public string instrument;

        public DateTime day1;
        public DateTime day2;

        public int tf;

        public DateTime time1;
        public DateTime time2;

        public DateTime[] Timeline;
        public Double[] Open;
        public Double[] High;
        public Double[] Low;
        public Double[] Close;
        public Double[] Volume;
        public Double[] DR;
        public Double[] ADR;

        int lastIndex;

        public double prevHigh;
        public double prevLow;

        public List<DBar> dBars;



        public DaysQuotes(string instrument, DateTime day1, DateTime day2, int tf, DateTime[] Timeline, Double[] Open, Double[] High, Double[] Low, Double[] Close, Double[] Volume, Double[] DR, Double[] ADR) 
        {
            this.instrument = instrument;
            this.day1 = day1;
            this.day2 = day2;
            this.tf = tf;
            this.Timeline = Timeline;
            this.Open = Open;
            this.High = High;
            this.Low = Low;
            this.Close = Close;
            this.Volume = Volume;
            this.DR = DR;
            this.ADR = ADR;

            
            prevHigh = -1;
            prevLow = -1;

            TimeSpan ts1 = new TimeSpan(0, 0, 0);
            time1 = day1.Date + ts1;

            TimeSpan ts2 = new TimeSpan(23, 59, 59);
            time2 = day2.Date + ts2;

            createDBars();
        }


        private void createDBarsOld()
        {
            dBars = new();

            int n = Timeline.Length;
            int i = 0;
            bool key = true;

            while (key)
            {
                if (DateTime.Compare(Timeline[i], time1) >= 0 && DateTime.Compare(Timeline[i], time2) <= 0)
                {
                    DBar dBar = new(Timeline[i], Open[i], High[i], Low[i], Close[i]);
                    dBars.Add(dBar);
                }
                else
                {
                    if (DateTime.Compare(Timeline[i], time2) > 0)
                    {
                        key = false;
                    }
                }

                i++;

                if (i >= n) { key = false; }
            }
        }


        private void createDBars()
        {
            dBars = new();

            int n = Timeline.Length;
            int i = 0;

            DBar dBar0;

            foreach (DateTime t in Timeline)
            {                
                if (DateTime.Compare(t, time1) >= 0) {
                    dBar0 = new(Timeline[i], Open[i], High[i], Low[i], Close[i]);
                    i--;
                    break; 
                }
                i++;
            }


            DateTime timeI = time1;

            bool key = true;

            while (key)
            {
                DBar dBar;

                if (DateTime.Compare(Timeline[i], timeI) == 0)
                {
                    dBar = new(Timeline[i], Open[i], High[i], Low[i], Close[i]);
                    dBars.Add(dBar);
                }


                if (DateTime.Compare(timeI, time2) == 0)
                {
                    key = false;
                }

                //dBar0 = dBar;

            }
        }


    }
}
