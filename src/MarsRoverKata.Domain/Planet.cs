using System;

namespace MarsRoverKata.Domain
{
    /// <summary>
    /// This configuration class contains planet's surface specs
    /// - By convention coordinates are numbers >= 0 for this reference system
    /// </summary>
    public static class Planet
    {
        public const int MinX = 0;
        public const int MaxX = 99;

        public const int MinY = 0;
        public const int MaxY = 99;

        public const int GridWidth = MaxX + 1;
        public const int GridHeight = MaxY + 1;
    }
}