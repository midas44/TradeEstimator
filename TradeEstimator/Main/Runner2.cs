﻿using TradeEstimator.Charts;
using TradeEstimator.Conf;
using OpenTK.Audio.OpenAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeEstimator.Trade;
using TradeEstimator.Data;

namespace TradeEstimator.Main
{
    public partial class Runner
    {

        //Runner2


        Chart1 chart1;


        public void createChart1()
        {
            chart1 = new(config, logger);

            f1.set_chart1(chart1);
        }


        public void outputData1()
        {

            if (timerange == "year")
            {
                chart1.displayInstrDataHL(instrConfig, daysQuotes);

                chart1.display_grids_light(daysQuotes, instrGrids.gridSets, displayDate1, displayDate2);
            }
            else
            {
                chart1.displayInstrDataOHLC(instrConfig, daysQuotes);

                chart1.display_grids(daysQuotes, instrGrids.gridSets, displayDate1, displayDate2);
            }

            chart1.finalize(); //TO DO: disable here, put after trade markers and equity outpuе?
        }


        public void outputTrades(Trader trader) //not in use
        {
            int instrI = trader.instruments.IndexOf(instrName);
            List<Order> instrOrders = trader.trProcesses[instrI].inactiveOrders;
            chart1.outputOrders(instrOrders, daysQuotes);
        }


    }
}
