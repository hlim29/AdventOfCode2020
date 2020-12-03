using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode2020
{
    public class Common
    {
        public static List<string> ReadFile(string filename)
        {
            return File.ReadAllText(@$"Inputs/{filename}").Split("\n").Where(x => !string.IsNullOrEmpty(x)).ToList();
        }

        public class Coord : Tuple<int, int>
        {
            public Coord(int X, int Y) : base(X, Y)
            {
            }
        }
    }
}
