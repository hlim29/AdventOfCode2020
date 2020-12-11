using System.IO;

namespace AdventOfCode2020
{
    public class Common
    {
        public static string[] ReadFile(string filename)
        {
            return File.ReadAllLines(@$"Inputs/{filename}");
        }
        public static string ReadRaw(string filename)
        {
            return File.ReadAllText(@$"Inputs/{filename}");
        }

        public class Square
        {
            public int X { get; set; }
            public int Y { get; set; }

            public Square(int x, int y)
            {
                X = x;
                Y = y;
            }

            public static Square operator +(Square a, Square b) => new Square(a.X + b.X, a.Y + b.Y);
            public static Square operator -(Square a, Square b) => new Square(a.X - b.X, a.Y - b.Y);
            public static Square operator *(Square a, Square b) => new Square(a.X * b.X, a.Y * b.Y);
            public static bool operator ==(Square a, Square b) => a.X == b.X && a.Y == b.Y;
            public static bool operator !=(Square a, Square b) => a.X != b.X || a.Y != b.Y;

            public override bool Equals(object obj)
            {
                if (obj == null || GetType() != obj.GetType())
                    return false;
                
                var other = (Square)obj;
                return other.X == X && other.Y == Y;
            }

            public override string ToString()
            {
                return $"{X}, {Y}";
            }
        }
    }
}
