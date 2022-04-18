using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Assignment2
{
    class Maze
    {
        public char[][] charMaze;
        public int rowLength;
        public int columnLength;
        public Point startingPoint;
        public string test;

        public Maze (string fileName)
        {
            StreamReader sr = new StreamReader(fileName);
            test = fileName;
            
        }

        public Maze (int startingRow, int startingColumn, char[][] existingMaze)
        {

        }


        public Point GetStartingPoint()
        {
            return startingPoint;
        }


        public int GetRowLength()
        {
            return rowLength;
        }

        
        public int GetColumnLength()
        {
            return columnLength;
        }


        public char[][] GetMaze()
        {
            return charMaze;
        }


    }
}
