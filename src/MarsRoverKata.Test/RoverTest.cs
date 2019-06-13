using Microsoft.VisualStudio.TestTools.UnitTesting;
using MarsRoverKata.Domain;

namespace MarsRoverKata.Test
{
    [TestClass]
    public class RoverTest
    {
        [TestMethod]
        public void Land_VerifyObjectConfiguration()
        {
            Direction direction = DirectionsList.N;

            var rover = Rover.Create(direction);
            
            Assert.AreEqual(direction, rover.Direction);
        }
    }
}
