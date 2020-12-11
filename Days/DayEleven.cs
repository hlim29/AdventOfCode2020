using System;
using System.Collections.Generic;
using System.Linq;
using static AdventOfCode2020.Common;

namespace AdventOfCode2020.Days
{
    public class DayEleven
    {
        private readonly List<string> _input;
        private readonly Dictionary<(int X, int Y), char> _map;
        private readonly HashSet<(int X, int Y)> _keys;
        private List<(int X, int Y)> _directions;

        public DayEleven()
        {
            _input = ReadFile("dayeleven.txt").ToList();
            _map = new Dictionary<(int X, int Y), char>();
            _directions = new List<(int X, int Y)> { (-1, 0), (-1, -1), (0, -1), (1, -1), (1, 0), (1, 1), (0, 1), (-1, 1) };

            for (int i = 0; i < _input.Count(); i++)
            {
                for (int j = 0; j < _input[0].Count(); j++)
                {
                    _map.Add((j, i), _input[i][j]);
                }
            }

            _keys = new HashSet<(int X, int Y)>(_map.Keys);
        }

        public void Process()
        {
            Console.WriteLine($"Part 1: {Solve(4, false)}");
            Console.WriteLine($"Part 2: {Solve(5, true)}");
        }

        private int Solve(int tolerance, bool isPartTwo)
        {
            var map = new Dictionary<(int X, int Y), char>(_map);
            var occupyCounts = new List<int> { CountOccupied(map) };

            while (occupyCounts.Count() < 3 || occupyCounts[^1] != occupyCounts[^2])
            {
                occupyCounts.Add(CountOccupied(map));
                map = Occupy(map, tolerance, isPartTwo);
            }

            return CountOccupied(map);
        }

        private Dictionary<(int X, int Y), char> Occupy(Dictionary<(int X, int Y), char> map, int tolerance, bool isPartTwo)
        {
            var newMap = new Dictionary<(int X, int Y), char>();
            var keys = _keys.ToList();
            foreach (var item in keys)
            {
                var neighbours = isPartTwo 
                    ? GetVisibleNeighbours(item, map)
                    : _directions.Select(x => (x.X + item.X, x.Y + item.Y)).Where(x => map.TryGetValue((x.Item1, x.Item2), out var value)).Select(x => map[x]).ToList();

                var occupiedNeighbours = neighbours.Where(x => x == '#').ToList();
                
                if (map[item] == 'L' && occupiedNeighbours.Count() == 0)
                {
                    newMap[item] = '#';
                }
                else if (map[item] == '#' && occupiedNeighbours.Count() >= tolerance)
                {
                    newMap[item] = 'L';
                }
                else
                {
                    newMap[item] = map[item];
                }
            }

            return newMap;
        }

        private int CountOccupied(Dictionary<(int X, int Y), char> map)
        {
            return map.Values.Count(x => x == '#');
        }

        private List<char> GetVisibleNeighbours((int X, int Y) currentSquare, Dictionary<(int X, int Y), char> map)
        {
            var neighbours = new List<char>();

            foreach (var direction in _directions)
            {
                var multiplier = 1;
                var square = (X: direction.X * multiplier + currentSquare.X, Y: direction.Y * multiplier + currentSquare.Y);
                while (_keys.Contains(square))
                {
                    if (map[square] != '.')
                    {
                        neighbours.Add(map[square]);
                        break;
                    }
                    multiplier++;
                    square = (X: direction.X * multiplier + currentSquare.X, Y: direction.Y * multiplier + currentSquare.Y);
                }
            }

            return neighbours;
        }
    }
}
