using System;
using System.Collections.Generic;
using System.Linq;
using static AdventOfCode2020.Common;

namespace AdventOfCode2020.Days
{
    public class DayTwelve
    {
        private readonly List<string> _input;
        private readonly List<(char Direction, int Unit)> _steps;
        private readonly List<char> _compass;

        public DayTwelve()
        {
            _input = ReadFile("daytwelve.txt").ToList();
            _steps = _input.Select(x => (x[0], int.Parse(x[1..]))).ToList();
            _compass = new List<char> { 'N', 'E', 'S', 'W' };
        }

        public void Process()
        {
            Console.WriteLine($"Part 1: {PartOne()}");
            Console.WriteLine($"Part 1: {PartTwo()}");
        }

        public int PartOne()
        {
            var currentDirection = 90;
            var currentCoord = (X: 0, Y: 0);
            foreach (var step in _steps)
            {
                if (step.Direction == 'R')
                {
                    currentDirection = (currentDirection + step.Unit + 360) % 360;
                } 
                else if (step.Direction == 'L')
                {
                    currentDirection = (currentDirection - step.Unit + 360) % 360;
                }
                else if (step.Direction == 'F')
                {
                    var offset = CalculateOffset(currentDirection, step.Unit);
                    currentCoord = (currentCoord.X + offset.X, currentCoord.Y + offset.Y);
                }
                else if (_compass.Contains(step.Direction))
                {
                    var offset = CalculateOffset(step.Direction, step.Unit);
                    currentCoord = (currentCoord.X + offset.X, currentCoord.Y + offset.Y);
                }
            }
            return Math.Abs(currentCoord.X) + Math.Abs(currentCoord.Y);
        }

        public int PartTwo()
        {
            var currentCoord = (X: 0, Y: 0);
            var wayPoint = (X: 10, Y: 1);
            foreach (var step in _steps)
            {
                if (step.Direction == 'R')
                {
                    wayPoint = RotateWaypoint(step.Unit, wayPoint);
                }
                else if (step.Direction == 'L')
                {
                    wayPoint = RotateWaypoint(-step.Unit, wayPoint);
                }
                else if (step.Direction == 'F')
                {
                    currentCoord = (currentCoord.X + wayPoint.X * step.Unit, currentCoord.Y + wayPoint.Y * step.Unit);
                }
                else if (_compass.Contains(step.Direction))
                {
                    var (X, Y) = CalculateOffset(step.Direction, step.Unit);
                    wayPoint = (wayPoint.X + X, wayPoint.Y + Y);
                }
            }
            return Math.Abs(currentCoord.X) + Math.Abs(currentCoord.Y);
        }

        private (int X, int Y) RotateWaypoint(int degrees, (int X, int Y) waypoint)
        {
            switch (degrees)
            {
                case 90:
                case -270:
                    return (waypoint.Y, -waypoint.X);
                case 180:
                case -180:
                    return (-waypoint.X, -waypoint.Y);
                case 270:
                case -90:
                    return (-waypoint.Y, waypoint.X);
            }
            return (0, 0);
        }

        private (int X, int Y) CalculateOffset(int degrees, int unit)
        {
            switch (degrees)
            {
                case 0:
                case 'N': 
                    return (0, unit);
                case 90:
                case 'E':
                    return (unit, 0);
                case 180:
                case 'S': 
                    return (0, -unit);
                case 270:
                case 'W': 
                    return (-unit, 0);
            }
            return (0, 0);
        }
    }
}
