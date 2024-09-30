using TradeEstimator.Conf;
using TradeEstimator.Data;
using TradeEstimator.Trade;
using OpenTK.Audio.OpenAL;
using ScottPlot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeEstimator.Charts
{
    public partial class Chart2
    {
        ScottPlot.Color indicator_color;

        ScottPlot.Color ex_color;

        ScottPlot.Color mp_color;

        ScottPlot.Color qt_color ;

        ScottPlot.Color cos_color;

        ScottPlot.Color ground_color;

        ScottPlot.Color ari0_color;



        //NEW



        public void display_profile_line(double[] price, double[] timeline, ScottPlot.Color profile_color, int width) //TODO: remove
        {
            display_line(timeline, price, profile_color, width);
        }



        public void display_profile_line_with_style(double[] price, double[] timeline, ScottPlot.Color profile_color, int width, ScottPlot.LinePattern line_style) //TODO: remove
        {
            display_line_with_style(timeline, price, profile_color, width, line_style);
        }



        //OLD



        private void display_line(double[] timeline_, double[] price_, ScottPlot.Color line_color, int line_width)
        {
            var line_plot = plot2.Plot.Add.Scatter(price_, timeline_);

            line_plot.Color = line_color;

            line_plot.MarkerStyle = ScottPlot.MarkerStyle.None;

            line_plot.LineStyle.Width = line_width;
        }




        private void display_line_with_style(double[] timeline_, double[] price_, ScottPlot.Color line_color, int line_width, ScottPlot.LinePattern line_style)
        {
            var line_plot = plot2.Plot.Add.Scatter(price_, timeline_);

            line_plot.Color = line_color;

            line_plot.MarkerStyle = ScottPlot.MarkerStyle.None;

            line_plot.LineStyle.Width = line_width;

            line_plot.LinePattern = line_style;
        }



        public void displayInstrDataOHLC(InstrConfig instrConfig, DaysQuotes daysQuotes)
        {

            timeline = daysQuotes.Timeline;

            ADR = daysQuotes.ADR;

            instrTick = instrConfig.instr_tick;

            instrPriceFormat = instrConfig.export_price_format;

            n = daysQuotes.Timeline.Length;

            y_min = 0;
            y_max = n;

            for (int i = 0; i < n; i++)
            {
                double high = daysQuotes.High[i];
                double low = daysQuotes.Low[i];

                if (high > x_max)
                {
                    x_max = high;
                }

                if (low < x_min)
                {
                    x_min = low;
                }
            }

            //sc_x1[9] = x_min;
            //sc_x2[9] = x_max;
/*
            double x_delta = x_max - x_min;
            x_max = x_max + displayed_x_range * x_delta;
            x_min = x_min - displayed_x_range * x_delta;
*/
            x0_max = x_max;
            x0_min = x_min;

            double y_delta = y_max - y_min;
            y_min = y_min - displayed_y_range * y_delta;
            y_max = y_max + displayed_y_range * y_delta;



            string[] labels = new string[n + 1];

            for (int i = 0; i < n; i++)
            {
                double[] timeline_ = new double[2];
                double[] price_ = new double[2];
                timeline_[0] = n - i;
                timeline_[1] = n - i;
                price_[0] = daysQuotes.Low[i];
                price_[1] = daysQuotes.High[i];
                display_line(timeline_, price_, ohlc_color, ohlc_width);

                double[] timeline1_ = new double[2];
                double[] price1_ = new double[2];
                timeline1_[0] = n - i + 0.5;
                timeline1_[1] = n - i;
                price1_[0] = daysQuotes.Open[i];
                price1_[1] = daysQuotes.Open[i];
                display_line(timeline1_, price1_, ohlc_color, ohlc_width);

                double[] timeline2_ = new double[2];
                double[] price2_ = new double[2];
                timeline2_[0] = n - i;
                timeline2_[1] = n - i - 0.5;
                price2_[0] = daysQuotes.Close[i];
                price2_[1] = daysQuotes.Close[i];
                display_line(timeline2_, price2_, ohlc_color, ohlc_width);
            }

            plot2.Plot.Layout.Frameless();
        }



        public void display_grids(DaysQuotes days_quotes, Gridset[] Grids, DateTime date1, DateTime date2)
        {
            bool key = true;
            int i = 0;

            while (key && i < Grids.Length)
            {
                Gridset gridset = Grids[i];

                /*
                DateTime grid1_date = gridset.timeline_trade_start[0].Date;
                if (DateTime.Compare(grid1_date, date1) >= 0 && DateTime.Compare(grid1_date, date2) <= 0)
                {
                    double[] timeline_ = new double[2];
                    timeline_[0] = n - find_chart_index(gridset.timeline_trade_start[0], days_quotes);
                    timeline_[1] = n - find_chart_index(gridset.timeline_trade_start[1], days_quotes);
                    display_line(timeline_, gridset.price_trade_start, targets_grid1_color, targets_grid_width);
                }
                */

                DateTime daybreak_grid_date = gridset.timeline_daybreak[0].Date;
                if (DateTime.Compare(daybreak_grid_date, date1) >= 0 && DateTime.Compare(daybreak_grid_date, date2) <= 0)
                {
                    double[] timeline_ = new double[2];

                    timeline_[0] = n - find_chart_index(gridset.timeline_daybreak[0], days_quotes);
                    timeline_[1] = n - find_chart_index(gridset.timeline_daybreak[1], days_quotes);

                    if (daybreak_grid_date.DayOfWeek == DayOfWeek.Monday)
                    {
                        display_line(timeline_, gridset.price_daybreak, weekbreak_color, daybreak_width);
                    }
                    else
                    {
                        display_line(timeline_, gridset.price_daybreak, daybreak_color, daybreak_width);
                    }
                }

                /*
                DateTime grid2_date = gridset.timeline_trade_end[0].Date;
                if (DateTime.Compare(grid2_date, date1) >= 0 && DateTime.Compare(grid2_date, date2) <= 0)
                {
                    double[] timeline_ = new double[2];
                    timeline_[0] = n - find_chart_index(gridset.timeline_trade_end[0], days_quotes);
                    timeline_[1] = n - find_chart_index(gridset.timeline_trade_end[1], days_quotes);
                    display_line(timeline_, gridset.price_trade_end, targets_grid2_color, targets_grid_width);
                }
                else
                {
                    if (DateTime.Compare(grid2_date, date2) > 0)
                    {
                        key = false;
                    }
                }
                */

                i++;

            }
        }




    }
}
