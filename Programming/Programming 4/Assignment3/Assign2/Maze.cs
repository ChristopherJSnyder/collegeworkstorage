using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace Assign3
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
        public Queue<Point> Directions { get; set; }
        public Queue<Point> NewQueue { get; set; }
        public Point EndPoint { get; set; }


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
        public Boolean FindNextAvailableSpot(Point DQ)
        {
           
            Directions.Enqueue(new Point(DQ.Row + 1, DQ.Column, DQ));
            Directions.Enqueue(new Point(DQ.Row, DQ.Column -1, DQ));
            Directions.Enqueue(new Point(DQ.Row, DQ.Column + 1, DQ));
            Directions.Enqueue(new Point(DQ.Row - 1, DQ.Column, DQ));

            while(!Directions.IsEmpty())
            {
                Point directionInProcess = Directions.Dequeue();
                //directionInProcess.Parent = DQ;

                if(CharMaze[directionInProcess.Row][directionInProcess.Column] == 'E')
                {
                    EndPoint = directionInProcess;
                    WayToFollow.Push(directionInProcess);
                    directionInProcess = directionInProcess.Parent;
                    while(directionInProcess != null){
                        CharMaze[directionInProcess.Row][directionInProcess.Column] = '.';
                        WayToFollow.Push(directionInProcess);
                        directionInProcess = directionInProcess.Parent;

                    }
                    return true;
                }
                else if(CharMaze[directionInProcess.Row][directionInProcess.Column] == ' ')
                {
                    CharMaze[directionInProcess.Row][directionInProcess.Column] = 'V';
                    NewQueue.Enqueue(directionInProcess);
                }
            }
            return false;
        }


        public string BreadthFirstSearch()
        {
            Boolean found = false;
            Directions = new Queue<Point>();
            NewQueue = new Queue<Point>();
            NewQueue.Enqueue(StartingPoint);
            WayToFollow = new Stack<Point>();
            HasBeenSearched = true;
            
            while (!NewQueue.IsEmpty() && !found)
            {
                Point CurrentTop = NewQueue.Dequeue();
                found = FindNextAvailableSpot(CurrentTop);
            }

            if (found)
            {
                //WayToFollow.Push(StartingPoint);
                //CharMaze[StartingPoint.Row][StartingPoint.Column] = '.';
                string results;
                results = "Path to follow from Start ";
                results += StartingPoint.ToString();
                results += " to Exit " + EndPoint.ToString();
                results += " - " + WayToFollow.Size() + " steps:\n";

                
                Node<Point> processPoint = WayToFollow.Head;
                

                while (processPoint != null)
                {
                    results += processPoint.Element.ToString() + "\n";
                    processPoint = processPoint.Previous;
                }
                results += PrintMaze();
                return results;
            }

            return "No exit found in maze!\n\n" + PrintMaze();
        }





        public Stack<Point> GetPathToFollow()
        {
            if (HasBeenSearched == false)
            {
                throw new ApplicationException("Run a search first.");
            }
            // Make a new stack of points and copy over to a new one, then return that.
            Stack<Point> ReturnPath = new Stack<Point>();
            ReturnPath.Head = WayToFollow.Head;
            ReturnPath.size = WayToFollow.size;
            return ReturnPath;
        }
    }
}
