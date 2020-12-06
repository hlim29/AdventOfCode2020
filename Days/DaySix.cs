using System;
using System.Linq;
using static AdventOfCode2020.Common;

namespace AdventOfCode2020.Days
{
    public class DaySix
    {
        private readonly string[] _input;

        public DaySix()
        {
            _input = ReadRaw("daysix.txt").Split("\n\n");
        }

        public void Process()
        {
            Console.WriteLine($"Part 1: {_input.Sum(x => x.Replace("\n", "").Distinct().Count())}");
            Console.WriteLine($"Part 2: {_input.Sum(GetCommonResponses)}");
        }

        private int GetCommonResponses(string input)
        {
            var lists = input.Split("\n");
            var common = lists[0].ToCharArray();
            foreach (var list in lists.Skip(1))
            {
                common = common.Intersect(list).ToArray();
            }
            return common.Length;
        }
    }
}
