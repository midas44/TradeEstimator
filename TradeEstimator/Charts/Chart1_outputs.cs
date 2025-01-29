using ScottPlot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeEstimator.Conf;
using TradeEstimator.Data;
using TradeEstimator.Trade;

namespace TradeEstimator.Charts
{
    public partial class Chart1
    {

        public void outputOrders(List <Order> orders, DaysQuotes days_quotes)
        {
            foreach (var order in orders)
            {
                outputOrder(order, days_quotes);
            }

        }


        private void outputOrder(Order order, DaysQuotes days_quotes)
        {
            logger.log_("outputOrder", 2);

            logger.log("-------- ", 2);

            logger.log("dir = " + order.dir, 2);

            logger.log("type = " + order.type, 2);

            logger.log("size = " + order.size.ToString(), 2);

            logger.log("triggerPrice = " + order.triggerPrice.ToString(), 2);

            logger.log("-------- ", 2);

            logger.log("entryPrice = " + order.entryPrice.ToString(), 2);


            logger.log("-------- ", 2);

            logger.log("startTime = " + order.startTime.ToString(), 2);

            logger.log("endTime = " + order.endTime.ToString(), 2);
            logger.log("-------- ", 2);

            logger.log("isActive = " + order.isActive.ToString(), 2);

            logger.log("isTriggered = " + order.isTriggered.ToString(), 2);


            int orderWidth = 1;

            ScottPlot.Color orderColor;
            ScottPlot.Color orderColor2;

            if (order.size > 0)
            {
                orderColor = ScottPlot.Colors.LimeGreen;
                
            }
            else
            {
                if (order.size < 0)
                {
                    orderColor = ScottPlot.Colors.Tomato;
                }
                else
                {
                    orderColor = ScottPlot.Colors.Black;
                }
            }

            orderColor2 = orderColor.WithAlpha(0.5);

            double[] timeline_ = new double[2];
            timeline_[0] = find_chart_index(order.startTime, days_quotes); // check missed bars  //bug here day_quotes  = null
            timeline_[1] = find_chart_index(order.endTime, days_quotes);

            double[] priceline_ = new double[2];
            priceline_[0] = order.triggerPrice;
            priceline_[1] = order.triggerPrice;

            logger.log_("display_line", 2);
            logger.log("timeline: " + timeline_[0].ToString() + " - " + timeline_[1].ToString(), 2); //bug here
            logger.log("priceline: " + priceline_[0].ToString() + " - " + priceline_[1].ToString(), 2);

            display_line(timeline_, priceline_, orderColor, orderWidth);

            double[] priceline__ = new double[2];
            priceline__[0] = order.entryPrice;
            priceline__[1] = order.entryPrice;

            display_line(timeline_, priceline__, orderColor2, orderWidth);

        }


        private int findChartIndex(DateTime timepoint, DaysQuotes days_quotes)
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


        private void createMarker(int shapeId, ScottPlot.Color color, DateTime time, float price, string msg)
        {
            MarkerShape[] markerShapes = Enum.GetValues<MarkerShape>().ToArray();

            var mp = plot1.Plot.Add.Marker(x: shapeId, y: 0);
            mp.MarkerStyle.Shape = markerShapes[shapeId];
            mp.MarkerStyle.Size = 10;

            // markers made from filled shapes have can be customized
            mp.MarkerStyle.FillColor = color.WithAlpha(.5);

            // markers made from filled shapes have optional outlines
            mp.MarkerStyle.OutlineColor = color;
            mp.MarkerStyle.OutlineWidth = 2;

            // markers created from lines can be customized
            mp.MarkerStyle.LineWidth = 2f;
            mp.MarkerStyle.LineColor = color;

            var txt = plot1.Plot.Add.Text(markerShapes[shapeId].ToString(), shapeId, 0.15);
            txt.LabelRotation = 0;
            txt.LabelAlignment = Alignment.MiddleLeft;
            txt.LabelFontColor = Colors.Black;
        }

    }
}
