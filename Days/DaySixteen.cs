using System;
using System.Collections.Generic;
using System.Linq;
using static AdventOfCode2020.Common;

namespace AdventOfCode2020.Days
{
    public class DaySixteen
    {
        private readonly List<string> _input;
        private readonly List<long> _myTicket;
        private readonly List< List<int>> _nearbyTickets;
        private readonly Dictionary<string, (int Min, int Max)[]> _criteria;

        public DaySixteen()
        {
            _criteria = new Dictionary<string, (int Min, int Max)[]>();
            _input = ReadRaw("daysixteen.txt").Split("\n\n").ToList();
            _input[0].Split("\n").ToList().ForEach(x => ProcessCriteria(x));
            _myTicket = _input[1].Split("\n")[1].Split(",").Select(long.Parse).ToList();
            _nearbyTickets = _input[2].Split("\n")[1..].Select(x => x.Split(",")).Select(x => x.Select(y => int.Parse(y)).ToList()).ToList();
        }

        public void Process()
        {
            Console.WriteLine($"Part 1: {GetInvalid()}");
            Console.WriteLine($"Part 2: {GetDepartureSum()}");
        }

        private void ProcessCriteria(string line)
        {
            var delimited = line.Split(" ");
            var key = string.Join(" ", delimited[..^3]);
            var value = new (int, int)[] { ProcessLimits(delimited[^1]), ProcessLimits(delimited[^3]) };
            _criteria.Add(key, value);
        }

        private bool IsValid(List<(int Min, int Max)> allCriterion, List<int> ticket)
        {
            return ticket.All(x => allCriterion.ToList().Any(y => x >= y.Min && x <= y.Max));
        }

        private long GetDepartureSum()
        {
            var usedCriteria = new HashSet<string>();
            var result = new List<int>();

            var allCriterion = _criteria.SelectMany(x => x.Value).ToList();
            var validTickets = _nearbyTickets.Where(x => IsValid(allCriterion, x)).ToList();
            var transposedTickets = Transpose(validTickets);

            var matchingFields = transposedTickets.Select((a, i) => (Fields: _criteria.Keys.Where(x => AllNumbersMatch(x, a)), Index: i)).OrderBy(x => x.Fields.Count()).ToList();

            foreach(var _ in transposedTickets)
            {
                var firstMatch = matchingFields.First();

                if (firstMatch.Fields.First().Contains("depart"))
                {
                    result.Add(firstMatch.Index);
                }

                usedCriteria.Add(firstMatch.Fields.First());
                matchingFields = matchingFields.Select(x => (Fields: x.Fields.Where(y => !usedCriteria.Contains(y)), x.Index)).Where(x => x.Fields.Count() > 0).OrderBy(x => x.Fields.Count()).ToList();
            }

            return result.Select(x => _myTicket[x]).Aggregate((x,y) => x * y);
        }

        private bool AllNumbersMatch(string key, List<int> tickets) => tickets.All(x => _criteria[key].Any(y => x >= y.Min && x <= y.Max));

        private List<List<int>> Transpose(List<List<int>> input)
        {
            var result = new List<List<int>>();
            for (int i = 0; i < input[0].Count(); i++)
            {
                result.Add(input.Select(x => x[i]).ToList());
            }
            return result;
        }

        private int GetInvalid()
        {
            var result = 0;
            var criteriaFlat = _criteria.SelectMany(x => x.Value);
            foreach (var ticket in _nearbyTickets)
            {
                foreach (var value in ticket)
                {
                    if (!criteriaFlat.Any(x => value >= x.Min && value <= x.Max))
                        result += value;
                }
                
            }
            return result;
        }

        private (int Min, int Max) ProcessLimits(string input)
        {
            var delimited = input.Split("-");
            return (int.Parse(delimited[0]), int.Parse(delimited[1]));
        }
    }
}
