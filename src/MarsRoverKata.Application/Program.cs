using System;
using System.Collections.Generic;
using MarsRoverKata.Domain;

namespace MarsRoverKata.Application
{
    class Program
    {
        static void Main(string[] args)
        {
            var obstacleMap = new List<Coordinates>()
            {
                Coordinates.Create(3,5),
                Coordinates.Create(6,4)
            };

            var obstacleDetector = ObstacleDetector.Create(obstacleMap);

            var rover = Rover.Create(1, 1, "N", obstacleDetector);
            Console.WriteLine($"Welcome to Mars, you landed in x: {rover.Position.X} y: {rover.Position.Y} and your direction is {rover.Direction.Code}");
            Console.WriteLine("Please insert commands:");

            while (true)
            {
                var commandString = Console.ReadLine();

                if (commandString == "Quit")
                    return;

                var result = rover.ExecuteCommandSequence(commandString.ToCharArray());

                Console.WriteLine(result.Result.ToString());
                Console.WriteLine($"Your actual position is x: {rover.Position.X} y: {rover.Position.Y} and your direction is {rover.Direction.Code}");
                Console.WriteLine(result.Message);
            }
        }
    }
}
