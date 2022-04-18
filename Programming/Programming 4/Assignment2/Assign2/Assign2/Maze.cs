using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace Assign2
{
    class Maze
    {
        public char[][] CharMaze;
        public int RowLength;
        public int ColumnLength;
        public Point StartingPoint;
        public bool HasBeenSearched = false;
        Stack<Point> MazeStack = new Stack<Point>();
        public Stack<Point> WayToFollow { get; set; }


        /// <summary>
        /// Grabs maze data, starting point, and row/column length from a text file.
        /// </summary>
        /// <param name="fileName">Relative file name of text file to read</param>
        public Maze(string fileName)
        {
            // Create new streamreader and grab the two numbers from the first row to get the row and column lengths
            StreamReader sr = new StreamReader(fileName);
            string firstline = sr.ReadLine();
            RowLength = Convert.ToInt32(firstline.Substring(0, 3));
            ColumnLength = Convert.ToInt32(firstline.Substring(firstline.Length - 3, 3));

            // Grab the two numbers from the second line, which are the X and Y starting points
            string secondline = sr.ReadLine();
            int firstnum = Convert.ToInt32(secondline.Substring(0, 1));
            int secondnum = Convert.ToInt32(secondline.Substring(2, 1));
            StartingPoint = new Point(firstnum, secondnum);

            // Read each line of the maze itself and add to the charMaze
            string currentline;
            CharMaze = new char[RowLength][];
            for (int i = 0; i < RowLength; i++)
            {
                 currentline = sr.ReadLine();
                 CharMaze[i] = currentline.ToCharArray();
            }
        }

        /// <summary>
        /// Create a new maze based on given starting params and an already existing charmaze
        /// </summary>
        /// <param name="startingRow">Starting point row</param>
        /// <param name="startingColumn">Starting point column</param>
        /// <param name="existingMaze">An already existing charMaze</param>
        public Maze(int startingRow, int startingColumn, char[][] existingMaze)
        {
            
            // Throw exception if the value at the starting point in E (an exit) or W (a wall)
            if (existingMaze[startingRow][startingColumn].Equals('E') || existingMaze[startingRow][startingColumn].Equals('W'))
            {
                throw new ApplicationException("Starting point cannot be on the exit or wall");
            }

            // ...Or if the starting column is invalid
            else if (startingColumn > existingMaze[0].Length || startingColumn < 0)
            {
                throw new ApplicationException("Invalid column value");
            }

            // Or if the starting row is.
            else if (startingRow > existingMaze.Length || startingRow < 0)
            {
                throw new ApplicationException("Invalid row value");
            }

            // If all good, create new maze with given params
            CharMaze = existingMaze;
            StartingPoint = new Point(startingRow, startingColumn);
            RowLength = CharMaze.Length;
            ColumnLength = CharMaze[0].Length;
             



        }


        /// <summary>
        /// Returns the maze's starting point
        /// </summary>
        /// <returns>The starting point</returns>
        public Point GetStartingPoint()
        {
            return StartingPoint;
        }

        /// <summary>
        /// Returns the maze's row length
        /// </summary>
        /// <returns>The row length</returns>
        public int GetRowLength()
        {
            return RowLength;
        }


        /// <summary>
        /// Returns the maze's column length
        /// </summary>
        /// <returns>The column length</returns>
        public int GetColumnLength()
        {
            return ColumnLength;
        }


        /// <summary>
        /// Returns the charmaze
        /// </summary>
        /// <returns>Gets charmaze</returns>
        public char[][] GetMaze()
        {
            return CharMaze;
        }

        /// <summary>
        /// Converts the Charmaze into a single string, keeping the layout in the process
        /// </summary>
        /// <returns></returns>
        public string PrintMaze()
        {
            string currentarray = "";

            for (int i = 0; i < RowLength; i++)
            {
                for (int j = 0; j < ColumnLength; j++)
                {
                    currentarray += CharMaze[i][j];
                }
                currentarray += "\n";
            }
            currentarray = currentarray.Remove(currentarray.Length - 1);
            return currentarray;
        }

        /// <summary>
        /// Looks South, East, North, then West for a valid empty space or exit to move to.
        /// </summary>
        /// <returns>The first valid spot to go.</returns>
        public Point FindNextAvailableSpot()
        {
            Point findingPoint = null;
            if (CharMaze[MazeStack.Top().Row + 1][MazeStack.Top().Column] == ' ' || CharMaze[MazeStack.Top().Row + 1][MazeStack.Top().Column] == 'E')
            {
                findingPoint = new Point(MazeStack.Top().Row + 1,MazeStack.Top().Column);
            }
            else if (CharMaze[MazeStack.Top().Row][MazeStack.Top().Column - 1] == ' ' || CharMaze[MazeStack.Top().Row][MazeStack.Top().Column - 1] == 'E')
            {
                findingPoint = new Point(MazeStack.Top().Row, MazeStack.Top().Column - 1);
            }
            else if (CharMaze[MazeStack.Top().Row][MazeStack.Top().Column + 1] == ' ' || CharMaze[MazeStack.Top().Row] [MazeStack.Top().Column + 1] == 'E')
            {
                findingPoint = new Point(MazeStack.Top().Row, MazeStack.Top().Column + 1);
            }
            else if (CharMaze[MazeStack.Top().Row - 1][MazeStack.Top().Column] == ' ' || CharMaze[MazeStack.Top().Row - 1][MazeStack.Top().Column] == 'E')
            {
                findingPoint = new Point(MazeStack.Top().Row - 1, MazeStack.Top().Column);
            }

                return findingPoint;
        }


        /// <summary>
        /// Does a Depth First Search to find an exit for a maze, or report that none exists.
        /// </summary>
        /// <returns>Returns either a success message detailing the steps to get out of the maze, or a failure message saying it impossible.</returns>
        public string DepthFirstSearch()
        {

            //Push starting point to the maze stack, and set HasBeenSearched to true, which allows running of the find way out command later. 
            // Creates the way to follow stack.
            MazeStack.Push(StartingPoint);
            HasBeenSearched = true;
            WayToFollow = new Stack<Point>();

            //While the maze stack is empty, check if the exit has been found. If so, create the list of steps needed to exit.
            while (!MazeStack.IsEmpty())
            {
                if (CharMaze[MazeStack.Top().Row][MazeStack.Top().Column].Equals('E'))
                {
                    //Success
                    string results;
                    results = "Path to follow from Start ";
                    results += StartingPoint.ToString();
                    results += " to Exit " + MazeStack.Top().ToString();
                    results += " - " + MazeStack.Size() + " steps:\n";

                    Stack<Point> CopyStack = new Stack<Point>();

                    while (!MazeStack.IsEmpty())
                    {
                        WayToFollow.Push(MazeStack.Top());
                        CopyStack.Push(MazeStack.Pop());
                    }

                    while (!CopyStack.IsEmpty())
                    {
                        if (CharMaze[CopyStack.Top().Row][CopyStack.Top().Column].Equals('V'))
                        {
                            CharMaze[CopyStack.Top().Row][CopyStack.Top().Column] = '.';
                        }
                        results += CopyStack.Pop().ToString();
                        results += "\n";
                    }

                    return results + PrintMaze();
                }
                // Otherwise, keep finding the next available path and following it.
                else
                {
                    CharMaze[MazeStack.Top().Row][MazeStack.Top().Column] = 'V';
                    Point p = FindNextAvailableSpot();
                   if (p == null)
                    {
                        MazeStack.Pop();
                    }
                    else
                    {
                        MazeStack.Push(p);
                    }
                }

            }

            //If it hits all the way down here, there is no possible exit.
            WayToFollow = MazeStack;
            return "No exit found in maze!\n\n" + PrintMaze();
        }


        /// <summary>
        /// Return the path to follow to exit.
        /// </summary>
        /// <returns>The way out</returns>
        public Stack<Point> GetPathToFollow()
        {
            if (HasBeenSearched == false)
            {
                throw new ApplicationException("Run a search first.");
            }
            // Make a new stack of points and copy over to a new one, then return that.
            Stack <Point> ReturnPath = new Stack<Point>();
            ReturnPath.Head = WayToFollow.Head;
            ReturnPath.size = WayToFollow.size;
            return ReturnPath;
        }

    }
}
