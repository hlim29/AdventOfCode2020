using System;
using System.Collections.Generic;
using System.Linq;
using static AdventOfCode2020.Common;

namespace AdventOfCode2020.Days
{
    public class DayTwenty
    {
        private readonly List<string> _input;
        private readonly Dictionary<string, Map> _maps;

        public DayTwenty()
        {
            _input = ReadRaw("daytwenty.txt").Split("\n\n").ToList();
            _maps = _input.Select(x => (Tile: x.Split("\n")[0], Grid: x.Split("\n")[1..].ToList())).ToDictionary(x => x.Tile, x => new Map(x.Grid));
        }

        public void Process()
        {
            PartOne();
            Console.WriteLine($"Part 1: {PartOne()}");
            Console.WriteLine($"Part 2: ");
        }

        private string PartOne()
        {
            var keys = _maps.Keys.ToList();
            keys.ForEach(x => GetBorder(_maps[x]));

            var borders = _maps.Values.SelectMany(x => x.Borders).ToList();
            var reversedBorders = borders.Select(x => string.Join("", x.Reverse())).ToList();

            borders.RemoveAll(x => borders.Intersect(reversedBorders).Contains(x));

            var nonMatches = borders.GroupBy(x => x).Where(x => x.Count() == 1).Select(x => x.Key);
            var corners = _maps.Where(z => z.Value.Borders.Intersect(nonMatches).Count() > 1);
            return corners.Select(x => string.Join("", x.Key.Where(y => char.IsDigit(y)))).Aggregate((x, y) => (long.Parse(x) * long.Parse(y)).ToString());
        }

        private void GetBorder(Map map)
        {
            map.TopBorder = map.Grid.First();
            map.BottomBorder = map.Grid.Last();
            map.LeftBorder = string.Join("", map.Grid.Select(x => x[0]));
            map.RightBorder = string.Join("", map.Grid.Select(x => x[^1]));
            map.Borders = new List<string> { map.Grid.First(), map.Grid.Last(), string.Join("", map.Grid.Select(x => x[0])), string.Join("", map.Grid.Select(x => x[^1])) };
        }
    }

    public class Map
    {
        public List<string> Grid { get; set; }
        public string TopBorder { get; set; }
        public string BottomBorder { get; set; }
        public string LeftBorder { get; set; }
        public string RightBorder { get; set; }
        public List<string> Borders { get; set; }

        public Map(List<string> grid)
        {
            Grid = grid;
        }
    }
}
