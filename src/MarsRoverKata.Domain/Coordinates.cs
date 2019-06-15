using System;
using System.Collections.Generic;

namespace MarsRoverKata.Domain
{
    public class Coordinates : ValueObject
    {
        protected Coordinates() { }
        
        public int X { get; protected set; }
        public int Y { get; protected set; }

        public static Coordinates Create(int x, int y)
        {
            if (x < Planet.MinX || x > Planet.MaxX)
                throw new ArgumentException($"{nameof(x)} is outside of the grid's limits");

            if (y < Planet.MinY || y > Planet.MaxY)
                throw new ArgumentException($"{nameof(y)} is outside of the grid's limits");

            Coordinates newCoordinates = new Coordinates();
            newCoordinates.X = x;
            newCoordinates.Y = y;

            return newCoordinates;
        }

        /// <summary>
        /// Calculates the new coordinates for a forward movement given the direction of the Rover
        /// </summary>
        /// <param name="direction">The considered Rover's direction</param>
        /// <returns></returns>
        public Coordinates GetForwardMovementCoordinates(Direction direction)
        {
            int newX = this.X;
            int newY = this.Y;

            if (direction == DirectionsList.N)
                newY = GetIncreasedY(1);
            if (direction == DirectionsList.E)
                newX = GetIncreasedX(1);
            if (direction == DirectionsList.S)
                newY = GetDecreasedY(1);
            if (direction == DirectionsList.W)
                newX = GetDecreasedX(1);

            return Coordinates.Create(newX, newY);
        }

        /// <summary>
        /// Calculates the new coordinates for a backward movement given the direction of the Rover
        /// </summary>
        /// <param name="direction">The considered Rover's direction</param>
        /// <returns></returns>
        public Coordinates GetBackwardMovementCoordinates(Direction direction)
        {
            int newX = this.X;
            int newY = this.Y;

            if (direction == DirectionsList.N)
                newY = GetDecreasedY(1);
            if (direction == DirectionsList.E)
                newX = GetDecreasedX(1);
            if (direction == DirectionsList.S)
                newY = GetIncreasedY(1);
            if (direction == DirectionsList.W)
                newX = GetIncreasedX(1);

            return Coordinates.Create(newX, newY);
        }

        #region Private methods

        private int GetIncreasedX(int steps)
        {
            return (X + steps) % Planet.GridWidth;
        }

        private int GetDecreasedX(int steps)
        {
            return (X + Planet.GridWidth - steps) % Planet.GridWidth;
        }

        private int GetIncreasedY(int steps)
        {
            return (Y + steps) % Planet.GridHeight;
        }

        private int GetDecreasedY(int steps)
        {
            return (Y + Planet.GridHeight - steps) % Planet.GridHeight;
        }

        #endregion

        protected override IEnumerable<object> GetAtomicValues()
        {
            // Using a yield return statement to return each element one at a time
            yield return X;
            yield return Y;
        }
    }
}