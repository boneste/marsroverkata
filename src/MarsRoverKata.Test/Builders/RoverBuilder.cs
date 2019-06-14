using MarsRoverKata.Domain;

namespace MarsRoverKata.Test
{
    public class RoverBuilder
    {
        private int initialPositionX = Planet.MinX;
        private int initialPositionY = Planet.MinY;
        private Direction initialDirection = DirectionsList.N;

        public RoverBuilder LandedIn(int x, int y)
        {
            initialPositionX = x;
            initialPositionY = y;

            return this;
        }

        public RoverBuilder DirectedTowards(Direction direction)
        {
            initialDirection = direction;

            return this;
        }

        public Rover Build()
        {
            var initialPosition = Coordinates.Create(initialPositionX, initialPositionY);
            return Rover.Create(initialPosition, initialDirection);
        }
    }
}