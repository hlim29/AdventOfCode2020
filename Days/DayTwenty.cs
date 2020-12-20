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
        private HashSet<string> _visited;

        public DayTwenty()
        {
            _input = ReadRaw("daytwenty.txt").Split("\n\n").ToList();
            _maps = _input.Select(x => (Tile: x.Split("\n")[0], Grid: x.Split("\n")[1..].ToList())).ToDictionary(x => x.Tile, x => new Map(x.Grid));
            _visited = new HashSet<string>();
        }

        public void Process()
        {
            PartOne();
            Console.WriteLine($"Part 1: {PartOne()}");
            Console.WriteLine($"Part 2: ");
            PartTwo("Tile 1951:");
            PartTwo("Tile 2971:");
        }

        private string PartOne()
        {
            var keys = _maps.Keys.ToList();
            foreach(var key in keys)
            {
                GetBorder(_maps[key]);
            }

            var values = _maps.Values.ToList();
            var borders = values.SelectMany(x => x.Borders).ToList();
            var reversedBorders = borders.Select(x => string.Join("", x.Reverse())).ToList();

            borders.RemoveAll(x => borders.Intersect(reversedBorders).Contains(x));

            var nonMatches = borders.GroupBy(x => x).Where(x => x.Count() == 1).Select(x => x.Key);
            var corners = _maps.Where(z => z.Value.Borders.Intersect(nonMatches).Count() > 1);

            return corners.Select(x => string.Join("", x.Key.Where(y => char.IsDigit(y)))).Aggregate((x, y) => (long.Parse(x) * long.Parse(y)).ToString());
        }

        public void PartTwo(string currentTileKey)
        {
            var borders = _maps.Values.Select(x => x.Borders);
            var currentTile = _maps[currentTileKey];
            var e= _maps.Where(x => x.Value.Borders.Contains(currentTile.RightBorder) && x.Key  != currentTileKey);
            var f= _maps.Values.Where(x => x.Borders.Select(x => string.Join("", x.Reverse()))
            .Contains(currentTile.RightBorder) && x.RightBorder != currentTile.RightBorder);

            if (e.Count() == 1 || f.Count() == 1)
            {
                var match = e.First().Value;
                _visited.Add(e.First().Key);

                if (match.Borders.Contains(currentTile.RightBorder))
                {
                    if (match.LeftBorder == currentTile.RightBorder)
                    {
                        currentTile.NeighbourRight = e.First().Key;

                    }
                    if (match.TopBorder == currentTile.RightBorder) //rotate 90 here
                    {
                        currentTile.NeighbourRight = e.First().Key;
                    }
                    if (match.BottomBorder == currentTile.RightBorder) //rotate 90 here
                    {
                        currentTile.NeighbourRight = e.First().Key;
                    }
                }
               
                
            }

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
        public string NeighbourTop { get; set; }
        public string NeighbourBottom { get; set; }
        public string NeighbourLeft { get; set; }
        public string NeighbourRight { get; set; }
        public bool IsFlipped { get; set; }
        public int RotationDegrees { get; set; }
    }
}
