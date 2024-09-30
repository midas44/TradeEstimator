using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeEstimator.Test
{
    public class InputSet
    {

        public List<InputSubSet> inputList;

        public InputSet() 
        {

            inputList = new();

        }  
        

        public void addInputSubSet(string instr, string dir, int size, string type, double triggerPrice, double tpPrice, double slPrice, double bePrice)
        {
            InputSubSet inputSubSet = new(instr, dir, size, type, triggerPrice, tpPrice, slPrice, bePrice);

            //InputSubSet(string instr, string dir, int size, string type, double triggerPrice, double tpPrice, double slPrice, double bePrice)

            inputList.Add(inputSubSet);
        }


    }






}
