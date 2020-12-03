using System;
using System.Collections.Generic;
using System.Linq;
using static AdventOfCode2020.Common;

namespace AdventOfCode2020.Days
{
    public class DayThree
    {
        private readonly string[] _input;
        private readonly int _length;
        private readonly int _width;

        public DayThree()
        {
            _input = ReadFile("daythree.txt");
            _length = _input[0].Length;
            _width = _input.Length; 
        }

        public void Process()
        {
            Console.WriteLine($"Part 1: {Traverse(3, 1)}");
            Console.WriteLine($"Part 2: {Traverse(1, 1) * Traverse(3, 1) * Traverse(5, 1) * Traverse(7, 1) * Traverse(1, 2)}");
        }

        private long Traverse(int xMultiplier, int yMultiplier)
        {
            var path = new List<char>();
            for (var i = 0; i < _width / yMultiplier; i++)
            {
                path.Add(_input[i * yMultiplier][i * xMultiplier % _length]);
            }
            return path.Count(x => x == '#');
        }
    }
}
