using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeEstimator;
using TradeEstimator.Data;
using System.Globalization;
using ScottPlot.WinForms;
using TradeEstimator.Conf;
using ScottPlot;
using ScottPlot.Plottables;
using TradeEstimator.Trade;
using System.Diagnostics.Metrics;
using TradeEstimator.Log;
using Color = ScottPlot.Color;

namespace TradeEstimator.Charts
{
    public partial class Chart2
    {
        Form1 f1;
        FormsPlot plot2;

        Config config;
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

        //ScottPlot.Color invisible_color;

        int ohlc_width;
        int hilo_width;

        int mark_width;

        int daybreak_width;

        int targets_grid_width;
        int entry_level_width;
        int exit_level_width;

        double x_min;
        double x_max;

        double x0_min;
        double x0_max;

        double[] scale_x1;
        double[] scale_x2;

        double y_min;
        double y_max;

        int n;

        double displayed_y_range;

        public int absGround;

        public int realGround;


        public DateTime[] timeline;

        public double[] ADR;

        public double instrTick;

        public string instrPriceFormat;

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

            daybreak_color = ScottPlot.Color.FromHex("#C7C9E4").WithOpacity(0.5);
            weekbreak_color = ScottPlot.Color.FromHex("#C7C9E4").WithOpacity(0.5);

            targets_grid1_color = ScottPlot.Color.FromHex("#98D6E0").WithOpacity(0.25); //#98D6E0
            targets_grid2_color = ScottPlot.Color.FromHex("#F892F8").WithOpacity(0.25); //#F892F8
            targets_rev_buy_color = ScottPlot.Colors.Blue.WithOpacity(0.5);
            targets_rev_sell_color = ScottPlot.Colors.Red.WithOpacity(0.5);

            h_track_color = ScottPlot.Colors.Silver.WithOpacity(0.3);
            v_track_color = ScottPlot.Colors.Silver.WithOpacity(0.3);

            //mark_color = ScottPlot.Color.FromHex("#00C8C8");

            //mark_color = ScottPlot.Colors.GreenYellow;

            mark_color = f1.highlightColor2Hex;

            //invisible_color = ScottPlot.Colors.Magenta.WithOpacity(0);


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

            plot2.Visible = true;


            plot2.Plot.ScaleFactor = 1;

            displayed_y_range = 0.01;

            scale_x1 = new double[9]; //0..8 - level grounds, 9 - real price high/low //???
            scale_x2 = new double[9];

            plot2.Plot.Benchmark.IsVisible = false;

            timeline = new DateTime[2];
            timeline[0] = config.date1;
            timeline[1] = config.date2;

            setup();
        }

        //-----------------------------------------------------------------------------------------------------------------------

        public void clear()
        {
            plot2.Plot.Clear();
            //f1.Refresh();
            Application.DoEvents();
        }


        private void set_chart_title()
        {
            //plot2.Plot.Title(instr_config.instr_name + " " + config.data_timeframe);
            //plot2.Plot.Title = "";
        }


        private void reset_chart_limits()
        {
            x_min = 9999999;
            x_max = -9999999;
        }


        public void set_chart_limits_x(double chart2XRange)
        {
            double x_delta = x0_max - x0_min;

            x_max = x0_max + chart2XRange * x_delta;
            x_min = x0_min - chart2XRange * x_delta;
        }


        public void set_chart_limits_x_scale(int scale_number)
        {
            if (scale_number == -1)
            {
                plot2.Plot.Axes.SetLimits(x_min, x_max, y_min, y_max);
            }
            else
            {
                plot2.Plot.Axes.SetLimits(scale_x1[scale_number], scale_x2[scale_number], y_min, y_max);
            }
        }


        public void set_chart_y_scale()
        {
            plot2.Plot.Axes.SetLimitsY(y_min, y_max);
        }


        private void setup()
        {
            f1.show_panelB();

            reset();

            //f1.Refresh();

            Application.DoEvents();
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



        public void finalize()
        {
            //set_chart_limits_x_scale(Form1.form1.chart_limit_x_scale); 

            //plot2.Plot.Axes.AutoScaleY();


            //plot2.Plot.Title(f1.getactiveIndicatorName());

            createAnnotation();

            f1.Refresh();
        }


        private void createAnnotation()
        {
            string s = "Annotation here";
            var anno = plot2.Plot.Add.Annotation(s);
            anno.LabelFontSize = 28;
            anno.LabelFontName = Fonts.Sans;
            anno.LabelBackgroundColor = Colors.Transparent;
            anno.LabelFontColor = Colors.LimeGreen;
            anno.LabelBorderColor = Colors.Transparent;
            anno.LabelBorderWidth = 0;
            anno.LabelShadowColor = Colors.Transparent;
            anno.OffsetY = plot2.Height - 40;
            anno.OffsetX = (int)Math.Abs(plot2.Width * 0.75);
        }



        //-----------------------------------------------------------------------------------------------------------------------

        public void set_chart_y_autoscale()
        {

            plot2.Plot.Axes.AutoScaleExpandY();

        }


        public void set_chart_x_autoscale()
        {

            plot2.Plot.Axes.AutoScaleExpandX();

        }


        public void set_chart_y_limits()
        {

            plot2.Plot.Axes.Left.Min = y_min;
            plot2.Plot.Axes.Left.Max = y_max;
        }



        public void setMark(double x, double y)
        {
            markX = x;
            markY = y;
        }


        public void mark()
        {
            /*
            foreach (ScottPlot.Plottables.Scatter line in plot2.Plot.PlottableList)
            {
                if (line.Color == mark_color)
                {
                    //line.Color = chart_background_color;
                    //line.IsVisible = false;
                    line.Color = invisible_color;

                }
            }
            */

            double x_delta = x_max - x_min;
            double[] h_timeline_ = { x_min - 2 * x_delta, x_max + 2 * x_delta };
            double[] h_price_ = { markY, markY };

            var mark_hline = plot2.Plot.Add.Scatter(h_timeline_, h_price_);

            mark_hline.Color = mark_color;
            mark_hline.MarkerStyle = ScottPlot.MarkerStyle.None;
            mark_hline.LineStyle.Width = mark_width;

 
            //f1.Refresh();
        }




    }
}