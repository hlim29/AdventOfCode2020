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

        public DayFour()
        {
            _compulsory = new string[] { "byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid", "cid" };
            _input = File.ReadAllText("Inputs/dayfour.txt").Split("\n\n").Select(x => x.Replace("\n", " ")).ToArray();
        }

        public bool IsValid(Dictionary<string, string> passport)
        {
            var keys = passport.Keys;
            var length = passport.ContainsKey("cid") ? _compulsory.Length : _compulsory.Length - 1;
            return _compulsory.Intersect(keys).Count() == length;
        }

        public bool IsValidPartTwo(Dictionary<string, string> passport)
        {
            var result = IsValid(passport) && ValidateBirthYear(passport) && ValidateIssueYear(passport)
                && ValidateExpYear(passport) && ValidateHeight(passport) && ValidateHairColour(passport) 
                && ValidateEyeColour(passport) && ValidatePassportNo(passport);
            return result;
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

            foreach(var file in spaceDelimited)
            {
                if (string.IsNullOrWhiteSpace(file))
                {
                    continue;
                }
                var colonDelimited = file.Split(":");
                output[colonDelimited[0]] = colonDelimited[1];
            }

            return output;
        }

        private bool ValidateBirthYear(Dictionary<string, string> passport)
        {
            var year = passport["byr"];
            return year.Length == 4 && int.Parse(year) >= 1920 && int.Parse(year) <= 2002;
        }

        private bool ValidateIssueYear(Dictionary<string, string> passport)
        {
            var year = passport["iyr"];
            return year.Length == 4 && int.Parse(year) >= 2010 && int.Parse(year) <= 2020;
        }

        private bool ValidateExpYear(Dictionary<string, string> passport)
        {
            var year = passport["eyr"];
            return year.Length == 4 && int.Parse(year) >= 2020 && int.Parse(year) <= 2030;
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

        private bool ValidateHairColour(Dictionary<string, string> passport)
        {
            var hairColour = passport["hcl"];
            var hairColourValue = hairColour[1..];
            return hairColour.StartsWith("#") && hairColourValue.Length == 6 && hairColourValue.All(c => "0123456789abcdefABCDEF".Contains(c));
        }


        private bool ValidateEyeColour(Dictionary<string, string> passport)
        {
            var eyeColour = passport["ecl"];
            var validEyeColours = new string[] { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" };
            return validEyeColours.Contains(eyeColour);
        }

        private bool ValidatePassportNo(Dictionary<string, string> passport)
        {
            var passportNo = passport["pid"];
            return passportNo.Length == 9 && long.TryParse(passportNo, out var _);
        }
    }
}
