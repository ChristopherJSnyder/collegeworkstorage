using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Assign4
{
    public class Adventure
    {
        private HashMap<StringKey, Item> map { get; set; }
        public string[] test;

        public Adventure(string fileName)
        {
            string line;

            if (fileName == null)
            {
                throw new ArgumentNullException();
            }

            if (!File.Exists(fileName))
            {
                throw new ArgumentException();
            }

            StreamReader sr = new StreamReader(fileName);


            map = new HashMap<StringKey, Item>();

            while ((line = sr.ReadLine()) != null)
            {

                string[] words = line.Split(',');
                Item newitem = new Item(words[0].Trim(), Int32.Parse(words[1].Trim()), Double.Parse(words[2].Trim()));
                StringKey newkey = new StringKey(words[0].Trim());
                map.Put(newkey, newitem);

            }
        }


        public HashMap<StringKey, Item> GetMap()
        {
            return this.map;
        }


        public string PrintLootMap()
        {
            string printedloot = "";
            List<string> sortedloot = new List<string>();
            IEnumerator<Item> items = map.Values();

            while (items.MoveNext()) { 
                Item item = items.Current;
                if (item.GoldPieces > 0)
                {
                    sortedloot.Add(item.ToString());
                }
            }


            sortedloot.Sort();

            foreach(String item in sortedloot)
            {
                printedloot += item + "\n";
            }


            return printedloot;
        }

    }
}