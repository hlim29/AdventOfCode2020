using System;
using System.Collections.Generic;
using System.Linq;
using static AdventOfCode2020.Common;

namespace AdventOfCode2020.Days
{
    public class DayTwentyfive
    {
        private readonly List<long> _input;

        public DayTwentyfive()
        {
            _input = ReadFile("daytwentyfive.txt").Select(long.Parse).ToList();
        }

        public void Process()
        {
            Console.WriteLine($"Merry Christmas!");
            Console.WriteLine($"Part 1: {PartOne()}");
            Console.WriteLine($"Part 2: null");
        }

        public long PartOne()
        {
            var doorLoopSize = GetLoopSize(_input[1], 7);
            return Transform(_input[0], doorLoopSize);
        }

        private long GetLoopSize(long pubKey, long subject)
        {
            var current = 1L;
            for (int i = 1; ; i++)
            {
                current = current * subject % 20201227;
                if (pubKey == current)
                {
                    return i;
                }
            }
        }

        private long Transform(long subject, long loop)
        {
            var current = 1L;
            for (int i = 1; i <= loop; i++)
            {
                current = current * subject % 20201227;
            }
            return current;
        }
    }
}
