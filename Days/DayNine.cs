using System;
using System.Linq;
using static AdventOfCode2020.Common;

namespace AdventOfCode2020.Days
{
    public class DayNine
    {
        private readonly long[] _input;

        public DayNine()
        {
            _input = ReadFile("daynine.txt").Select(x => long.Parse(x)).ToArray();
        }

        public void Process()
        {
            var lastNonSum = GetLastNonSum(25);

            var weaknessRange = GetEncryptonWeakness(lastNonSum);
            var slice = _input[weaknessRange.Lower..weaknessRange.Upper].OrderBy(x => x).ToList();

            Console.WriteLine($"Part 1: {lastNonSum}");
            Console.WriteLine($"Part 2: {slice.First() + slice.Last()}");            
        }

        private long GetLastNonSum(int distance)
        {
            for (int i = distance; i < _input.Count(); i++)
            {
                var preamble = _input[(i - distance)..i].ToList();
                var diff = preamble.Select(x => Math.Abs(_input[i] - x));

                if (diff.Intersect(preamble).Count() == 0)
                {
                    return _input[i];
                }
                preamble.Add(_input[i]);
            }
            return -1;
        }

        private (int Lower, int Upper) GetEncryptonWeakness(long value)
        {
            for (int i = 0; i < _input.Count(); i++)
            {
                var sum = _input[i];
                for (var j = i+1; j < _input.Count(); j++)
                {
                    sum += _input[j];
                    if (sum == value)
                    {
                        return (i, j);
                    }
                    else if (sum > value)
                    {
                        break;
                    }
                }
            }
            return (0,0);
        }
    }
}
