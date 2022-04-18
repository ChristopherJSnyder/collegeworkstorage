using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assign2
{
    class Program
    {
        static void Main(string[] args)
        {
            Maze newmaze = new Maze("simpleWithoutExit.maze");
            Console.WriteLine(newmaze.PrintMaze());
        }
    }
}
