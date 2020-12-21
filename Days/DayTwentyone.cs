using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static AdventOfCode2020.Common;

namespace AdventOfCode2020.Days
{
    public class DayTwentyone
    {
        private readonly List<(string[] Allergens, List<string> Ingredients)> _allergens;
        private Dictionary<string, List<string>> _foodsWithAllergens;

        public DayTwentyone()
        {
            _allergens = new List<(string[], List<string>)>();
            _foodsWithAllergens = new Dictionary<string, List<string>>();
            var input = ReadFile("daytwentyone.txt").ToList();

            _allergens = input.Select(x => 
            (Allergens: x.Replace(")","").Split(" (contains ")[1].Replace(" ", "").Split(","),
            Ingredients: x.Split("(contains")[0].Trim().Split(" ").ToList())).ToList();
        }

        public void Process()
        {
            Console.WriteLine($"Part 1: {PartOne()}");
            Console.WriteLine($"Part 2: {PartTwo()}");
        }

        public int PartOne()
        {
            var allAllergens = _allergens.SelectMany(x => x.Allergens).Distinct();
            foreach (var allergen in allAllergens)
            {
                var matchingAllergens = _allergens.Where(x => x.Allergens.Contains(allergen)).Select(x => x.Ingredients).ToList();
                _foodsWithAllergens.Add(allergen, matchingAllergens.Aggregate((x, y) => x.Intersect(y).ToList()));
            }
            
            return _allergens.SelectMany(x => x.Ingredients).Where(x => !_foodsWithAllergens.SelectMany(x => x.Value).Distinct().Contains(x)).Count();
        }

        public string PartTwo()
        {
            while (_foodsWithAllergens.Values.Any(x => x.Count() != 1))
            {
                var singles = _foodsWithAllergens.Where(x => x.Value.Count() == 1).SelectMany(x => x.Value).ToList();
                var nonSingleKeys = _foodsWithAllergens.Where(x => x.Value.Count() > 1).Select(x => x.Key).ToList();
                nonSingleKeys.ForEach(x => _foodsWithAllergens[x] = _foodsWithAllergens[x].Where(x => !singles.Contains(x)).ToList());
            }

            return string.Join(",", _foodsWithAllergens.OrderBy(x => x.Key).SelectMany(x => x.Value));
        }
    }
}
