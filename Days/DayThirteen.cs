using System;
using System.Collections.Generic;
using System.Linq;
using static AdventOfCode2020.Common;

namespace AdventOfCode2020.Days
{
    public class DayThirteen
    {
        private readonly List<string> _input;
        private readonly List<long> _busses;
        private readonly List<long> _earliestDepartures;
        private readonly long _departTime;

        public DayThirteen()
        {
            _input = ReadFile("daythirteen.txt").ToList();
            _departTime = long.Parse(_input[0]);
            _busses = _input[1].Split(",").Where(x => long.TryParse(x, out var _)).Select(x => long.Parse(x)).ToList();
            _earliestDepartures = new List<long>();

            for (int i = 0; i < _input[1].Split(",").Count(); i++)
            {
                if (int.TryParse(_input[1].Split(",")[i], out var value))
                {
                    _earliestDepartures.Add(i + 1);
                }
            }
        }

        public void Process()
        {
            Console.WriteLine($"Part 1: {PartOne()}");
            Console.WriteLine($"Part 2: {PartTwo()}");
        }

        private long PartOne()
        {
            var sortedTimes = _busses.OrderBy(x => Math.Abs(_departTime % x - x)).ToList();
            return sortedTimes.First() * Math.Abs(_departTime % sortedTimes.First() - sortedTimes.First());
        }

        private long PartTwo()
        {
            var result =  SolveChineseRemainder(_busses, _earliestDepartures);
            var modTotal = _busses.Aggregate((x, y) => x * y);

            return modTotal - result + 1;
        }

        private static long SolveChineseRemainder(List<long> order, List<long> modValues)
        {
            long prod = order.Aggregate(1L, (i, j) => i * j);
            long p;
            long sm = 0;
            for (int i = 0; i < order.Count(); i++)
            {
                p = prod / order[i];
                sm += modValues[i] * ModularMultiplicativeInverse(p, order[i]) * p;
            }
            return sm % prod;
        }

        private static long ModularMultiplicativeInverse(long a, long mod)
        {
            long b = a % mod;
            for (long x = 1; x < mod; x++)
            {
                if ((b * x) % mod == 1)
                {
                    return x;
                }
            }
            return 1;
        }
    }
}
