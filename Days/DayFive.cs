using System;
using System.Linq;
using static AdventOfCode2020.Common;

namespace AdventOfCode2020.Days
{
    public class DayFive
    {
        private readonly string[] _input;
        private readonly int[] _availableColumns;

        public DayFive()
        {
            _input = ReadFile("dayfive.txt");
            _availableColumns = new int[] { 0, 1, 2, 3, 4, 5, 6, 7 };
        }

        public void Process()
        {
            var seatIds = _input.Select(x => GetRow(x) * 8 + GetColumn(x));
            Console.WriteLine($"Part 1: {seatIds.Max()}");

            var seats = _input.Select(x => (Row: GetRow(x), Column: GetColumn(x)));

            var rowWithMySeat = seats.GroupBy(x => x.Row).Where(row => row.Count() == 7).SelectMany(x => x);
            var occupiedColumns = rowWithMySeat.Select(x => x.Column);
            var column = _availableColumns.First(x => !occupiedColumns.Contains(x));
            var row = rowWithMySeat.First().Row;

            Console.WriteLine($"Part 2: {row * 8 + column}");
        }

        private int GetRow(string input)
        {
            var binary = input[..^3].Replace("F", "0").Replace("B", "1");
            return Convert.ToInt32(binary, 2);
        }

        private int GetColumn(string input)
        {
            var binary = input[^3..].Replace("L", "0").Replace("R", "1");
            return Convert.ToInt32(binary, 2);
        }
    }
}
