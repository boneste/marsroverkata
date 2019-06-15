using System;
using System.Collections.Generic;
using MarsRoverKata.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MarsRoverKata.Test
{
    /* Test list
     * - It's impossible to create an obstacle if no map is provided
     * - Test the detection of an obstacle
     */
    [TestClass]
    public class ObstacleDetectorTest
    {
        /// <summary>
        /// Tests that the obstacleMap is mandatory for detector creation
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Create_NullMap()
        {
            ObstacleDetector.Create(null);
        }

        /// <summary>
        /// Tests that the obstacle is detected in the expected position
        /// </summary>
        [TestMethod]
        public void VerifyCoordinates()
        {
            var obstacleCoord = Coordinates.Create(1, 1);
            var obstacleMap = new List<Coordinates>() { obstacleCoord };

            var obstacleDetector = ObstacleDetector.Create(obstacleMap);
            var obstacleFound = obstacleDetector.VerifyCoordinates(obstacleCoord);

            Assert.IsTrue(obstacleFound);
        }
    }
}