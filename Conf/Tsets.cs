using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.IO;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;

namespace TradeEstimator.Conf
{
    public class Tsets
    {

        public string tset_name;

        //Astro Num Points

        public List<string> astroNumPoints;

        public int lastIndex;

        IniFile INI;


        public Tsets(Config config, string tset_name)
        {
            this.tset_name = tset_name;
            string path = config.tsets_path + "/" + tset_name + ".ini";

            INI = new IniFile(path);

            //Astro Num Points

            astroNumPoints = new List<string>();

            lastIndex = -1;


            for (int i = 0; i <= 1000; i++)
            {
                string s = INI.Read("anpoint_" + i.ToString(), "Points").Trim();
                if (s.Length > 0)
                {
                    astroNumPoints.Add(s);
                    lastIndex = i;
                }
                
            }            
        }

        public void addRecord(string s)
        {
            lastIndex++;

            string key = "anpoint_" + lastIndex.ToString();

            INI.Write(key, s, "Points");
        }


        public void saveRecord(int index, string s)
        {
            string key = "anpoint_" + index.ToString();
            INI.Write(key, s, "Points");
        }

        public void deleteRecord(int index) //not in use
        {
            string key = "anpoint_" + index.ToString();
            INI.DeleteKey(key, "Points");
        }


        public void clear()
        {
            INI.DeleteSection("Points");
        lastIndex = -1; 
        }


        public void saveAll(List<string> items)
        {
            clear();

            foreach (string item in items)
            {
                addRecord(item);
            }
        }

        

    }
}
