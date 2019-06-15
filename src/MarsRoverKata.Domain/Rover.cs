using System;

namespace MarsRoverKata.Domain
{
    public class Rover
    {
        protected Rover() { }

        private ObstacleDetector obstacleDetector;
        public Coordinates Position { get; protected set; }
        public Direction Direction { get; protected set; }

        /// <summary>
        /// Instantiates a new Rover instance given the initial position coordinates, the initial direction
        /// and provides the Rover with an obstacle detection system
        /// </summary>
        /// <param name="initialPositionX">The starting X position on planet surface</param>
        /// <param name="initialPositionY">The starting Y position on planet surface</param>
        /// <param name="direction">The starting direction on planet surface</param>
        /// <param name="obstacleDetector">The obstacle detection system is mandatory for safely reasons</param>
        /// <returns></returns>
        public static Rover Create(int initialPositionX, int initialPositionY, string direction, ObstacleDetector obstacleDetector)
        {
            if (obstacleDetector == null)
                throw new ArgumentNullException($"{nameof(obstacleDetector)}");

            var initialPosition = Coordinates.Create(initialPositionX, initialPositionY);
            var initialDirection = DirectionsList.GetByCode(direction);

            if (initialDirection == null)
                throw new ArgumentException($"{nameof(initialDirection)} was not recognized as valid direction");

            Rover newRover = new Rover();
            newRover.Position = initialPosition;
            newRover.Direction = initialDirection;
            newRover.obstacleDetector = obstacleDetector;
            return newRover;
        }

        /// <summary>
        /// Asks the Rover to execute a sequence of commands.
        /// A ResultCode and optionally a message is returned to communicate if the command executed succesfully
        /// </summary>
        /// <param name="commands">the sequence of commands to be executed</param>
        /// <returns>The commands execution feedback</returns>
        public CommandSequenceExecutionResult ExecuteCommandSequence(char[] commands)
        {
            var output = new CommandSequenceExecutionResult()
            {
                Result = ResultCode.CommandExecuted
            };

            for (int i = 0; i < commands.Length; ++i)
            {
                output.Result = ExecuteSingleCommand(commands[i]);

                switch (output.Result)
                {
                    case ResultCode.CommandNotRecognized:
                        output.Message = $"Command '{commands[i]}' at position {i} was not recognized as valid, the rover stopped processing other commands";
                        return output;
                    case ResultCode.ObstacleFound:
                        output.Message = $"An obstacle was detected trying to execute command '{commands[i]}' at position {i}, the rover stopped processing other commands";
                        return output;
                    default:
                        break;
                }
            }

            return output;
        }

        /// <summary>
        /// Asks the Rover to execute a command.
        /// A ResultCode is returned to communicate if the command executed succesfully
        /// </summary>
        /// <param name="commandCode">The commandCode to be executed</param>
        /// <returns>The command execution feedback</returns>
        private ResultCode ExecuteSingleCommand(char commandCode)
        {
            var command = CommandsList.GetByCode(commandCode);

            if (command == null)
                return ResultCode.CommandNotRecognized;

            Coordinates newPosition = Position;
            if (command == CommandsList.MoveForward)
                newPosition = Position.GetForwardMovementCoordinates(Direction);
            if (command == CommandsList.MoveBackward)
                newPosition = Position.GetBackwardMovementCoordinates(Direction);
            if (command == CommandsList.TurnLeft)
                Direction = Direction.TurnLeft();
            if (command == CommandsList.TurnRight)
                Direction = Direction.TurnRight();

            if (obstacleDetector.VerifyCoordinates(newPosition))
                return ResultCode.ObstacleFound;

            Position = newPosition;

            return ResultCode.CommandExecuted;
        }
    }
}
