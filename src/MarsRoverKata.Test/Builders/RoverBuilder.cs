using System.Collections.Generic;
using MarsRoverKata.Domain;

namespace MarsRoverKata.Test
{
    public class RoverBuilder
    {
        private int initialPositionX = Planet.MinX;
        private int initialPositionY = Planet.MinY;
        private string initialDirection = "N";
        private List<Coordinates> obstacleMap = new List<Coordinates>();

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

        public RoverBuilder WithObstacleDetectionSystem(Dictionary<int, int> map)
        {
            foreach(var item in map)
                obstacleMap.Add(Coordinates.Create(item.Key, item.Value));
            
            return this;
        }

        public Rover Build()
        {
            var obstacleDetector = ObstacleDetector.Create(obstacleMap);
            return Rover.Create(initialPositionX, initialPositionY, initialDirection, obstacleDetector);
        }
    }
}