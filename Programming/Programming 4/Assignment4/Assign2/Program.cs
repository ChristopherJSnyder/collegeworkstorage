using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assign4
{
    class Program
    {
        static void Main(string[] args)
        {
            Adventure testadventure = new Adventure("ItemData.txt");
            
            foreach (string thing in testadventure.test)
            {
                Console.WriteLine(thing);
            }

            Console.ReadKey();
            
        }
    }
}
