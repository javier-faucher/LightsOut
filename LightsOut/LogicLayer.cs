using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightsOut
{

    public class LogicLayer
    {
        public bool[,] grid;
        public int boxesLeft;
        public int clicks;
        public LogicLayer()
        {
             generateGrid();
        }
        public void generateGrid()
        {
            clicks = 0;
            Random rand = new Random();
            bool[,] grids = new bool[5,5];
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    #region generate completly random grid 
                    bool check = rand.Next(3) == 0;
                    grids[i, j] = check;
                    #endregion
                    #region Create grid which is nearly completed
                    //if (i < 2 && j < 2)
                    //{
                    //    grids[i, j] = false;
                    //}
                    //else
                    //{
                    //    grids[i, j] = true;
                    //}
                    //if (i == 1 && j == 1)
                    //{
                    //    grids[i, j] = true;
                    //}

                    #endregion
                }
            }
            grid = grids;
        }
        
        public bool checkCompleted()
        {
            boxesLeft = 0;
            bool complete = true;
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for( int j = 0; j < grid.GetLength(1); j++)
                {
                    if (!grid[i, j])
                    {
                        boxesLeft++;
                        complete = false;
                    }
                }
            }
            return complete;
        }
        public bool[,] onClick(int [] position)
        {
            clicks++;
            int row = position[0];
            int column = position[1];
            bool check = grid[row, column];
            grid[row, column] = !grid[row, column];
            bool check1 = grid[row, column];
            try
            {
                grid[row, column + 1] = !grid[row, column + 1];
            }
            catch
            {

            }
            try
            {
                grid[row, column - 1] = !grid[row, column - 1];
            }
            catch
            {

            }
            try
            {
                grid[row + 1, column] = !grid[row + 1, column];
            }
            catch
            {

            }
            try
            {
                grid[row - 1, column] = !grid[row - 1, column];
            }
            catch
            {

            }
            return grid;
        }
    }

}
