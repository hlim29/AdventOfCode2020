using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Days
{
    public class DayOne
    {
        private readonly List<int> _entries;

        public DayOne()
        {
            _entries = Common.ReadFile("dayone.txt").Select(x => int.Parse(x)).ToList();
        }

        private string GetTwoSums()
        {
            var firstEntry = _entries.Where(x => _entries.Any(y => y == (2020 - x))).First();
            var secondEntry = 2020 - firstEntry;
            return $"{firstEntry} * {secondEntry} = {firstEntry * secondEntry}";
        }

        private string GetThreeSums()
        {
            var differences = _entries.Select(x => 2020 - x).ToList();
            var sums = differences.Where(x => _entries.Any(y => _entries.Any(z => z + y == x))).Select(x => 2020 - x).ToList();
            return $"{string.Join(" * ", sums)} = {sums.Aggregate((a, x) => a * x)}";
        }

        public void Process()
        {
            Console.WriteLine($"Part 1: {GetTwoSums()}");
            Console.WriteLine($"Part 2: {GetThreeSums()}");
        }
    }
}
