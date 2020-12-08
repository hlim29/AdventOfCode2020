using System;
using System.Collections.Generic;
using System.Linq;
using static AdventOfCode2020.Common;

namespace AdventOfCode2020.Days
{
    public class DayEight
    {
        private readonly List<string> _input;

        public DayEight()
        {
            _input = ReadFile("dayeight.txt").ToList();
        }

        public void Process()
        {
            Console.WriteLine($"Part 1: {GetLastInstruction(_input).Total}");
            Console.WriteLine($"Part 2: {GetFixedAccValue()}");
        }

        public int GetFixedAccValue()
        {
            var tempInput = _input;

            for (int i = 0; i < _input.Count(); i++)
            {
                if (tempInput[i].Contains("jmp") || tempInput[i].Contains("nop"))
                {
                    tempInput[i] = SwitchJmpNop(tempInput[i]);
                    if (GetLastInstruction(tempInput).Last == tempInput.Count() - 1)
                    {
                        return GetLastInstruction(tempInput).Total;
                    }
                    tempInput[i] = SwitchJmpNop(tempInput[i]);
                }
            }
            return -1;
        }

        private static string SwitchJmpNop(string value)
        {
            if (value.Contains("jmp")) return value.Replace("jmp", "nop");
            if (value.Contains("nop")) return value.Replace("nop", "jmp");
            return value;
        }

        private static (int Total, int Last) GetLastInstruction(List<string> input)
        {
            var visited = new HashSet<int>();
            var total = 0;
            for (var i = 0; i < input.Count(); i++)
            {
                if (visited.Contains(i))
                {
                    break;
                }
                visited.Add(i);
                var splittedInstruction = input[i].Split(" ");
                if (splittedInstruction[0] == "jmp")
                {
                    i += int.Parse(splittedInstruction[1]) - 1;
                    continue;
                }
                else if (splittedInstruction[0] == "acc")
                {
                    total += int.Parse(splittedInstruction[1]);
                }
            }
            return (total, visited.Max());
        }
    }
}
