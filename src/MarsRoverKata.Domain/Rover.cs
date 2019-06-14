using System;

namespace MarsRoverKata.Domain
{
    public class Rover
    {
        public Coordinates Position { get; protected set; }
        public Direction Direction { get; protected set; }

        public static Rover Create(Coordinates initialPosition, Direction direction)
        {
            Rover newRover = new Rover();
            newRover.Position = initialPosition;
            newRover.Direction = direction;
            return newRover;
        }
    }
}
