using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment2
{
        public class Program
        {
            public static void Main(string[] args)
            {
                Maze testmaze = new Maze("simpleWithExit");
                Console.WriteLine(testmaze.test);
                Console.WriteLine(testmaze);
                


            }
        }
    }

