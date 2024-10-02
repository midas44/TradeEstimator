using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using ScottPlot.WinForms;
using ScottPlot;
using ScottPlot.Plottables;

using ScottPlot.TickGenerators;
using TradeEstimator.Conf;
using TradeEstimator.Data;
using TradeEstimator.Log;
using TradeEstimator.Trade;
using TradeEstimator;
using ScottPlot.Control;

namespace TradeEstimator.Charts
{
    public partial class Chart2
    {
        Form1 f1;

        FormsPlot plot2;

        Config config;
        InstrConfig instr_config;
        TradeModel tr_model;
        Logger logger;

        ScottPlot.Color chart_background_color;
        ScottPlot.Color chart_axes_color;
        ScottPlot.Color chart_grids_color;

        ScottPlot.Color hilo_color;
        ScottPlot.Color ohlc_color;

        ScottPlot.Color daybreak_color;
        ScottPlot.Color weekbreak_color;

        ScottPlot.Color targets_grid1_color;
        ScottPlot.Color targets_grid2_color;
        ScottPlot.Color targets_rev_buy_color;
        ScottPlot.Color targets_rev_sell_color;

        ScottPlot.Color v_track_color;
        ScottPlot.Color h_track_color;

        ScottPlot.Color mark_color;

        ScottPlot.Color invisible_color;

        int ohlc_width;
        int hilo_width;

        int mark_width;

        int daybreak_width;

        int targets_grid_width;
        int entry_level_width;
        int exit_level_width;

        double x_min;
        double x_max;

        double y_min;
        double y_max;

        public DaysQuotes days_quotes;

        public string price_format;

        int n;

        double displayed_x_range;
        double displayed_y_range;

        public DateTime[] timeline;

        public double[] ADR;
        public double[] Open;
        public double[] Close;
        public double[] High;
        public double[] Low;
        public double[] Volume;

        public double instr_tick;

        public string instr_price_format;

        double markX;
        double markY;

        string markLabel;


        public Chart2(Config config, Logger logger)
        {
            this.config = config;

            this.logger = logger;

            f1 = Application.OpenForms["Form1"] as Form1;

            plot2 = Application.OpenForms["Form1"].Controls.Find("plot2", true)[0] as FormsPlot;

            chart_background_color = f1.backgroundColor1Hex;
            chart_axes_color = f1.foregroundColor1Hex;
            chart_grids_color = f1.backgroundColor2Hex;

            hilo_color = f1.foregroundColor4Hex;
            ohlc_color = f1.foregroundColor4Hex;

            daybreak_color = ScottPlot.Color.FromHex("#C7C9E4").WithOpacity(0.25);
            weekbreak_color = ScottPlot.Color.FromHex("#C7C9E4").WithOpacity(0.5);

            targets_grid1_color = ScottPlot.Color.FromHex("#98D6E0").WithOpacity(0.25);
            targets_grid2_color = ScottPlot.Color.FromHex("#F892F8").WithOpacity(0.25);
            targets_rev_buy_color = ScottPlot.Colors.Blue;
            targets_rev_sell_color = ScottPlot.Colors.Red;

            h_track_color = ScottPlot.Colors.Silver.WithOpacity(0.3);
            v_track_color = ScottPlot.Colors.Silver.WithOpacity(0.3);

            mark_color = f1.highlightColor2Hex;

            invisible_color = ScottPlot.Colors.Magenta.WithOpacity(0);

            ohlc_width = 1;
            hilo_width = 1;

            mark_width = 1;

            daybreak_width = 1;

            targets_grid_width = 1;

            entry_level_width = 3;
            exit_level_width = 1;

            plot2.Plot.FigureBackground.Color = chart_background_color;
            plot2.Plot.DataBackground.Color = chart_background_color;
            plot2.Plot.Axes.Color(chart_axes_color);

            plot2.Plot.Layout.Frameless();

            plot2.Plot.ScaleFactor = 1;

            //plot2.Plot.Benchmark.IsVisible = false;

            var interaction = plot2.Interaction as Interaction;
            if (interaction is not null)
            {
                interaction.Actions.ToggleBenchmark = delegate { };
            }


            plot2.Visible = true;


            displayed_x_range = 0.01;
            displayed_y_range = 0.1;

            timeline = new DateTime[2];
            timeline[0] = config.date1;
            timeline[1] = config.date2;

            setup();
        }



        private void reset_chart_limits()
        {
            x_min = 999999;
            x_max = -999999;

            y_min = 999999;
            y_max = -999999;
        }


        public void set_chart_limits()
        {
            plot2.Plot.Axes.SetLimits(x_min, x_max, y_min, y_max);
        }


        public void set_chart_limits_y()
        {
            plot2.Plot.Axes.SetLimitsY(y_min, y_max);
        }


        public void set_chart_limits_x()
        {
            plot2.Plot.Axes.SetLimitsX(x_min, x_max);
        }


        private void setup()
        {
            f1.show_panelB();
            reset();
        }


        public void reset()
        {
            plot2.Plot.Clear();
            reset_chart_limits();
        }


        private int find_chart_index(DateTime timepoint, DaysQuotes days_quotes)
        {
            for (int i = 0; i < n; i++)
            {
                if (days_quotes.Timeline[i] == timepoint)
                {
                    return i;
                }
            }
            return -1;
        }


        public void mark()
        {
            double y_delta = y_max - y_min;
            double[] v_timeline_ = { markX, markX };
            double[] v_price_ = { y_min - displayed_y_range * y_delta, y_max + displayed_y_range * y_delta };

            var mark_vline = plot2.Plot.Add.Scatter(v_timeline_, v_price_);

            mark_vline.Color = mark_color;
            mark_vline.MarkerStyle = ScottPlot.MarkerStyle.None;
            mark_vline.LineStyle.Width = mark_width;

            /*
            double x_delta = x_max - x_min;
            double[] h_timeline_ = { x_min - 2 * x_delta, x_max + 2 * x_delta };
            double[] h_price_ = { markY, markY };


            var mark_hline = plot2.Plot.Add.Scatter(h_timeline_, h_price_);

            mark_hline.Color = mark_color;
            mark_hline.MarkerStyle = ScottPlot.MarkerStyle.None;
            mark_hline.LineStyle.Width = mark_width;
            */
        }


        public void setMark(double x, double y)
        {
            markX = x;
            markY = y;
        }



        public void finalize()
        {
            createAnnotation();
            f1.Refresh();
        }


        private void createAnnotation()
        {
            //string s = f1.getactiveIndicatorName();
            string s = "Annotation";
            var anno = plot2.Plot.Add.Annotation(s);
            anno.LabelFontSize = 24;
            anno.LabelFontName = Fonts.Sans;
            anno.LabelBackgroundColor = Colors.Transparent;
            anno.LabelFontColor = Colors.LimeGreen;
            anno.LabelBorderColor = Colors.Transparent;
            anno.LabelBorderWidth = 0;
            anno.LabelShadowColor = Colors.Transparent;
            anno.OffsetY = plot2.Height - 40;
            anno.OffsetX = (int)Math.Abs(plot2.Width * 0.75);
        }


    }
}