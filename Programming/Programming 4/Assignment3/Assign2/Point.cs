﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assign3
{
    public class Point
    {
        public int Row { get; set; }
        public int Column { get; set; }
        public Point Parent { get; set; }

        /// <summary>
        /// Create a new point in the maze.
        /// </summary>
        /// <param name="row">Row of point</param>
        /// <param name="column">Column of point</param>
        public Point(int row, int column) : this(row,column,null)
        {
        }


        /// <summary>
        /// Create a new point in the maze.
        /// </summary>
        /// <param name="row">Row of point</param>
        /// <param name="column">Column of point</param>
        public Point(int row, int column, Point parent)
        {
            this.Row = row;
            this.Column = column;
            this.Parent = parent;
        }

        /// <summary>
        /// Displays the Point in a clear readable format
        /// </summary>
        /// <returns>Returns a point in the form of [row, column]</returns>
        public override string ToString()
        {
            return ("[" + Row + ", " + Column + "]");
        }
    }
}
