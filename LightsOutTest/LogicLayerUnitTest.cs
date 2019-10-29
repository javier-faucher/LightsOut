using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LightsOut;

namespace LightsOutTest
{
    [TestClass]
    public class LogicLayerUnitTest
    {
        private LogicLayer logic;
        /// <summary>
        /// Test to ensure that on initialising the logic layer a 5x5 grid of booleans is created
        /// </summary>
        [TestMethod]    
        public void GenerateGridTest()
        {
            logic = new LogicLayer();
            bool[,] verify = new bool[5,5];
            Assert.IsTrue(logic.grid.GetLength(0) == 5);
            Assert.IsTrue(logic.grid.GetLength(1) == 5);
            var x = logic.grid.GetType();
            Assert.IsTrue(logic.grid.GetType() == verify.GetType());
        }
        /// <summary>
        /// Test to ensure that the onClick method changes the state of the correct cells in the grids.
        /// </summary>

        [TestMethod]
        public void onClickTest()
        {
            logic = new LogicLayer();

            int[] clickPosition = new int[2] { 3, 2 };
            bool aboveStateInit = logic.grid[2, 2];
            bool belowStateInit = logic.grid[4, 2];
            bool rightStateInit = logic.grid[3, 3];
            bool leftStateInit = logic.grid[3, 1];
            bool middleStateinit = logic.grid[3, 2];

            logic.onClick(clickPosition);
            // Assert that the initial state of the cell has been changed
            Assert.IsFalse(aboveStateInit == logic.grid[2, 2]);
            Assert.IsFalse(belowStateInit == logic.grid[4, 2]);
            Assert.IsFalse(rightStateInit == logic.grid[3, 3]);
            Assert.IsFalse(leftStateInit == logic.grid[3, 1]);
            Assert.IsFalse(middleStateinit == logic.grid[3, 2]);   
        }
        /// <summary>
        /// Thi test ensures that the checkCompleted method returns true when the grid is complete
        /// and return false when the grid is not complete 
        /// </summary>
        [TestMethod]
        public void checkCompletedTest()
        {
            logic = new LogicLayer();
            bool[,] nearlyComplete = new bool[5,5];
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                { 
                    if (i < 2 && j < 2)
                    {
                        nearlyComplete[i, j] = false;
                    }
                    else
                    {
                        nearlyComplete[i, j] = true;
                    }
                    if (i == 1 && j == 1)
                    {
                        nearlyComplete[i, j] = true;
                    }
                }
            }
            logic.grid = nearlyComplete;
            int[] clickPosition = new int[2] { 0, 0 };
            Assert.IsFalse(logic.checkCompleted());
            logic.onClick(clickPosition);
            Assert.IsTrue(logic.checkCompleted());
            
        }
    }
}
