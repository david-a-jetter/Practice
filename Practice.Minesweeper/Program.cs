using System;
using System.Linq;

namespace Practice.Minesweeper
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Enter number of Rows:");
            var yLength = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Enter number of Columns:");
            var xLength = Int32.Parse(Console.ReadLine());

            var generator = new GameGenerator();
            var gameGrid = generator.GenerateGame(xLength, yLength);
            var gameWon = true;
            PrintGrid(gameGrid, Console.Write);
            while (! IsGameWon(gameGrid))
            {
                Console.WriteLine("Enter X Axis to click:");
                var x = Int32.Parse(Console.ReadLine());
                Console.WriteLine("Enter Y Axis to click:");
                var y = Int32.Parse(Console.ReadLine());

                var cell = gameGrid[y, x];
                cell.Click();
                PrintGrid(gameGrid, Console.Write);
                if (cell.IsBomb)
                {
                    Console.WriteLine("You clicked a bomb :(");
                    gameWon = false;
                    break;
                }
            }

            if (gameWon)
            {
                Console.WriteLine("You Won!");
                Console.WriteLine(Environment.NewLine);
            }
            else
            {
                Console.WriteLine("You Lost :(");
            }
        }

        private static void PrintGrid(Cell[,] grid, Action<string> write)
        {
            var rowCount = grid.GetLength(0);
            var columnCount = grid.GetLength(1);
            for (int y = 0; y < rowCount; y++)
            {
                for (int x = 0; x < columnCount; x++)
                {
                    var cell = grid[y, x];
                    write(cell.Display);
                }
                write(Environment.NewLine);
            }
        }

        private static bool IsGameWon(Cell[,] grid)
        {
            var won = true;

            foreach(var cell in grid)
            {
                if (!cell.IsBomb && !cell.Clicked)
                {
                    won = false;
                    break;
                }
            }

            return won;
        }
    }
}
