using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeEstimator.Conf;
using TradeEstimator.Data;

namespace TradeEstimator.Charts
{
    public partial class Chart1
    {

        public void displayInstrDataOHLC(InstrConfig instr_config, DaysQuotes days_quotes)
        {


            timeline = days_quotes.Timeline;

            ADR = days_quotes.ADR;
            Open = days_quotes.Open;
            High = days_quotes.High;
            Low = days_quotes.Low;
            Close = days_quotes.Close;
            Volume = days_quotes.Volume;

            instr_tick = instr_config.instr_tick;
            instr_price_format = instr_config.export_price_format;



            n = days_quotes.Timeline.Length;


            price_format = instr_config.export_price_format;

            n = days_quotes.Timeline.Length;

            x_min = 0;
            x_max = n;

            for (int i = 0; i < n; i++)
            {
                double high = days_quotes.High[i];
                double low = days_quotes.Low[i];

                if (high > y_max)
                {
                    y_max = high;
                }

                if (low < y_min)
                {
                    y_min = low;
                }
            }

            double y_delta = y_max - y_min;
            y_max = y_max + displayed_y_range * y_delta;
            y_min = y_min - displayed_y_range * y_delta;

            double x_delta = x_max - x_min;
            x_min = x_min - displayed_x_range * x_delta;
            x_max = x_max + displayed_x_range * x_delta;

            for (int i = 0; i < n; i++)
            {
                double[] timeline_ = new double[2];
                double[] price_ = new double[2];
                timeline_[0] = i;
                timeline_[1] = i;
                price_[0] = days_quotes.Low[i];
                price_[1] = days_quotes.High[i];
                display_line(timeline_, price_, ohlc_color, ohlc_width);

                double[] timeline1_ = new double[2];
                double[] price1_ = new double[2];
                timeline1_[0] = i - 0.5;
                timeline1_[1] = i;
                price1_[0] = days_quotes.Open[i];
                price1_[1] = days_quotes.Open[i];
                display_line(timeline1_, price1_, ohlc_color, ohlc_width);

                double[] timeline2_ = new double[2];
                double[] price2_ = new double[2];
                timeline2_[0] = i;
                timeline2_[1] = i + 0.5;
                price2_[0] = days_quotes.Close[i];
                price2_[1] = days_quotes.Close[i];
                display_line(timeline2_, price2_, ohlc_color, ohlc_width);
            }
        }


        public void displayInstrDataHL(InstrConfig instr_config, DaysQuotes days_quotes)
        {


            timeline = days_quotes.Timeline;

            ADR = days_quotes.ADR;
            Open = days_quotes.Open;
            High = days_quotes.High;
            Low = days_quotes.Low;
            Close = days_quotes.Close;
            Volume = days_quotes.Volume;

            instr_tick = instr_config.instr_tick;
            instr_price_format = instr_config.export_price_format;



            n = days_quotes.Timeline.Length;


            price_format = instr_config.export_price_format;

            n = days_quotes.Timeline.Length;

            x_min = 0;
            x_max = n;

            for (int i = 0; i < n; i++)
            {
                double high = days_quotes.High[i];
                double low = days_quotes.Low[i];

                if (high > y_max)
                {
                    y_max = high;
                }

                if (low < y_min)
                {
                    y_min = low;
                }
            }

            double y_delta = y_max - y_min;
            y_max = y_max + displayed_y_range * y_delta;
            y_min = y_min - displayed_y_range * y_delta;

            double x_delta = x_max - x_min;
            x_min = x_min - displayed_x_range * x_delta;
            x_max = x_max + displayed_x_range * x_delta;

            for (int i = 0; i < n; i++)
            {
                double[] timeline_ = new double[2];
                double[] price_ = new double[2];
                timeline_[0] = i;
                timeline_[1] = i;
                price_[0] = days_quotes.Low[i];
                price_[1] = days_quotes.High[i];
                display_line(timeline_, price_, ohlc_color, ohlc_width);
            }
        }


        public void display_grids(DaysQuotes days_quotes, Gridset[] grids, DateTime date1, DateTime date2)
        {
            bool key = true;
            int i = 0;

            while (key && i < grids.Length)
            {
                Gridset gridset = grids[i];

                DateTime daybreak_grid_date = gridset.timeline_daybreak[0].Date;
                if (DateTime.Compare(daybreak_grid_date, date1) >= 0 && DateTime.Compare(daybreak_grid_date, date2) <= 0)
                {
                    double[] timeline_ = new double[2];
                    timeline_[0] = find_chart_index(gridset.timeline_daybreak[0], days_quotes);
                    timeline_[1] = find_chart_index(gridset.timeline_daybreak[1], days_quotes);

                    if (daybreak_grid_date.DayOfWeek == DayOfWeek.Monday)
                    {
                        display_line(timeline_, gridset.price_daybreak, weekbreak_color, daybreak_width);
                    }
                    else
                    {
                        display_line(timeline_, gridset.price_daybreak, daybreak_color, daybreak_width);
                    }
                }

                i++;
            }
        }


        public void display_grids_light(DaysQuotes days_quotes, Gridset[] grids, DateTime date1, DateTime date2)
        {
            bool key = true;
            int i = 0;

            while (key && i < grids.Length)
            {
                Gridset gridset = grids[i];

                DateTime daybreak_grid_date = gridset.timeline_daybreak[0].Date;
                if (DateTime.Compare(daybreak_grid_date, date1) >= 0 && DateTime.Compare(daybreak_grid_date, date2) <= 0)
                {
                    double[] timeline_ = new double[2];
                    timeline_[0] = find_chart_index(gridset.timeline_daybreak[0], days_quotes);
                    timeline_[1] = find_chart_index(gridset.timeline_daybreak[1], days_quotes);

                    if (daybreak_grid_date.DayOfWeek == DayOfWeek.Monday)
                    {
                        display_line(timeline_, gridset.price_daybreak, daybreak_color, daybreak_width);
                    }
                }

                i++;
            }
        }


        private void display_line(double[] timeline_, double[] price_, ScottPlot.Color line_color, int line_width)
        {
            var line_plot = plot1.Plot.Add.Scatter(timeline_, price_);

            line_plot.LineStyle.Color = line_color;

            line_plot.MarkerStyle = ScottPlot.MarkerStyle.None;

            line_plot.LineStyle.Width = line_width;
        }

    }
}
