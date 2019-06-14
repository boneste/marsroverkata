using System;
using MarsRoverKata.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MarsRoverKata.Test
{
    /* Test list
     * - Successfully create the obj verifying properties
     * - x or y can be equal to the lower and upper limit
     * - x or y lower than allowed
     * - x or y higher than allowed
     * Movement calculation
     * - Succesfully calculate a forward and backward movement
     * - Test case limits using min and max allowed coordinates
     * - Test each possible starting direction N,E,S,W
     */
    [TestClass]
    public class CoordinatesTest
    {
        /// <summary>
        /// Tests that Coordinates class instantiate properties correctly
        /// </summary>
        /// <param name="x">The x value to set</param>
        /// <param name="y">The y value to set</param>
        [DataTestMethod]
        [DataRow(Planet.MinX, Planet.MinY)]
        [DataRow(Planet.MaxX, Planet.MaxY)]
        [DataRow(Planet.MinX + 1, Planet.MinY + 1)]
        public void Create_VerifyObjectConfiguration(int x, int y)
        {
            var actual = Coordinates.Create(x, y);

            Assert.AreEqual(x, actual.X);
            Assert.AreEqual(y, actual.Y);
        }

        /// <summary>
        /// Tests that Coordinates class doesn't accept points outside of the configured boundaries
        /// </summary>
        /// <param name="x">The x value to set</param>
        /// <param name="y">The y value to set</param>
        [DataTestMethod]
        [DataRow(Planet.MinX - 1, Planet.MinY)]
        [DataRow(Planet.MinX, Planet.MinY - 1)]
        [DataRow(Planet.MaxX + 1, Planet.MaxY)]
        [DataRow(Planet.MaxX, Planet.MaxY + 1)]
        [ExpectedException(typeof(ArgumentException))]
        public void Create_InvalidCoordinates(int x, int y)
        {
            Coordinates.Create(x, y);
        }

        #region GetForwardMovementCoordinates

        /// <summary>
        /// Tests forward movement given the North direction and actual position set to the four edges
        /// </summary>
        /// <param name="startX">The initial x value to set</param>
        /// <param name="startY">The initial y value to set</param>
        /// <param name="endX">The expected x value after calculation</param>
        /// <param name="endY">The expected y value after calculation</param>
        [DataTestMethod]
        [DataRow(Planet.MinX, Planet.MinY, Planet.MinX, Planet.MinY + 1)]
        [DataRow(Planet.MaxX, Planet.MinY, Planet.MaxX, Planet.MinY + 1)]
        [DataRow(Planet.MinX, Planet.MaxY, Planet.MinX, Planet.MinY)]
        [DataRow(Planet.MaxX, Planet.MaxX, Planet.MaxX, Planet.MinY)]
        public void GetForwardMovementCoordinates_PositionLimits_DirectionNorth(int startX, int startY, int endX, int endY)
        {
            var position = Coordinates.Create(startX, startY);

            var actual = position.GetForwardMovementCoordinates(DirectionsList.N);

            Assert.AreEqual(endX, actual.X);
            Assert.AreEqual(endY, actual.Y);
        }

        /// <summary>
        /// Tests forward movement given the East direction and actual position set to the four edges
        /// </summary>
        /// <param name="startX">The initial x value to set</param>
        /// <param name="startY">The initial y value to set</param>
        /// <param name="endX">The expected x value after calculation</param>
        /// <param name="endY">The expected y value after calculation</param>
        [DataTestMethod]
        [DataRow(Planet.MinX, Planet.MinY, Planet.MinX + 1, Planet.MinY)]
        [DataRow(Planet.MaxX, Planet.MinY, Planet.MinX, Planet.MinY)]
        [DataRow(Planet.MinX, Planet.MaxY, Planet.MinX + 1, Planet.MaxY)]
        [DataRow(Planet.MaxX, Planet.MaxX, Planet.MinX, Planet.MaxY)]
        public void GetForwardMovementCoordinates_PositionLimits_DirectionEast(int startX, int startY, int endX, int endY)
        {
            var position = Coordinates.Create(startX, startY);

            var actual = position.GetForwardMovementCoordinates(DirectionsList.E);

            Assert.AreEqual(endX, actual.X);
            Assert.AreEqual(endY, actual.Y);
        }

        /// <summary>
        /// Tests forward movement given the South direction and actual position set to the four edges
        /// </summary>
        /// <param name="startX">The initial x value to set</param>
        /// <param name="startY">The initial y value to set</param>
        /// <param name="endX">The expected x value after calculation</param>
        /// <param name="endY">The expected y value after calculation</param>
        [DataTestMethod]
        [DataRow(Planet.MinX, Planet.MinY, Planet.MinX, Planet.MaxY)]
        [DataRow(Planet.MaxX, Planet.MinY, Planet.MaxX, Planet.MaxY)]
        [DataRow(Planet.MinX, Planet.MaxY, Planet.MinX, Planet.MaxY - 1)]
        [DataRow(Planet.MaxX, Planet.MaxX, Planet.MaxX, Planet.MaxY - 1)]
        public void GetForwardMovementCoordinates_PositionLimits_DirectionSouth(int startX, int startY, int endX, int endY)
        {
            var position = Coordinates.Create(startX, startY);

            var actual = position.GetForwardMovementCoordinates(DirectionsList.S);

            Assert.AreEqual(endX, actual.X);
            Assert.AreEqual(endY, actual.Y);
        }

        /// <summary>
        /// Tests forward movement given the West direction and actual position set to the four edges
        /// </summary>
        /// <param name="startX">The initial x value to set</param>
        /// <param name="startY">The initial y value to set</param>
        /// <param name="endX">The expected x value after calculation</param>
        /// <param name="endY">The expected y value after calculation</param>
        [DataTestMethod]
        [DataRow(Planet.MinX, Planet.MinY, Planet.MaxX, Planet.MinY)]
        [DataRow(Planet.MaxX, Planet.MinY, Planet.MaxX - 1, Planet.MinY)]
        [DataRow(Planet.MinX, Planet.MaxY, Planet.MaxX, Planet.MaxY)]
        [DataRow(Planet.MaxX, Planet.MaxX, Planet.MaxX - 1, Planet.MaxY)]
        public void GetForwardMovementCoordinates_PositionLimits_DirectionWest(int startX, int startY, int endX, int endY)
        {
            var position = Coordinates.Create(startX, startY);

            var actual = position.GetForwardMovementCoordinates(DirectionsList.W);

            Assert.AreEqual(endX, actual.X);
            Assert.AreEqual(endY, actual.Y);
        }

        #endregion

        #region GetBackwardMovementCoordinates

        /// <summary>
        /// Tests backward movement given the North direction and actual position set to the four edges
        /// </summary>
        /// <param name="startX">The initial x value to set</param>
        /// <param name="startY">The initial y value to set</param>
        /// <param name="endX">The expected x value after calculation</param>
        /// <param name="endY">The expected y value after calculation</param>
        [DataTestMethod]
        [DataRow(Planet.MinX, Planet.MinY, Planet.MinX, Planet.MaxY)]
        [DataRow(Planet.MaxX, Planet.MinY, Planet.MaxX, Planet.MaxY)]
        [DataRow(Planet.MinX, Planet.MaxY, Planet.MinX, Planet.MaxY - 1)]
        [DataRow(Planet.MaxX, Planet.MaxX, Planet.MaxX, Planet.MaxY - 1)]
        public void GetBackwardMovementCoordinates_PositionLimits_DirectionNorth(int startX, int startY, int endX, int endY)
        {
            var position = Coordinates.Create(startX, startY);

            var actual = position.GetBackwardMovementCoordinates(DirectionsList.N);

            Assert.AreEqual(endX, actual.X);
            Assert.AreEqual(endY, actual.Y);
        }

        /// <summary>
        /// Tests backward movement given the East direction and actual position set to the four edges
        /// </summary>
        /// <param name="startX">The initial x value to set</param>
        /// <param name="startY">The initial y value to set</param>
        /// <param name="endX">The expected x value after calculation</param>
        /// <param name="endY">The expected y value after calculation</param>
        [DataTestMethod]
        [DataRow(Planet.MinX, Planet.MinY, Planet.MaxX, Planet.MinY)]
        [DataRow(Planet.MaxX, Planet.MinY, Planet.MaxX - 1, Planet.MinY)]
        [DataRow(Planet.MinX, Planet.MaxY, Planet.MaxX, Planet.MaxY)]
        [DataRow(Planet.MaxX, Planet.MaxX, Planet.MaxX - 1, Planet.MaxY)]
        public void GetBackwardMovementCoordinates_PositionLimits_DirectionEast(int startX, int startY, int endX, int endY)
        {
            var position = Coordinates.Create(startX, startY);

            var actual = position.GetBackwardMovementCoordinates(DirectionsList.E);

            Assert.AreEqual(endX, actual.X);
            Assert.AreEqual(endY, actual.Y);
        }

        /// <summary>
        /// Tests backward movement given the South direction and actual position set to the four edges
        /// </summary>
        /// <param name="startX">The initial x value to set</param>
        /// <param name="startY">The initial y value to set</param>
        /// <param name="endX">The expected x value after calculation</param>
        /// <param name="endY">The expected y value after calculation</param>
        [DataTestMethod]
        [DataRow(Planet.MinX, Planet.MinY, Planet.MinX, Planet.MinY + 1)]
        [DataRow(Planet.MaxX, Planet.MinY, Planet.MaxX, Planet.MinY + 1)]
        [DataRow(Planet.MinX, Planet.MaxY, Planet.MinX, Planet.MinY)]
        [DataRow(Planet.MaxX, Planet.MaxX, Planet.MaxX, Planet.MinY)]
        public void GetBackwardMovementCoordinates_PositionLimits_DirectionSouth(int startX, int startY, int endX, int endY)
        {
            var position = Coordinates.Create(startX, startY);

            var actual = position.GetBackwardMovementCoordinates(DirectionsList.S);

            Assert.AreEqual(endX, actual.X);
            Assert.AreEqual(endY, actual.Y);
        }

        /// <summary>
        /// Tests backward movement given the West direction and actual position set to the four edges
        /// </summary>
        /// <param name="startX">The initial x value to set</param>
        /// <param name="startY">The initial y value to set</param>
        /// <param name="endX">The expected x value after calculation</param>
        /// <param name="endY">The expected y value after calculation</param>
        [DataTestMethod]
        [DataRow(Planet.MinX, Planet.MinY, Planet.MinX + 1, Planet.MinY)]
        [DataRow(Planet.MaxX, Planet.MinY, Planet.MinX, Planet.MinY)]
        [DataRow(Planet.MinX, Planet.MaxY, Planet.MinX + 1, Planet.MaxY)]
        [DataRow(Planet.MaxX, Planet.MaxX, Planet.MinX, Planet.MaxY)]
        public void GetBackwardMovementCoordinates_PositionLimits_DirectionWest(int startX, int startY, int endX, int endY)
        {
            var position = Coordinates.Create(startX, startY);

            var actual = position.GetBackwardMovementCoordinates(DirectionsList.W);

            Assert.AreEqual(endX, actual.X);
            Assert.AreEqual(endY, actual.Y);
        }

        #endregion

        [TestMethod]
        public void CoordinatesIsAValueObject()
        {
            var coordinates1 = Coordinates.Create(Planet.MinX, Planet.MinY);
            var coordinates2 = Coordinates.Create(coordinates1.X, coordinates1.Y);

            Assert.AreEqual(coordinates1, coordinates2);
        }
    }
}