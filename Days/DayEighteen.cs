using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using static AdventOfCode2020.Common;

namespace AdventOfCode2020.Days
{
    public class DayEighteen
    {
        private readonly List<string> _input;
        private const string _bracketRegex = @"\([\d\s\-\+\/\*]+\)";
        private readonly List<string> _partTwoOperators;
        private readonly List<string> _operators;

        public DayEighteen()
        {
            _input = ReadFile("dayeighteen.txt").ToList();
            _operators = new List<string> { "+", "-", "*", "/" };
            _partTwoOperators = new List<string> { "+", "-" };
        }

        public void Process()
        {
            Console.WriteLine($"Part 1: {PartOne()}");
            Console.WriteLine($"Part 2: {PartTwo()}");
        }

        private long PartOne() => _input.Select(x => ProcessBrackets(x, false)).Select(x => Calculate(x, _operators)).Sum();

        private long PartTwo() => _input.Select(x => ProcessBrackets(x, true)).Select(x => Calculate(x, _partTwoOperators)).Sum();

        private long Calculate(string arithmetic, List<string> priorityOperators)
        {
            var delimited = arithmetic.Split(' ').ToList();
            var operatorIndices = Enumerable.Range(0, delimited.Count())
                .Where(i => priorityOperators.Contains(delimited[i]))
                .ToList();

            while (operatorIndices.Count() > 0)
            {
                var index = operatorIndices[0];
                var currResult = CalculateResult(delimited.GetRange(index - 1, 3)).ToString();

                delimited.RemoveRange(index - 1, 3);
                delimited.Insert(index - 1, currResult);

                operatorIndices = Enumerable.Range(0, delimited.Count())
                    .Where(i => priorityOperators.Contains(delimited[i]))
                    .ToList();
            }

            return _operators.Intersect(delimited).Count() == 0 
                ? long.Parse(delimited[0])
                : Calculate(string.Join(" ", delimited), _operators);
        }

        private long CalculateResult(List<string> arithmetic)
        {
            var lhs = long.Parse(arithmetic[0]);
            var rhs = long.Parse(arithmetic[2]);
            switch (arithmetic[1])
            {
                case "+": return lhs + rhs;
                case "-": return lhs - rhs;
                case "*": return lhs * rhs;
                case "/": return lhs / rhs;
                default: return 0;
            }
        }

        private string ProcessBrackets(string arithmetic, bool isPartTwo)
        {
            var regex = new Regex(_bracketRegex);

            while (Regex.IsMatch(arithmetic, _bracketRegex))
            {
                arithmetic = isPartTwo
                    ? regex.Replace(arithmetic, x => Calculate(x.Value[1..^1], _partTwoOperators).ToString())
                    : regex.Replace(arithmetic, x => Calculate(x.Value[1..^1], _operators).ToString());
            }

            return arithmetic;
        }
    }
}
