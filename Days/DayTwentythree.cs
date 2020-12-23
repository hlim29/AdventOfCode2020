using System;
using System.Collections.Generic;
using System.Linq;
using static AdventOfCode2020.Common;

namespace AdventOfCode2020.Days
{
    public class DayTwentythree
    {
        private List<long> _input;

        public DayTwentythree()
        {
            _input = ReadRaw("daytwentythree.txt").Select(x => long.Parse(x.ToString())).ToList();
        }

        public void Process()
        {
            Console.WriteLine($"Part 1: {string.Join("", PartOne())}");
            Console.WriteLine($"Part 2: ");
        }

        private long[] PartOne()
        {
            for (int i = 0; i < 100; i++)
            {
                var max = _input.Max();
                var currentIndex = i % _input.Count();
                var current = _input[currentIndex];
                var destination = current - 1;

                var range = Enumerable.Range(1, 3).Select(x => long.Parse(_input[(x + currentIndex) % _input.Count()].ToString())).ToList();

                while (range.Contains(destination) || destination < 1)
                {
                    destination--;
                    if (destination < 1)
                    {
                        destination = max;
                    }
                }
                
                var pickedCups = range;
                range.ForEach(x => _input.Remove(x));

                var insertIndex = _input.IndexOf(destination);

                _input.InsertRange(insertIndex + 1, pickedCups);

                var displacement = _input.IndexOf(current) - currentIndex;

                _input = displacement != 0 
                    ? _input.ToArray()[displacement..].Concat(_input.ToArray()[..displacement]).ToList()
                    : _input;
            }

            var oneIndex = _input.IndexOf(1);
            return _input.ToArray()[(oneIndex+1)..].Concat(_input.ToArray()[..oneIndex]).ToArray();
        }

        private void PartTwo()
        {

        }
    }
}
