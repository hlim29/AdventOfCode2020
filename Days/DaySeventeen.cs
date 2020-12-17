using System;
using System.Collections.Generic;
using System.Linq;
using static AdventOfCode2020.Common;

namespace AdventOfCode2020.Days
{
    public class DaySeventeen
    {
        private readonly List<(int x, int y, int z, int w)> _neighbourOffsets;
        private Dictionary<(int x, int y, int z, int w), char> _cubes;

        public DaySeventeen()
        {
            var input = ReadFile("dayseventeen.txt").ToList();
            _neighbourOffsets = GenerateNeighours().ToList();
            _neighbourOffsets.Remove((0, 0, 0, 0));
            _cubes = input.SelectMany((x, i) => x.Select((y, j) => (Coord: (j, i, 0, 0), Char: y))).ToDictionary(x => x.Coord, x => x.Char);
        }

        public void Process()
        {
            var initialState = _cubes;
            Console.WriteLine($"Part 1: {Solve(false)}");

            _cubes = initialState;
            Console.WriteLine($"Part 2: {Solve(true)}");
        }

        private int Solve(bool isPartTwo)
        {
            var result = 0;
            for (int i = 0; i < 6; i++)
            {
                result = RunCycle(isPartTwo);
            }
            return result;
        }

        private int RunCycle(bool isPartTwo)
        {
            var nextDict = new Dictionary<(int x, int y, int z, int w), char>();

            Expand(isPartTwo);

            var keys = _cubes.Keys.ToList();
            foreach (var key in keys)
            {
                var activeNeighbours = isPartTwo
                    ? _neighbourOffsets.Select(x => (key.x + x.x, key.y + x.y, key.z + x.z, key.w + x.w)).Where(x => _cubes.ContainsKey(x) && _cubes[x] == '#').Count()
                    : _neighbourOffsets.Where(x => x.w == 0).Select(x => (key.x + x.x, key.y + x.y, key.z + x.z, 0)).Where(x => _cubes.ContainsKey(x) && _cubes[x] == '#').Count();

                char nextStatus;

                if (_cubes[key] == '#')
                    nextStatus = activeNeighbours == 2 || activeNeighbours == 3 ? '#' : '.';
                else
                    nextStatus = activeNeighbours == 3 ? '#' : '.';

                nextDict[key] = nextStatus;

            }
            _cubes = nextDict;
            return _cubes.Keys.Count(x => _cubes[x] == '#');
        }

        private void Expand(bool isPartTwo)
        {
            var keys = _cubes.Keys.ToList();
            foreach (var key in keys)
            {
                var neighbours = isPartTwo
                    ? _neighbourOffsets.Select(x => (key.x + x.x, key.y + x.y, key.z + x.z, key.w + x.w))
                    : _neighbourOffsets.Select(x => (key.x + x.x, key.y + x.y, key.z + x.z, 0));

                foreach (var neighbour in neighbours)
                {
                    if (!_cubes.TryGetValue(neighbour, out var value))
                    {
                        _cubes[neighbour] = '.';
                    }
                }
            }
        }

        private IEnumerable<(int x, int y, int z, int w)> GenerateNeighours()
        {
            for (int x = -1; x < 2; x++)
                for (int y = -1; y < 2; y++)
                    for (int z = -1; z < 2; z++)
                        for (int w = -1; w < 2; w++)
                            yield return (x, y, z, w);
        }
    }
}
