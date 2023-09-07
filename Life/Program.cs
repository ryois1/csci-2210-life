using System.Threading;

namespace Life
{

    enum Cell
    {
        UNOCCUPIED, OCCUIPIED
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Cell[,] grid = new Cell[40,80];
            Occupy(grid);
            DrawGrid(grid);
            Thread.Sleep(2000);
            UpdateGrid(grid);
            DrawGrid(grid);
        }

        /// <summary>
        /// Draw grid onto console with given data
        /// </summary>
        /// <param name="grid">2D Array of Cells to Draw</param>
        static void DrawGrid(Cell[,] grid)
        {
            Console.Clear();
            for (int x = 0; x < grid.GetLength(0); x++)
            {
                for (int y = 0; y < grid.GetLength(1); y++)
                {
                    switch (grid[x, y])
                    {
                        case Cell.UNOCCUPIED:
                            Console.BackgroundColor = ConsoleColor.Black;
                            Console.Write("U ");
                            break;
                        case Cell.OCCUIPIED:
                            Console.BackgroundColor = ConsoleColor.Green;
                            Console.Write("O");
                            Console.BackgroundColor = ConsoleColor.Black;
                            Console.Write(' ');
                            break;
                    }
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Creates a random starting position for interesting 
        /// results each time you run the program
        /// </summary>
        /// <param name="grid">2D Array of Cells to Occupy</param>
        /// <param name="percentOccupied">Float representation of a percentage of cells to occupy. 0-1</param>
        static void Occupy(Cell[,] grid, float percentOccupied = 0.05f)
        {
            Random random = new Random();
            for (int i = 0; i < grid.Length * percentOccupied; i++)
            {
                int x = random.Next(grid.GetLength(0));
                int y = random.Next(grid.GetLength(1));
                grid[x, y] = Cell.OCCUIPIED;
            }
        }

        /// <summary>
        /// Figure out how many occupied neighbors are around a given point
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        static int CountOccupiedNeighbors(Cell[,] grid, int x, int y)
        {
            int count = 0;

            // Top Left
            count += grid[x - 1, y - 1] == Cell.OCCUIPIED ? 1 : 0;
            // Top
            count += grid[x, y - 1] == Cell.OCCUIPIED ? 1 : 0;
            // Top Right
            count += grid[x + 1, y - 1] == Cell.OCCUIPIED ? 1 : 0;
            // Left
            count += grid[x - 1, y] == Cell.OCCUIPIED ? 1 : 0;
            // Right
            count += grid[x + 1, y] == Cell.OCCUIPIED ? 1 : 0;
            // Bottom Left
            count += grid[x - 1, y + 1] == Cell.OCCUIPIED ? 1 : 0;
            // Bottom
            count += grid[x, y +1 ] == Cell.OCCUIPIED ? 1 : 0;
            // Bottom Right
            count += grid[x + 1, y + 1] == Cell.OCCUIPIED ? 1 : 0;

            return count;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="grid">2D Array of Cells to Draw</param>
        static void UpdateGrid(Cell[,] grid)
        {
            for (int x = 0; x < grid.GetLength(0); x++)
            {
                for (int y = 0; y < grid.GetLength(1); y++)
                {
                    // Any live cell with two or three live neighbours survives.
                    // Any dead cell with three live neighbours becomes a live cell.
                    // All other live cells die in the next generation.Similarly, all other dead cells stay dead.

                    int occupiedNeigborCount = CountOccupiedNeighbors(grid, x, y);
                    if (occupiedNeigborCount < 2 || occupiedNeigborCount > 3)
                    {
                        grid[x, y] = Cell.UNOCCUPIED;
                    } 
                    else if (occupiedNeigborCount == 3 ) 
                    {
                        grid[x, y] = Cell.OCCUIPIED;
                    }

                }
            }
        }
    }
}