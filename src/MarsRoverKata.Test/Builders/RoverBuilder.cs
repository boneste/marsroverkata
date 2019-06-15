using MarsRoverKata.Domain;

namespace MarsRoverKata.Test
{
    public class RoverBuilder
    {
        private int initialPositionX = Planet.MinX;
        private int initialPositionY = Planet.MinY;
        private string initialDirection = "N";

        public RoverBuilder LandedIn(int x, int y)
        {
            initialPositionX = x;
            initialPositionY = y;

            return this;
        }

        public RoverBuilder DirectedTowards(string directionCode)
        {
            initialDirection = directionCode;

            return this;
        }

        public Rover Build()
        {
            return Rover.Create(initialPositionX, initialPositionY, initialDirection);
        }
    }
}