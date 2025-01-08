using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static OpenTK.Graphics.OpenGL.GL;
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


        public List<DBar> dBars;


        int lastIndex;



        public DaysQuotes(string instrument, DateTime day1, DateTime day2, int tf, 
            DateTime[] Timeline, Double[] Open, Double[] High, Double[] Low, Double[] Close, Double[] Volume, Double[] DR, Double[] ADR, bool createDbars) 
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


            TimeSpan ts1 = new TimeSpan(0, 0, 0);
            time1 = day1.Date + ts1;

            TimeSpan ts2 = new TimeSpan(23, 59, 59);
            time2 = day2.Date + ts2;

            if (createDbars)
            {
                createDBars();
            }

            lastIndex = 0;
        }


        private void createDBars()
        {
            dBars = new();

            DateTime timeI = time1;

            int lasti = 0;

            int n = Timeline.Length;

            if (n == 0) { return; }

            bool key = true;

            double lastPrice = 0;

            if ((DateTime.Compare(Timeline[0], time1) >= 0) && (DateTime.Compare(Timeline[0], time2) <= 0))
            {
                lastPrice = Open[0]; //(case of non-existing prev bar)
            }

            while (key)
            {

                bool isBarCreated = false;

                for (int i = lasti; i < n; i++)
                {

                    if (DateTime.Compare(Timeline[i], timeI) == 0)
                    {
                        DBar dBar = new(Timeline[i], Open[i], High[i], Low[i], Close[i]);
                        dBars.Add(dBar);
                        lasti = i+1;
                        isBarCreated = true;
                        lastPrice = Close[i]; //fix (case of existing prev bar)
                        break;
                    }
                    else
                    {
                        if (DateTime.Compare(Timeline[i], timeI) > 0)
                        {
                            break;
                        }

                    }

                }

                if (!isBarCreated) 
                {
                    DBar dBar = new(timeI, lastPrice, lastPrice, lastPrice, lastPrice);
                    dBars.Add(dBar);
                }


                timeI = timeI.AddMinutes(tf);


                if (DateTime.Compare(timeI, time2) > 0)
                {
                    key = false;
                }
                
            }

        }


        public DBar getDBar(DateTime timeI)
        {
            DBar dBar = null;

            int n = dBars.Count;

            if (n > 0)
            {
                for (int i = lastIndex; i < n; i++)
                {
                    if (DateTime.Compare(dBars[i].time, timeI) == 0)
                    {
                        lastIndex = i;
                        return dBars[lastIndex];
                    }
                    else
                    {
                        if (DateTime.Compare(dBars[i].time, timeI) > 0)
                        {
                            break;
                        }

                    }
                }
            }

            return dBar;
        }


        public int getBarI()
        {
            return lastIndex;
        }


    }
}
