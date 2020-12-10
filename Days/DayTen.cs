using System;
using System.Collections.Generic;
using System.Linq;
using static AdventOfCode2020.Common;

namespace AdventOfCode2020.Days
{
    public class DayTen
    {
        private readonly List<int> _input;

        public DayTen()
        {
            _input = ReadFile("dayten.txt").Select(x => int.Parse(x)).ToList();
        }

        public void Process()
        {
            var diff1 = 0;
            var diff2 = 0;
            _input.Add(0);
            _input.Sort();
            for (int i = 0; i < _input.Count()-1; i++)
            {
                var diff = _input[i + 1] - _input[i];
                if (diff == 1)
                {
                    diff1++;
                } 
                else if (diff == 3)
                {
                    diff2++;
                }
            }
            Console.WriteLine($"{diff1} {diff2}");
            Console.WriteLine($"{diff1*diff2}");
        }
    }
}
