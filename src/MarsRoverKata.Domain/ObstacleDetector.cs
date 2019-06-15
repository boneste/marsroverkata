using System;
using System.Collections.Generic;

namespace MarsRoverKata.Domain
{
    public class ObstacleDetector
    {
        public const ObstacleDetector Null = null;

        protected ObstacleDetector() { }

        private List<Coordinates> obstacleMap;

        /// <summary>
        /// The creation of ObstacleDetector requires the map of known obstacles
        /// </summary>
        /// <param name="obstacleList">The map of known obstacles</param>
        /// <returns></returns>
        public static ObstacleDetector Create(List<Coordinates> obstacleMap)
        {
            if(obstacleMap == null)
            throw new ArgumentNullException($"{nameof(obstacleMap)}");

            ObstacleDetector obj = new ObstacleDetector();
            obj.obstacleMap = obstacleMap;
            return obj;
        }

        /// <summary>
        /// The ObstacleDetector detects if and obstacle is at the specified coordinates
        /// </summary>
        /// <param name="coordinates">Coordinates to check</param>
        /// <returns>Returns true when an obstacle is found</returns>
        public bool VerifyCoordinates(Coordinates coordinates)
        {
            return obstacleMap.Contains(coordinates);
        }
    }
}