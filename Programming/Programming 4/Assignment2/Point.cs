using System;

namespace Assignment2
{
    public class Point
    {
        public int row { get; set; }
        public int column { get; set; }

        public Point(int row, int column)
        {
            this.row = row;
            this.column = column;
        }

        public int Row()
        {
            return this.row;
        }

        public int Column()
        {
            return this.column;
        }

        public override string ToString()
        {
            return ("[{1},{2}]", row, column);
    }
    }
}
