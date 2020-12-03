using System;
using System.IO;

namespace AdventOfCode2020
{
    public class Common
    {
        public static string[] ReadFile(string filename)
        {
            return File.ReadAllLines(@$"Inputs/{filename}");
        }

        public class Coord : Tuple<int, int>
        {
            public Coord(int X, int Y) : base(X, Y)
            {
            }
        }
    }
}
