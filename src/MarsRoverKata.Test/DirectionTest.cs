using Microsoft.VisualStudio.TestTools.UnitTesting;
using MarsRoverKata.Domain;

namespace MarsRoverKata.Test
{
    [TestClass]
    public class DirectionTest
    {
        [DataTestMethod]
        [DataRow("N", "E")]
        [DataRow("E", "S")]
        [DataRow("S", "W")]
        [DataRow("W", "N")]
        public void TurnRight(string start, string end)
        {
            var before = DirectionsList.GetByCode(start);
            var expected = DirectionsList.GetByCode(end);

            var actual = before.TurnRight();

            Assert.AreEqual(expected, actual);
        }

        [DataTestMethod]
        [DataRow("N", "W")]
        [DataRow("E", "N")]
        [DataRow("S", "E")]
        [DataRow("W", "S")]
        public void TurnLeft(string start, string end)
        {
            var before = DirectionsList.GetByCode(start);
            var expected = DirectionsList.GetByCode(end);

            var actual = before.TurnLeft();
            
            Assert.AreEqual(expected, actual);
        }
    }
}