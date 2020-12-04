using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2020.Days
{
    public class DayFour
    {
        private readonly string[] _input;
        private readonly string[] _compulsory;
        private readonly string[] _validEyeColours;

        public DayFour()
        {
            _compulsory = new string[] { "byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid", "cid" };
            _validEyeColours = new string[] { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" };
            _input = File.ReadAllText("Inputs/dayfour.txt").Split("\n\n").Select(x => x.Replace("\n", " ")).ToArray();
        }

        public void Process()
        {
            var passports = _input.Select(x => Convert(x)).ToList();
            Console.WriteLine($"Part 1: {passports.Count(x => IsValid(x))}");
            Console.WriteLine($"Part 2: {passports.Count(x => IsValidPartTwo(x))}");
        }

        private Dictionary<string, string> Convert(string input)
        {
            var output = new Dictionary<string, string>();
            var spaceDelimited = input.Trim().Split(" ").ToList();

            foreach (var passport in spaceDelimited)
            {
                if (string.IsNullOrWhiteSpace(passport))
                {
                    continue;
                }
                var colonDelimited = passport.Split(":");
                output[colonDelimited[0]] = colonDelimited[1];
            }

            return output;
        }

        private bool IsValid(Dictionary<string, string> passport)
        {
            var keys = passport.Keys;
            var length = passport.ContainsKey("cid") ? _compulsory.Length : _compulsory.Length - 1;
            return _compulsory.Intersect(keys).Count() == length;
        }

        private bool IsValidPartTwo(Dictionary<string, string> passport)
        {
            return IsValid(passport) 
                && ValidateHeight(passport) 
                && int.Parse(passport["byr"]) >= 1920 && int.Parse(passport["byr"]) <= 2002
                && int.Parse(passport["iyr"]) >= 2010 && int.Parse(passport["iyr"]) <= 2020
                && int.Parse(passport["eyr"]) >= 2020 && int.Parse(passport["eyr"]) <= 2030
                && _validEyeColours.Contains(passport["ecl"])
                && passport["pid"].Length == 9 && long.TryParse(passport["pid"], out var _)
                && passport["hcl"].StartsWith("#") && passport["hcl"].Length == 7 && passport["hcl"].All(c => "#0123456789abcdefABCDEF".Contains(c));
        }

        private bool ValidateHeight(Dictionary<string, string> passport)
        {
            var height = passport["hgt"];
            if (height.EndsWith("cm"))
            {
                var cmHeight = int.Parse(height.Replace("cm", ""));
                return cmHeight >= 150 && cmHeight <= 193;
            }
            else if (height.EndsWith("in"))
            {
                var usHeight = int.Parse(height.Replace("in", ""));
                return usHeight >= 59 && usHeight <= 76;
            }
            else
            {
                return false;
            }
        }
    }
}
