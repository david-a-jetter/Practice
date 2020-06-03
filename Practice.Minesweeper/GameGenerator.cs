using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Practice.Minesweeper
{
    public class GameGenerator
    {
        private Random _Random { get; }
        public GameGenerator()
        {
            _Random = new Random();
        }

        public Cell[,] GenerateGame(int xLength, int yLength)
        {
            var grid = InitiateGrid(xLength, yLength);
            CalculateGridDisplays(grid);

            return grid;
        }

        private void CalculateGridDisplays(Cell[,] grid)
        {
            var rowCount = grid.GetLength(0);
            var columnCount = grid.GetLength(1);
            for (int y = 0; y < rowCount; y++)
            {
                for (int x = 0; x < columnCount; x++)
                {
                    var cell = grid[y, x];
                    var upIndex = y - 1;
                    var rightIndex = x + 1;
                    var downIndex = y + 1;
                    var leftIndex = x - 1;

                    var adjacentBombs = 0;

                    adjacentBombs += AdjacentBombIncr(grid, upIndex, x);
                    adjacentBombs += AdjacentBombIncr(grid, upIndex, rightIndex);
                    adjacentBombs += AdjacentBombIncr(grid, y, rightIndex);
                    adjacentBombs += AdjacentBombIncr(grid, downIndex, rightIndex);
                    adjacentBombs += AdjacentBombIncr(grid, downIndex, x);
                    adjacentBombs += AdjacentBombIncr(grid, downIndex, leftIndex);
                    adjacentBombs += AdjacentBombIncr(grid, y, leftIndex);
                    adjacentBombs += AdjacentBombIncr(grid, upIndex, leftIndex);

                    cell.SetDisplay(adjacentBombs.ToString());
                }
            }
        }

        private int AdjacentBombIncr(Cell[,] grid, int y, int x)
        {
            var rowCount = grid.GetLength(0);
            var columnCount = grid.GetLength(1);

            if (y < 0 || x < 0 || y >= rowCount || x >= columnCount)
            {
                return 0;
            }

            var adjacent = grid[y, x];
           
            if (adjacent.IsBomb)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        private Cell[,] InitiateGrid(int xLength, int yLength)
        {
            var grid = new Cell[yLength, xLength];
            for (int y = 0; y < yLength; y++)
            {
                for (int x = 0; x < xLength; x++)
                {
                    grid[y, x] = new Cell(RandomBomb());
                }
            }

            return grid;
        }

        private bool RandomBomb()
        {
            var isBomb = false;
            var next = _Random.Next(10);

            if (next > 7)
            {
                isBomb = true;
            }

            return isBomb;


        }
    }
}
