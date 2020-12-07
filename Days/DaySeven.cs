using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using static AdventOfCode2020.Common;

namespace AdventOfCode2020.Days
{
    public class DaySeven
    {
        private Dictionary<string, List<BagContent>> _bags;
        public DaySeven()
        {
            _bags = new Dictionary<string, List<BagContent>>();
            var input = ReadFile("dayseven.txt").ToList();
            input.ForEach(Parse);
        }

        public void Process()
        {
            var bags = _bags.Where(x => x.Value.Select(x => x.Colour).Contains("shiny gold bag")).Select(x => x.Key).ToList();
            var total = bags;
            while (bags.Any())
            {
                var nextBags = _bags.Where(x => x.Value.Select(x => x.Colour).Intersect(bags).Count() > 0).ToList();
                bags = nextBags.Select(x => x.Key).ToList();
                total = total.Concat(bags).ToList();
            }
            total = total.Distinct().ToList();

            Console.WriteLine($"Part 1: {total.Count()}");

            var goldBag = _bags["shiny gold bag"];
            Console.WriteLine($"Part 2: {CalculateContentsCount(goldBag)}");
        }

        private int CalculateContentsCount(List<BagContent> contents)
        {
            return contents.Sum(x => x.Count) + contents.Sum(x => x.Colour == "no other bag" ? 0 : CalculateContentsCount(_bags[x.Colour]) * x.Count);
        }

        private void Parse(string line)
        {
            line = line.Replace("bags", "bag").Replace(".", "");
            var bagColour = line.Split(" contain ")[0];
            var bagContents = line.Split(" contain ")[1].Split(",");
            _bags.Add(bagColour, bagContents.Select(ParseContent).ToList());
        }

        private BagContent ParseContent(string contentString)
        {
            contentString = contentString.Trim();
            var allNumbers = Regex.Matches(contentString, @"\d+").Select(x => int.Parse(x.Value));
            var count = allNumbers.Count() == 0 ? 0 : allNumbers.First();
            var result = new BagContent
            {
                Count = count,
                Colour = Regex.Replace(contentString, @"\d", "").Trim()
            };
            return result;
        }

        public class BagContent
        {
            public int Count { get; set; }
            public string Colour { get; set; }
        }
    }
}
