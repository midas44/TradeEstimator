using Microsoft.VisualBasic;
using TradeEstimator.Conf;
using TradeEstimator.Log;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeEstimator.Data
{
    public partial class Quotes
    {
        Config config;
        Logger logger;

        public string instrument;

        string path;

        DateTime[] Timeline;
        Double[] Open;
        Double[] High;
        Double[] Low;
        Double[] Close;
        Double[] Volume;
        Double[] DR;
        Double[] ADR;

        List<DateTime> Timeline_list;
        List<Double> Open_list;
        List<Double> High_list;
        List<Double> Low_list;
        List<Double> Close_list;
        List<Double> Volume_list;

        List<DateTime> Days;
        List<int[]> DaysIndexes;

        
        public Quotes(Config config, Logger logger, string instrument) 
        {
            this.config = config;
            this.logger = logger;

            this.instrument = instrument;

            path = config.quotes_path + "/" + instrument + "_" + config.data_timeframe + "." + config.data_ext;

            Timeline_list = new();
            Open_list = new();
            High_list = new();
            Low_list = new();
            Close_list = new();
            Volume_list = new();

            Days = new();
            DaysIndexes = new();

            load();

            Application.DoEvents();

            find_dr();

            Application.DoEvents();

        }


        private void load()
        {
            logger.log_("Quotes load: " + instrument, 1);

            string[] dataset0 = System.IO.File.ReadAllLines(path);

            string[] dataset = dataset0.Where(item => item != string.Empty).ToArray();

            DateTime curr_day = DateTime.ParseExact("19010101", "yyyyMMdd", CultureInfo.InvariantCulture).Date;

            DateTime last_day = curr_day;

            DateTime first_day = DateTime.ParseExact("21500101", "yyyyMMdd", CultureInfo.InvariantCulture).Date;

            bool first_day_key = true;

            int i = 0;
            int j = 0;

            foreach (string line in dataset)
            {
                //20101017 195200; 1.3977; 1.3977; 1.3977; 1.3977; 500000

                string[] s = line.Split(';');

                DateTime dt = DateTime.ParseExact(s[0], "yyyyMMdd HHmmss", CultureInfo.InvariantCulture);

                if (first_day_key)
                {
                    if (first_day > dt.Date)
                    {
                        first_day = dt.Date;
                        first_day_key = false;
                    }
                }

                TimeSpan begin_time_shift = TimeSpan.FromDays(config.begin_time_shift_days);
                DateTime date0 = config.date1.Add(-begin_time_shift);

                if (DateTime.Compare(dt.Date, date0) >= 0 && DateTime.Compare(dt.Date, config.date2) <= 0)
                {
                    Timeline_list.Add(dt);
                    Open_list.Add(Double.Parse(s[1].Replace(',', '.'), CultureInfo.InvariantCulture));
                    High_list.Add(Double.Parse(s[2].Replace(',', '.'), CultureInfo.InvariantCulture));
                    Low_list.Add(Double.Parse(s[3].Replace(',', '.'), CultureInfo.InvariantCulture));
                    Close_list.Add(Double.Parse(s[4].Replace(',', '.'), CultureInfo.InvariantCulture));
                    Volume_list.Add(Double.Parse(s[5].Replace(',', '.'), CultureInfo.InvariantCulture));

                    curr_day = dt.Date;

                    if (curr_day > last_day)
                    {
                        Days.Add(curr_day);

                        DaysIndexes.Add(new int[2]);

                        DaysIndexes[j][0] = i;

                        if (j > 0)
                        {
                            DaysIndexes[j-1][1] = i-1;
                        }
                        
                        last_day = curr_day;

                        j++;
                    }

                    i++;
                }
            }

            DaysIndexes[j - 1][1] = i - 1; //last index

            Timeline = Timeline_list.ToArray();
            Open = Open_list.ToArray();
            High = High_list.ToArray();
            Low = Low_list.ToArray();
            Close = Close_list.ToArray();
            Volume = Volume_list.ToArray();

            string msg = "loaded data: " + instrument + " " + config.data_timeframe + " " + first_day.ToString(config.date_format) + " - " + last_day.ToString(config.date_format);
            logger.log(msg, 2);
        }


        private void find_dr()
        {
            DR = new double[Timeline.Length];

            foreach(DateTime day in Days)
            {
                int dayIndex = Days.IndexOf(day);
                int[] indexes = DaysIndexes[dayIndex];

                int day_index1 = indexes[0];
                int day_index2 = indexes[1];

                double dayily_range = find_daily_range(day_index1, day_index2);

                for (int i = day_index1; i <= day_index2; i++)
                {
                    DR[i] = dayily_range;
                }
            }
        }


        public DaysQuotes get_days_quotes(TradeModel tr_model, DateTime day1, DateTime day2)
        {
            day1 = validate(day1);
            day2 = validate(day2);

            int day1Index = Days.IndexOf(day1);
            int day2Index = Days.IndexOf(day2);

            int[] indexes1 = DaysIndexes[day1Index];
            int[] indexes2 = DaysIndexes[day2Index];

            int index1 = indexes1[0];

            int index2 = indexes2[1];

            int n = index2 - index1 + 1;

            DateTime[] dayTimeline = new DateTime[n];
            Double[] dayOpen = new Double[n];
            Double[] dayHigh = new Double[n];
            Double[] dayLow = new Double[n];
            Double[] dayClose = new Double[n];
            Double[] dayVolume = new Double[n];
            Double[] dayDR = new Double[n];
            Double[] dayADR = new Double[n];

            calc_adr(config.adr_period, day1, day2);

            int j = 0;
            for (int i = index1; i <= index2; i++)
            {
                dayTimeline[j] = Timeline[i];
                dayOpen[j] = Open[i];
                dayHigh[j] = High[i];
                dayLow[j] = Low[i];
                dayClose[j] = Close[i];
                dayVolume[j] = Volume[i];
                dayDR[j] = DR[i];
                dayADR[j] = ADR[i];
                j++;
            }

            return new(instrument, day1, day2, config.tf, dayTimeline, dayOpen, dayHigh, dayLow, dayClose, dayVolume, dayDR, dayADR);
        }

        public DaysQuotes get_days_quotes_no_adr(TradeModel tr_model, DateTime day1, DateTime day2)
        {

            day1 = validate(day1);
            day2 = validate(day2);

            int day1Index = Days.IndexOf(day1);
            int day2Index = Days.IndexOf(day2);

            int[] indexes1 = DaysIndexes[day1Index];
            int[] indexes2 = DaysIndexes[day2Index];

            int index1 = indexes1[0];

            int index2 = indexes2[1];

            int n = index2 - index1 + 1;

            DateTime[] dayTimeline = new DateTime[n];
            Double[] dayOpen = new Double[n];
            Double[] dayHigh = new Double[n];
            Double[] dayLow = new Double[n];
            Double[] dayClose = new Double[n];
            Double[] dayVolume = new Double[n];
            Double[] dayDR = new Double[n];
            Double[] dayADR = new Double[n];

            //calc_adr(tr_model, day1, day2);

            int j = 0;
            for (int i = index1; i <= index2; i++)
            {
                dayTimeline[j] = Timeline[i];
                dayOpen[j] = Open[i];
                dayHigh[j] = High[i];
                dayLow[j] = Low[i];
                dayClose[j] = Close[i];
                dayVolume[j] = Volume[i];
                dayDR[j] = DR[i];
                dayADR[j] = 0;
                j++;
            }

            return new(instrument, day1, day2, config.tf, dayTimeline, dayOpen, dayHigh, dayLow, dayClose, dayVolume, dayDR, dayADR);
        }



        private void calc_adr(int adr_period, DateTime day1, DateTime day2)
        {
            ADR = new double[Timeline.Length];

            DateTime sd = day1.AddDays(-1);
            DateTime ed = day2.AddDays(-1);

            foreach (DateTime day in Days)
            {
                if(day >= sd && day <= ed)
                {
                    double value = 0;
                    int i = 0;
                    int j = 0;
                    bool key = true;

                    while (key)
                    {
                        DateTime calc_day = day.AddDays(-i);

                        int calc_dayIndex = Days.IndexOf(calc_day);
                        
                        if (calc_dayIndex >= 0)
                        {
                            int[] calc_indexes = DaysIndexes[calc_dayIndex];
                            int calc_index = calc_indexes[0];
                            double dr = DR[calc_index];

                            if (dr > 0)
                            {
                                j++;
                                value += dr;
                            }

                            if (j >= adr_period) { key = false; }
                        }

                        if (i >= adr_period*3) { MessageBox.Show("calc_adr error"); }
                        i++;
                    }

                    if (value > 0)
                    {
                        double adr = value / adr_period;

                        DateTime the_day = day.AddDays(1);

                        int the_dayIndex = Days.IndexOf(the_day);

                        if (the_dayIndex>=0)
                        {
                            int[] indexes = DaysIndexes[the_dayIndex];

                            int index1 = indexes[0];
                            int index2 = indexes[1];                            

                            for (int l = index1; l <= index2; l++)
                            {
                                ADR[l] = adr;
                            }
                        }
                    }
                }                
            }
        }



        public DateTime validate(DateTime day)
        {
            day = day.Date;

            DateTime result;
            
            if (Days.Contains(day))
            {
                result = day;
            }
            else
            {
                result = day.AddDays(1);

                if (!Days.Contains(result))
                {
                    result = day.AddDays(2);

                    if (!Days.Contains(result))
                    {
                        result = day.AddDays(-1);

                        if (!Days.Contains(result))
                        {
                            result = day.AddDays(-2);

                            if (!Days.Contains(result))
                            {
                                result = day.AddDays(3);

                                if (!Days.Contains(result))
                                {
                                    result = day.AddDays(-3);
                                }
                            }
                        }
                    }
                }
            }          
            return result;
        }



    }
}
