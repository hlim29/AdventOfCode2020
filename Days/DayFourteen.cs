using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using static AdventOfCode2020.Common;

namespace AdventOfCode2020.Days
{
    public class DayFourteen
    {
        private readonly List<string> _input;
        private readonly Dictionary<string, long> _addresses;
        private const string _maskRegex = @"mask = (?<mask>[X01]+)";
        private const string _memoryRegex = @"mem\[(?<address>\d+)\] = (?<value>(\d+))";

        public DayFourteen()
        {
            _input = ReadFile("dayfourteen.txt").ToList();
            _addresses = new Dictionary<string, long>();
        }

        public void Process()
        {
            Console.WriteLine($"Part 1: {Calculate(false)}");
            _addresses.Clear();
            Console.WriteLine($"Part 2: {Calculate(true)}");
        }

        public long Calculate(bool isPartTwo)
        {
            string bitmask = _input[0].Split(" ")[^1];

            foreach (var line in _input.Skip(1))
            {
                if (Regex.IsMatch(line, _maskRegex))
                {
                    bitmask = line.Split(" ")[^1];
                    continue;
                }

                var matches = Regex.Match(line, _memoryRegex).Groups;

                var value = isPartTwo 
                    ? Convert.ToString(long.Parse(matches["address"].Value), 2).PadLeft(bitmask.Length, '0')
                    : Convert.ToString(long.Parse(matches["value"].Value), 2).PadLeft(bitmask.Length, '0');

                var maskAndValues = bitmask.Zip(value, (x, y) => (Mask: x, Value: y));
                var result = string.Empty;
                foreach (var (Mask, Value) in maskAndValues)
                {
                    if (isPartTwo)
                    {
                        result += Mask != '0' ? Mask : Value;
                    }
                    else
                    {
                        result += Mask != 'X' ? Mask : Value;
                    }
                }

                var addresses = isPartTwo
                    ? GenerateCombinations(result).Select(x => Convert.ToInt64(x, 2).ToString()).ToList()
                    : new List<string> { matches["address"].Value };

                foreach (var address in addresses)
                {
                    _addresses[address] =  isPartTwo ? long.Parse(matches["value"].Value) : Convert.ToInt64(result, 2);
                }
            }

            return _addresses.Keys.Sum(x => _addresses[x]);
        }

        private static List<string> GenerateCombinations(string value)
        {
            if (!value.Any(c => c.Equals('X')))
            {
                return new List<string> { value };
            }
            else
            {
                var zeroMask = ReplaceFirstMatch(value, "X", "0");
                var oneMask = ReplaceFirstMatch(value, "X", "1");
                return GenerateCombinations(zeroMask).Concat(GenerateCombinations(oneMask)).ToList();
            }
        }

        private static string ReplaceFirstMatch(string value, string mask, string replacement)
        {
            var firstMaskIndex = value.IndexOf(mask);
            if (firstMaskIndex < 0)
            {
                return value;
            }
            return value.Remove(firstMaskIndex, 1).Insert(firstMaskIndex, replacement);
        }
    }
}
