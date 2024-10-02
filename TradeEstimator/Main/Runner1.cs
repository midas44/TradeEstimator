using TradeEstimator.Charts;
using TradeEstimator.Conf;
using TradeEstimator.Data;
using ScottPlot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TradeEstimator.Main
{
    public partial class Runner
    {
        //Runner1

        public InstrConfig instrConfig;

        Grids instrGrids;

        Quotes instrQuotes;

        DaysQuotes daysQuotes;


        public int timerange_index;

        DateTime displayDate1;

        DateTime displayDate2;

        string instrName;

        string timerange;

        TimeSpan timeDelta;

        TimeSpan timeSubDelta;



        public void getInstrData()
        {
            instrQuotes = runnerBase.allData.getInstrQuotes(instrName);

            daysQuotes = instrQuotes.get_days_quotes(trModel, displayDate1, displayDate2);

            instrConfig = new(instrName);

            instrGrids = new(config, logger, daysQuotes, displayDate1, displayDate2);
        }


        public void setTimingUI()
        {
            Application.DoEvents();
            getDisplayDates();
            setTimeDelta();
            setPeriod();
            setDisplayDates();
        }


        public void getTimingUI()
        {
            Application.DoEvents();
            getDisplayDates();
        }


        private void setTimeDelta()
        {
            timerange = f1.getActiveTimerange();

            switch (timerange)
            {
                case "day":
                    timeDelta = TimeSpan.FromDays(1);
                    break;

                case "2 days":
                    timeDelta = TimeSpan.FromDays(2);
                    break;

                case "3 days":
                    timeDelta = TimeSpan.FromDays(3);
                    break;

                case "week":
                    timeDelta = TimeSpan.FromDays(6);
                    break;

                case "2 weeks":
                    timeDelta = TimeSpan.FromDays(12);
                    break;

                case "month":  //TO DO: make calendar month, not 4 weeks
                    timeDelta = TimeSpan.FromDays(26);
                    break;

                case "year":
                    timeDelta = TimeSpan.FromDays(361);
                    break;
            }
        }


        private bool isWeekend(DateTime day)
        {
            return (day.DayOfWeek == DayOfWeek.Sunday || day.DayOfWeek == DayOfWeek.Saturday);
        }



        private DateTime goFuture(DateTime day, int daysNumber)
        {
            int counter = 0;

            while (counter < daysNumber || isWeekend(day))
            {
                day = day.AddDays(1);
                if (!isWeekend(day))
                {
                    counter++;
                }
            }
            return day;
        }


        private DateTime goPast(DateTime day, int daysNumber)
        {
            int counter = 0;

            while (counter < daysNumber || isWeekend(day))
            {
                day = day.AddDays(-1);
                if (!isWeekend(day))
                {
                    counter++;
                }
            }
            return day;
        }


        private DateTime goNextWeekDay(DateTime day, DayOfWeek weekDay)
        {
            while (day.DayOfWeek != weekDay)
            {
                day = day.AddDays(1);
            }
            return day;
        }


        private DateTime goPrevWeekDay(DateTime day, DayOfWeek weekDay)
        {
            while (day.DayOfWeek != weekDay)
            {
                day = day.AddDays(-1);
            }
            return day;
        }


        private DateTime goMonthLastDay(DateTime day)
        {
            return goMonthFirstDay(day).AddMonths(1).AddHours(-12);
        }


        private DateTime goMonthFirstDay(DateTime day)
        {
            return new DateTime(day.Year, day.Month, 1);
        }


        private DateTime goYearLastDay(DateTime day)
        {
            return goYearFirstDay(day).AddYears(1).AddHours(-12);
        }


        private DateTime goYearFirstDay(DateTime day)
        {
            return new DateTime(day.Year, 1, 1);
        }


        private void setDisplayDates()
        {
            f1.set_date1(displayDate1);
            f1.set_date2(displayDate2);
        }


        private void getDisplayDates()
        {
            displayDate1 = f1.get_date1();
            displayDate2 = f1.get_date2();
        }


        private void setPeriodForward()
        {
            displayDate2 = config.date2.Add(timeDelta).Date;
            displayDate1 = displayDate2;
            setPeriod();
        }


        private void setPeriodBack()
        {
            displayDate2 = config.date2.Add(-timeDelta).Date;
            displayDate1 = displayDate2;

            setPeriod();
        }


        private void setPeriod()
        {
            DateTime d1 = displayDate2.Date;
            DateTime d2 = displayDate2.Date;

            if (timerange == "day")
            {
                if (isWeekend(d2))
                {
                    d2 = goFuture(d2, 1);
                }
                d1 = d2;
            }

            if (timerange == "2 days")
            {
                if (isWeekend(d2))
                {
                    d2 = goFuture(d2, 1);
                }
                d1 = goPast(d2, 1);
            }

            if (timerange == "3 days")
            {
                if (isWeekend(d2))
                {
                    d2 = goFuture(d2, 1);
                }
                d1 = goPast(d2, 2);
            }


            if (timerange == "week")
            {
                d2 = goNextWeekDay(d2, DayOfWeek.Friday);
                d1 = goPrevWeekDay(d2, DayOfWeek.Monday); //Maybe Sunday???
            }

            if (timerange == "2 weeks")
            {

                if(displayDate1 == config.date1.Date)
                {
                    d1 = displayDate1.Date;

                    d1 = goPrevWeekDay(d1, DayOfWeek.Monday);
                    d2 = goNextWeekDay(d1, DayOfWeek.Friday);

                    d2 = goNextWeekDay(d2, DayOfWeek.Monday);
                    d2 = goNextWeekDay(d2, DayOfWeek.Friday); //second week forward
                }
                else
                {
                    d2 = goNextWeekDay(d2, DayOfWeek.Friday);
                    d1 = goPrevWeekDay(d2, DayOfWeek.Monday); 

                    d1 = goPrevWeekDay(d1, DayOfWeek.Friday);
                    d1 = goPrevWeekDay(d1, DayOfWeek.Monday); //second week back
                }

            }

            if (timerange == "month")
            {
                d2 = goMonthLastDay(d2);
                if (isWeekend(d2))
                {
                    d2 = goPrevWeekDay(d2, DayOfWeek.Friday);
                }

                d1 = goMonthFirstDay(d2);
                if (isWeekend(d1))
                {
                    d1 = goNextWeekDay(d1, DayOfWeek.Monday); //Maybe Sunday???
                }
            }


            if (timerange == "year")
            {
                d2 = goYearLastDay(d2);
                d2 = assureDayInRange(d2);

                if (isWeekend(d2))
                {
                    d2 = goPrevWeekDay(d2, DayOfWeek.Friday);
                }

                d1 = goYearFirstDay(d2);
                if (isWeekend(d1))
                {
                    d1 = goNextWeekDay(d1, DayOfWeek.Monday); 
                }
            }



            if (checkDayInRange(d1) && checkDayInRange(d2))
            {
                displayDate1 = d1;
                displayDate2 = d2;
            }
            else
            {
                if (DateTime.Compare(d2.Date, config.date2.Date) > 0)
                {
                    setPeriodBack();
                }

                if (DateTime.Compare(config.date1.Date, d2.Date) > 0)
                {
                    setPeriodForward();
                }
            }

        }


        private bool checkDayInRange(DateTime day)
        {
            return (DateTime.Compare(config.date1.Date, day.Date) <= 0 && DateTime.Compare(day.Date, config.date2.Date) <= 0);
        }


        private DateTime assureDayInRange(DateTime day)
        {
            if (DateTime.Compare(config.date1.Date, day.Date) <= 0 && DateTime.Compare(day.Date, config.date2.Date) <= 0)
            {
                return day;
            }
            else
            {
                if (DateTime.Compare(config.date1.Date, day.Date) > 0)
                {
                    return config.date1.Date;
                }
                else
                {
                    return config.date2.Date;
                }
            }
        }


        public void goFirstPeriod()
        {
            Application.DoEvents();
            getDisplayDates();
            setTimeDelta();

            displayDate1 = config.date1.Date;
            displayDate2 = displayDate1.Add(timeDelta);

            if (timerange == "week")
            {
                displayDate2 = displayDate1;
            }

            if (timerange == "year")
            {
                displayDate1 = assureDayInRange(displayDate1);
                displayDate2 = assureDayInRange(displayDate2);
            }

            setPeriod();

            setDisplayDates();
        }


        public void goLastPeriod()
        {
            Application.DoEvents();
            getDisplayDates();
            setTimeDelta();

            displayDate2 = config.date2.Date;

            displayDate1 = displayDate2.Add(-timeDelta);

            if (timerange == "year")
            {
                displayDate1 = assureDayInRange(displayDate1);
                displayDate2 = assureDayInRange(displayDate2);
            }

            setPeriod();

            setDisplayDates();
        }


        public void goPrevPeriod()
        {
            Application.DoEvents();
            getDisplayDates();
            setTimeDelta();

            DateTime d1 = displayDate2.Date;
            DateTime d2 = displayDate2.Date;


            if (timerange == "day")
            {
                d2 = goPast(d2, 1); //prev

                d1 = d2;
            }

            if (timerange == "2 days")
            {

                d2 = goPast(d2, 2); //prev

                d1 = goPast(d2, 1);
            }

            if (timerange == "3 days")
            {
                d2 = goPast(d2, 3); //prev

                d1 = goPast(d2, 2);
            }


            if (timerange == "week")
            {
                d2 = goPast(d2, 6); //prev

                d2 = goNextWeekDay(d2, DayOfWeek.Friday);
                d1 = goPrevWeekDay(d2, DayOfWeek.Monday); //Maybe Sunday???
            }


            if (timerange == "2 weeks")
            {
                d2 = goPast(d2, 12); //prev

                d2 = goNextWeekDay(d2, DayOfWeek.Friday);
                d1 = goPrevWeekDay(d2, DayOfWeek.Monday); //Maybe Sunday???
            }



            if (timerange == "month")
            {
                d2 = d2.AddMonths(-1); //prev

                d2 = goMonthLastDay(d2);
                if (isWeekend(d2))
                {
                    d2 = goPrevWeekDay(d2, DayOfWeek.Friday);
                }

                d1 = goMonthFirstDay(d2);
                if (isWeekend(d1))
                {
                    d1 = goNextWeekDay(d1, DayOfWeek.Monday); //Maybe Sunday???
                }
            }


            if (timerange == "year")
            {

                d2 = goYearFirstDay(d2);

                d2 = d2.AddMonths(-1); //prev

                d2 = goYearLastDay(d2);
                if (isWeekend(d2))
                {
                    d2 = goPrevWeekDay(d2, DayOfWeek.Friday);
                }

                d1 = goYearFirstDay(d2);
                if (isWeekend(d1))
                {
                    d1 = goNextWeekDay(d1, DayOfWeek.Monday); //Maybe Sunday???
                }
            }


            if (timerange != "year")
            {
                if (checkDayInRange(d1) && checkDayInRange(d2))
                {
                    displayDate1 = d1;
                    displayDate2 = d2;
                }
            }
            else
            {
                if (checkDayInRange(d1))
                {
                    displayDate1 = d1;
                }

                if (checkDayInRange(d2))
                {
                    displayDate2 = d2;
                }

                if (checkDayInRange(d1) || checkDayInRange(d2))
                {
                    if (!checkDayInRange(d1))
                    {
                        displayDate1 = config.date1.Date;
                    }

                    if (!checkDayInRange(d2))
                    {
                        displayDate2 = config.date2.Date;
                    }
                }
            }
            setDisplayDates();
        }


        public void goNextPeriod()
        {
            Application.DoEvents();
            getDisplayDates();
            setTimeDelta();

            DateTime d1 = displayDate2.Date;
            DateTime d2 = displayDate2.Date;


            if (timerange == "day")
            {
                d2 = goFuture(d2, 1); //next

                d1 = d2;
            }

            if (timerange == "2 days")
            {

                d2 = goFuture(d2, 2); //next

                d1 = goPast(d2, 1);
            }

            if (timerange == "3 days")
            {
                d2 = goFuture(d2, 3); //next

                d1 = goPast(d2, 2);
            }


            if (timerange == "week")
            {
                d2 = goFuture(d2, 6); //next

                d2 = goPrevWeekDay(d2, DayOfWeek.Friday);
                d1 = goPrevWeekDay(d2, DayOfWeek.Monday); //Maybe Sunday???
            }


            if (timerange == "2 weeks")
            {
                d2 = goFuture(d2, 12); //next

                d2 = goPrevWeekDay(d2, DayOfWeek.Friday);
                d1 = goPrevWeekDay(d2, DayOfWeek.Monday); //Maybe Sunday???
            }


            if (timerange == "month")
            {
                d2 = d2.AddMonths(1); //next

                d2 = goMonthLastDay(d2);
                if (isWeekend(d2))
                {
                    d2 = goPrevWeekDay(d2, DayOfWeek.Friday);
                }

                d1 = goMonthFirstDay(d2);
                if (isWeekend(d1))
                {
                    d1 = goNextWeekDay(d1, DayOfWeek.Monday); //Maybe Sunday???
                }
            }


            if (timerange == "year")
            {
                d2 = goYearLastDay(d2);

                d2 = d2.AddMonths(1); //next

                d2 = goYearLastDay(d2);
                if (isWeekend(d2))
                {
                    d2 = goPrevWeekDay(d2, DayOfWeek.Friday);
                }

                d1 = goYearFirstDay(d2);
                if (isWeekend(d1))
                {
                    d1 = goNextWeekDay(d1, DayOfWeek.Monday); //Maybe Sunday???
                }
            }


            if (timerange != "year")
            {
                if (checkDayInRange(d1) && checkDayInRange(d2))
                {
                    displayDate1 = d1;
                    displayDate2 = d2;
                }
            }
            else
            {
                if (checkDayInRange(d1))
                {
                    displayDate1 = d1;
                }

                if (checkDayInRange(d2))
                {
                    displayDate2 = d2;
                }

                if (checkDayInRange(d1) || checkDayInRange(d2))
                {
                    if (!checkDayInRange(d1))
                    {
                        displayDate1 = config.date1.Date;
                    }

                    if (!checkDayInRange(d2))
                    {
                        displayDate2 = config.date2.Date;
                    }
                }
            }
            setDisplayDates();
        }


        public void goRandomPeriod()
        {
            Application.DoEvents();
            getDisplayDates();
            setTimeDelta();

            DateTime d1 = config.date1.Date;
            DateTime d2 = config.date2.Date;

            Random rnd = new Random();
            int range = (d2 - d1).Days;

            d2 = d1.AddDays(rnd.Next(range));

            if (checkDayInRange(d2))
            {
                displayDate1 = d1;
                displayDate2 = d2;

                setDisplayDates();
            }

        }


        public void goRandomInstr()
        {
            Application.DoEvents();

            Random rnd = new Random();

            int n = config.instruments.Count;

            int i = rnd.Next(n);

            config.chosen_instrument = config.instruments[i];

            f1.set_instrument(config.chosen_instrument);
        }

    }

}
