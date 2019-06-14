using Microsoft.VisualStudio.TestTools.UnitTesting;
using MarsRoverKata.Domain;

namespace MarsRoverKata.Test
{
    [TestClass]
    public class RoverTest
    {
        /// <summary>
        /// Tests the correct initialization of Rover's properties
        /// </summary>
        [TestMethod]
        public void Create_VerifyObjectConfiguration()
        {
            Coordinates initialPosition = Coordinates.Create(1, 1);
            Direction direction = DirectionsList.N;

            var rover = Rover.Create(initialPosition, direction);

            Assert.AreEqual(direction, rover.Direction);
            Assert.AreEqual(initialPosition, rover.Position);
        }

        /// <summary>
        /// Tests the RoverBuilder building skills
        /// </summary>
        [TestMethod]
        public void RoverBuilderTest()
        {
            int initialPositionX = 1;
            int initialPositionY = 2;
            Direction direction = DirectionsList.E;

            var actual = new RoverBuilder().LandedIn(initialPositionX, initialPositionY)
                                           .DirectedTowards(direction)
                                           .Build();

            Assert.AreEqual(direction, actual.Direction);
            Assert.AreEqual(initialPositionX, actual.Position.X);
            Assert.AreEqual(initialPositionY, actual.Position.Y);
        }
    }
}
