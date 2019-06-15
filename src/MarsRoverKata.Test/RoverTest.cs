using Microsoft.VisualStudio.TestTools.UnitTesting;
using MarsRoverKata.Domain;
using System;
using System.Collections.Generic;

namespace MarsRoverKata.Test
{
    /* Test list
     * - Create rover succesfully
     * - Pass an invalid initialDirection during creation
     * - Pass an invalid initialPosition during creation
     * - The creation withouth obstacle detection system will fail
     * - Move the rover forward
     * - Move the rover backward
     * - Turn the rover left
     * - Turn the rover right
     * - The rover receives an invalid command
     * - Test multiple commands
     * - Test multiple commands with an invalid command in it
     * - Test multiple commands execution with obstacle on the path
     */
    [TestClass]
    public class RoverTest
    {
        /// <summary>
        /// Tests the correct initialization of Rover's properties
        /// </summary>
        [TestMethod]
        public void Create_VerifyObjectConfiguration()
        {
            int initialPositionX = 1;
            int initialPositionY = 2;
            string initialDirection = "N";
            var obstacleDetector = ObstacleDetector.Create(new List<Coordinates>());

            var initialPosition = Coordinates.Create(initialPositionX, initialPositionY);
            var direction = DirectionsList.GetByCode(initialDirection);

            var rover = Rover.Create(initialPositionX, initialPositionY, initialDirection, obstacleDetector);

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
            string direction = "E";
            var obstacleDetector = ObstacleDetector.Create(new List<Coordinates>());

            var actual = new RoverBuilder().LandedIn(initialPositionX, initialPositionY)
                                           .DirectedTowards(direction)
                                           .Build();

            Assert.AreEqual(direction, actual.Direction.Code);
            Assert.AreEqual(initialPositionX, actual.Position.X);
            Assert.AreEqual(initialPositionY, actual.Position.Y);
        }

        /// <summary>
        /// Tests that an invalid initial direction is validated
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Create_InvalidDirection()
        {
            var actual = new RoverBuilder().DirectedTowards("Z")
                                           .Build();
        }

        /// <summary>
        /// Tests that the rover cannot be instantiated without an obstacle detection system
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Create_WithouthObstacleDetectionSystem()
        {
            int initialPositionX = 1;
            int initialPositionY = 2;
            string initialDirection = "N";
            var obstacleDetector = ObstacleDetector.Null;

            var rover = Rover.Create(initialPositionX, initialPositionY, initialDirection, obstacleDetector);
        }

        /// <summary>
        /// Tests that an invalid initial position is validated
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Create_InvalidInitialPosition()
        {
            var actual = new RoverBuilder().LandedIn(-1, -1)
                                           .Build();
        }

        /// <summary>
        /// Tests the MoveForward command on the Rover
        ///  - The position should change accordingly to the direction
        ///  - The direction should not be affected
        /// </summary>
        [TestMethod]
        public void MoveForward()
        {
            var rover = new RoverBuilder().Build();
            var position = rover.Position;
            var direction = rover.Direction;
            var commands = new char[] { 'f' };

            var result = rover.ExecuteCommandSequence(commands);

            Assert.AreEqual(ResultCode.CommandExecuted, result.Result);
            Assert.AreEqual(position.GetForwardMovementCoordinates(direction), rover.Position);
            Assert.AreEqual(direction, rover.Direction);
        }

        /// <summary>
        /// Tests the MoveBackward command on the Rover
        ///  - The position should change accordingly to the direction
        ///  - The direction should not be affected
        /// </summary>
        [TestMethod]
        public void MoveBackward()
        {
            var rover = new RoverBuilder().Build();
            var position = rover.Position;
            var direction = rover.Direction;
            var commands = new char[] { 'b' };

            var result = rover.ExecuteCommandSequence(commands);

            Assert.AreEqual(ResultCode.CommandExecuted, result.Result);
            Assert.AreEqual(position.GetBackwardMovementCoordinates(direction), rover.Position);
            Assert.AreEqual(direction, rover.Direction);
        }

        /// <summary>
        /// Tests the TurnLeft command on the Rover
        ///  - The position should not be affected
        ///  - The direction should change accordingly
        /// </summary>
        [TestMethod]
        public void TurnLeft()
        {
            var rover = new RoverBuilder().Build();
            var position = rover.Position;
            var direction = rover.Direction;
            var commands = new char[] { 'l' };

            var result = rover.ExecuteCommandSequence(commands);

            Assert.AreEqual(ResultCode.CommandExecuted, result.Result);
            Assert.AreEqual(position, rover.Position);
            Assert.AreEqual(direction.TurnLeft(), rover.Direction);
        }

        /// <summary>
        /// Tests the TurnRight command on the Rover
        ///  - The position should not be affected
        ///  - The direction should change accordingly
        /// </summary>
        [TestMethod]
        public void TurnRight()
        {
            var rover = new RoverBuilder().Build();
            var position = rover.Position;
            var direction = rover.Direction;
            var commands = new char[] { 'r' };

            var result = rover.ExecuteCommandSequence(commands);

            Assert.AreEqual(ResultCode.CommandExecuted, result.Result);
            Assert.AreEqual(position, rover.Position);
            Assert.AreEqual(direction.TurnRight(), rover.Direction);
        }

        /// <summary>
        /// Tests a sequence of commands reprensenting the Knight movement (game of chess)
        ///  - [f,f,r,f]
        /// </summary>
        [TestMethod]
        public void KnightMovement()
        {
            var rover = new RoverBuilder().LandedIn(1, 1)
                                          .DirectedTowards("N")
                                          .Build();
            var commands = new char[] { 'f', 'f', 'r', 'f' };
            var expectedPosition = Coordinates.Create(2, 3);
            var expectedDirection = DirectionsList.E;

            var result = rover.ExecuteCommandSequence(commands);

            Assert.AreEqual(ResultCode.CommandExecuted, result.Result);
            Assert.AreEqual(expectedPosition, rover.Position);
            Assert.AreEqual(expectedDirection, rover.Direction);
        }

        /// <summary>
        /// Tests a sequence of commands reprensenting the Knight movement reversed (game of chess)
        ///  - The knight turns back and moves to the initial position
        ///  - [r,r,f,l,f,f]
        /// </summary>
        [TestMethod]
        public void KnightMovement_Reversed()
        {
            var rover = new RoverBuilder().LandedIn(2, 3)
                                          .DirectedTowards("E")
                                          .Build();
            var commands = new char[] { 'r', 'r', 'f', 'l', 'f', 'f' };
            var expectedPosition = Coordinates.Create(1, 1);
            var expectedDirection = DirectionsList.S;

            var result = rover.ExecuteCommandSequence(commands);

            Assert.AreEqual(ResultCode.CommandExecuted, result.Result);
            Assert.AreEqual(expectedPosition, rover.Position);
            Assert.AreEqual(expectedDirection, rover.Direction);
        }

        /// <summary>
        /// Tests the exectution attempt with an invalid command
        /// </summary>
        [TestMethod]
        public void ExecuteCommandSequence_InvalidCommand()
        {
            var rover = new RoverBuilder().Build();
            var commands = new char[] { 'Z' };

            var result = rover.ExecuteCommandSequence(commands);

            Assert.AreEqual(ResultCode.CommandNotRecognized, result.Result);
        }

        /// <summary>
        /// Tests a sequence of commands reprensenting the Knight movement (game of chess)
        ///  - The sequence contains an invalid command
        /// </summary>
        [TestMethod]
        public void KnightMovement_InvalidSequence()
        {
            var rover = new RoverBuilder().LandedIn(1, 1)
                                          .DirectedTowards("N")
                                          .Build();
            var commands = new char[] { 'f', 'f', 'Z', 'f' };
            var expectedPosition = Coordinates.Create(1, 3);
            var expectedDirection = DirectionsList.N;

            var result = rover.ExecuteCommandSequence(commands);

            Assert.AreEqual(ResultCode.CommandNotRecognized, result.Result);
            Assert.AreEqual(expectedPosition, rover.Position);
            Assert.AreEqual(expectedDirection, rover.Direction);
        }

        /// <summary>
        /// Tests a sequence of commands when obstacles are on the path
        /// </summary>
        [TestMethod]
        public void MovementWithObstacleOnThePath()
        {
            var obstacleMap = new Dictionary<int, int>() { {99, 3} };

            var rover = new RoverBuilder().LandedIn(1, 1)
                                          .DirectedTowards("N")
                                          .WithObstacleDetectionSystem(obstacleMap)
                                          .Build();
            var commands = new char[] { 'f', 'f', 'r', 'b', 'b' };
            var expectedPosition = Coordinates.Create(0, 3);
            var expectedDirection = DirectionsList.E;

            var result = rover.ExecuteCommandSequence(commands);

            Assert.AreEqual(ResultCode.ObstacleFound, result.Result);
            Assert.AreEqual(expectedPosition, rover.Position);
            Assert.AreEqual(expectedDirection, rover.Direction);
        }
    }
}
