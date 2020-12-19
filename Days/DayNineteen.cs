using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using static AdventOfCode2020.Common;

namespace AdventOfCode2020.Days
{
    public class DayNineteen
    {
        private readonly string[] _input;
        private readonly Dictionary<string, string> _data;

        public DayNineteen()
        {
            _input = ReadRaw("daynineteen.txt").Split("\n\n");
            _data = new Dictionary<string, string>();
            _input[0].Split("\n").ToList().ForEach(x => Parse(x));
        }

        public void Process()
        {
            Console.WriteLine($"Part 1 : {CountMatches()}");

            _data["8"] = "( 42 | 42 8 )";
            _data["11"] = "( 42 31 | 42 11 31 )";

            Console.WriteLine($"Part 2 : {CountMatches()}");
        }

        private int CountMatches()
        {
            var regex = $"^{GenerateRegex()}$";
            return  _input[1].Split("\n").Where(x => Regex.IsMatch(x, regex)).Count();
        }

        private string GenerateRegex()
        {
            var current = _data["0"].Split(" ").ToList();
            while (current.Any(x => x.Any(y => char.IsDigit(y))) && current.Count() < 100000)
            {
                current = current.Select(x => _data.ContainsKey(x) ? _data[x] : x).SelectMany(x => x.Split(" ")).ToList();
            }
            current.Remove("8");
            current.Remove("11");

            return string.Join("", current);
        }

        private void Parse(string line)
        {
            var colonDelimited = line.Split(":");
            var key = colonDelimited[0];

            var value = colonDelimited[1].Replace("\"","").Trim();
            if (value.Contains("|"))
            {
                var delimited = value.Split("|");
                value = $"( {delimited[0]} | {delimited[1]} )";
            }

            _data.Add(key, value);
        }
    }
}
