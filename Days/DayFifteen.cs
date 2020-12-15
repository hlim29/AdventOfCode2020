using System;
using System.Collections.Generic;
using System.Linq;
using static AdventOfCode2020.Common;

namespace AdventOfCode2020.Days
{
    public class DayFifteen
    {
        private int _length;
        private long _lastValue;
        private readonly Dictionary<long, (long? SecondLast, long Last)> _dictNumbers;

        public DayFifteen()
        {
            var input = ReadRaw("dayfifteen.txt").Split(',').ToList();
            _dictNumbers = new Dictionary<long, (long? SecondLast, long Last)>();

            List<(int Index, long Value)> numbers = input.Select((x, i) => (i + 1, long.Parse(x))).ToList();
            foreach (var item in numbers)
            {
                if (!_dictNumbers.ContainsKey(item.Value))
                    _dictNumbers[item.Value] = (null, item.Index);
                else 
                    _dictNumbers[item.Value] = (_dictNumbers[item.Value].Last, item.Index);
            }
            _length = numbers.Count();
            _lastValue = numbers[^1].Value;
        }

        public void Process()
        {
            Console.WriteLine($"Part 1: {Calculate(2020)}");
            Console.WriteLine($"Part 2: {Calculate(30000000)}");
        }

        public long Calculate(int target)
        {
            while (_length < target)
            {
                _length++;
                var matches = _dictNumbers[_lastValue];
                _lastValue = matches.SecondLast == null ? 0 : matches.Last - matches.SecondLast.Value;

                if (_dictNumbers.ContainsKey(_lastValue))
                    _dictNumbers[_lastValue] = (_dictNumbers[_lastValue].Last, _length);
                else
                    _dictNumbers[_lastValue] = (null, _length) ;
            }
            return _lastValue;
        }
    }
}
