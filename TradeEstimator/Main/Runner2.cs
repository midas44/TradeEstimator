using TradeEstimator.Charts;
using TradeEstimator.Conf;
using OpenTK.Audio.OpenAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeEstimator.Main
{
    public partial class Runner
    {

        //Runner2


        Chart2 chart2;


        public void createChart2()
        {
            chart2 = new(config, logger);

            f1.set_chart2(chart2);
        }


        public void outputData2()
        {
            chart2.displayInstrDataOHLC(instrConfig, daysQuotes);

            chart2.display_grids(daysQuotes, instrGrids.gridSets, displayDate1, displayDate2);

            //chart2.finalize();
        }


        public void outputTrades2()
        {
            getAnIterUiState();

        }


    }
}
