using System;
using System.Collections.Generic;
using System.Linq;
using static AdventOfCode2020.Common;

namespace AdventOfCode2020.Days
{
    public class DayTwentyfour
    {
        private readonly List<string> _input;
        private readonly List<string> _diagonals = new List<string> { "se", "sw", "nw", "ne" };
        private Dictionary<(double X, double Y), bool> _grid;
        private readonly Dictionary<string, (double X, double Y)> _offsets;
        private readonly List<(double X, double Y)> _offsetValues;

        public DayTwentyfour()
        {
            _input = ReadFile("daytwentyfour.txt").ToList();
            _offsets = new Dictionary<string, (double, double)> 
            { 
                {"e", (1,0) },
                {"w", (-1,0) },
                {"se", (0.5,-1) },
                {"sw", (-0.5,-1) },
                {"nw", (-0.5,1) },
                {"ne", (0.5,1) },
            };
            _grid = new Dictionary<(double X, double Y), bool>();
            _offsetValues = _offsets.Values.Select(x => x).ToList();
        }

        public void Process()
        {
            Console.WriteLine($"Part 1: {PartOne()}");
            Console.WriteLine($"Part 2: {PartTwo()}");
        }

        private int PartOne()
        {
            return GetTiles().Count(x => !x.Value);
        }

        private int PartTwo()
        {
            _grid = GetTiles();
            Enumerable.Range(1, 100).ToList().ForEach(_ =>
            {
                var tiles = _grid.Keys.ToList();
                var copy = new Dictionary<(double X, double Y), bool>(_grid);
                var keys = _grid.Keys.ToHashSet();

                foreach (var tile in tiles)
                {
                    var neighbours = _offsetValues.Select(x => (x.X + tile.X, x.Y + tile.Y)).ToList();
                    neighbours.ForEach(tile =>
                    {
                        copy[tile] = UpdateTile(tile, keys);
                    });

                    copy[tile] = UpdateTile(tile, keys);
                }

                _grid = new Dictionary<(double X, double Y), bool>(copy);
            });

            return _grid.Values.Count(x => !x);
        }

        private bool UpdateTile((double X, double Y) tile, HashSet<(double X, double Y)> keys)
        {
            var exists = keys.Contains(tile);
            var inactiveNeighbours = _offsetValues.Select(x => (x.X + tile.X, x.Y + tile.Y)).Count(item => keys.Contains(item) && !_grid[item]);
            var isInactive = exists && !_grid[tile];

            if (isInactive && (inactiveNeighbours == 0 || inactiveNeighbours > 2))
            {
                return true;
            }
            else if (!isInactive && inactiveNeighbours == 2)
            {
                return false;
            }

            return !exists || _grid[tile];
        }

        private Dictionary<(double X, double Y), bool> GetTiles()
        {
            var toFlip = new List<(double X, double Y)>();
            foreach (var line in _input)
            {
                var steps = new List<string>();
                var buffer = "";

                for (var i = 0; i < line.Length; i++)
                {
                    buffer += line[i];
                    if (i < line.Length - 1)
                    {
                        buffer += line[i + 1];
                    }

                    if (_diagonals.Contains(buffer))
                    {
                        steps.Add(buffer);
                        i++;
                    }
                    else
                    {
                        steps.Add(buffer[0].ToString());
                    }
                    buffer = string.Empty;
                }

                var coord = (X: 0.0, Y: 0.0);
                steps.Select(x => _offsets[x]).ToList().ForEach(x => coord = (coord.X + x.X, coord.Y + x.Y));
                toFlip.Add(coord);
            }

            return toFlip.GroupBy(x => x).ToDictionary(x => x.Key, x => x.Count() % 2 == 0);
        }
    }
}
