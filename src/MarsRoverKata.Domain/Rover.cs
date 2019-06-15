using System;

namespace MarsRoverKata.Domain
{
    public class Rover
    {
        public Coordinates Position { get; protected set; }
        public Direction Direction { get; protected set; }

        public static Rover Create(int initialPositionX, int initialPositionY, string direction)
        {
            var initialPosition = Coordinates.Create(initialPositionX, initialPositionY);
            var initialDirection = DirectionsList.GetByCode(direction);

            if (initialDirection == null)
                throw new ArgumentException($"{nameof(initialDirection)} was not recognized as valid direction");

            Rover newRover = new Rover();
            newRover.Position = initialPosition;
            newRover.Direction = initialDirection;
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

                switch(output.Result)
                {
                    case ResultCode.CommandNotRecognized:
                        output.Message = $"Command '{commands[i]}' at position {i} was not recognized as valid, the rover stopped processing other commands";
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

            if (command == CommandsList.MoveForward)
                Position = Position.GetForwardMovementCoordinates(Direction);
            if (command == CommandsList.MoveBackward)
                Position = Position.GetBackwardMovementCoordinates(Direction);
            if (command == CommandsList.TurnLeft)
                Direction = Direction.TurnLeft();
            if (command == CommandsList.TurnRight)
                Direction = Direction.TurnRight();

            return ResultCode.CommandExecuted;
        }
    }
}
