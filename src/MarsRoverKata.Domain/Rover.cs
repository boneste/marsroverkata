using System;

namespace MarsRoverKata.Domain
{
    public class Rover
    {
        public Direction Direction { get; protected set; }

        public static Rover Create(Direction direction)
        {
            Rover newRover = new Rover();
            newRover.Direction = direction;
            return newRover;
        }
    }
}
