using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Days
{
    public class DayTwo
    {
        private readonly List<Password> _passwords;
        public DayTwo()
        {
            var input = Common.ReadFile("daytwo.txt");
            _passwords = input.Select(x => ProcessPassword(x)).ToList();
        }

        public void Process()
        {
            var count = _passwords.Count(x => Validate(x));
            var countPartTwo = _passwords.Count(x => ValidatePartTwo(x));

            Console.WriteLine($"Part 1: {count}");
            Console.WriteLine($"Part 2: {countPartTwo}");
        }

        private bool Validate(Password password)
        {
            var letterCounts = password.PasswordString.GroupBy(x => x).ToDictionary(g => g.Key, g => g.Count());
            if (!letterCounts.TryGetValue(password.Letter, out var item))
                return false;
            return letterCounts[password.Letter] >= password.Lower && letterCounts[password.Letter] <= password.Upper;
        }

        private bool ValidatePartTwo(Password password)
        {
            return password.PasswordString[password.Upper-1] == password.Letter ^ password.PasswordString[password.Lower - 1] == password.Letter;
        }

        private Password ProcessPassword(string input)
        {
            var spaceDelimited = input.Split(' ');

            return new Password
            {
                Lower = int.Parse(spaceDelimited[0].Split('-')[0]),
                Upper = int.Parse(spaceDelimited[0].Split('-')[1]),
                Letter = spaceDelimited[1][0],
                PasswordString = spaceDelimited[2]
            };
        }

        public class Password
        {
            public int Lower { get; set; }
            public int Upper { get; set; }
            public char Letter { get; set; }
            public string PasswordString { get; set; }
        }
    }
}
