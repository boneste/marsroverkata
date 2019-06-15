using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace MarsRoverKata.Domain
{
    public class Direction
    {
        protected Direction() { }
        
        public int Index { get; protected set; }
        public string Code { get; protected set; }

        public Direction(byte index, string code)
        {
            this.Index = index;
            this.Code = code;
        }

        public Direction TurnRight()
        {
            var nextDirectionIndex = Convert.ToByte((this.Index + 1) % DirectionsList.AllDirections.Count);
            return DirectionsList.GetByIndex(nextDirectionIndex);
        }

        public Direction TurnLeft()
        {
            var nextDirectionIndex = Convert.ToByte((this.Index + DirectionsList.AllDirections.Count - 1) % DirectionsList.AllDirections.Count);
            return DirectionsList.GetByIndex(nextDirectionIndex);
        }
    }

    public class DirectionsList : ReadOnlyCollection<Direction>
    {
        public static readonly Direction N = new Direction(0, "N");
        public static readonly Direction E = new Direction(1, "E");
        public static readonly Direction S = new Direction(2, "S");
        public static readonly Direction W = new Direction(3, "W");

        public static readonly ReadOnlyCollection<Direction> AllDirections = new ReadOnlyCollection<Direction>(new List<Direction> { N, E, S, W });

        public DirectionsList() : base(AllDirections) { }

        public static Direction GetByIndex(byte index)
        {
            return AllDirections.Single(x => x.Index == index);
        }

        public static Direction GetByCode(string code)
        {
            return AllDirections.SingleOrDefault(x => x.Code == code);
        }
    }
}