using AdventOfCode2020.Days;
using System;

namespace AdventOfCode
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Advent of Code 2020");
            var input = -1;
            while (input == -1)
            {
                input = AskForDay();
                switch (input)
                {
                    case 1:
                        var report = new DayOne();
                        report.Process();
                        break;
                    case 2:
                        var password = new DayTwo();
                        password.Process();
                        break;
                    case 3:
                        var trajectory = new DayThree();
                        trajectory.Process();
                        break;
                    case 4:
                        var passport = new DayFour();
                        passport.Process();
                        break;
                    case 5:
                        var boarding = new DayFive();
                        boarding.Process();
                        break;
                    case 6:
                        var form = new DaySix();
                        form.Process();
                        break;
                    case 7:
                        var bags = new DaySeven();
                        bags.Process();
                        break;
                    case 8:
                        var assembly = new DayEight();
                        assembly.Process();
                        break;
                    case 9:
                        var encoding = new DayNine();
                        encoding.Process();
                        break;
                    case 10:
                        var plugs = new DayTen();
                        plugs.Process();
                        break;
                    case 11:
                        var seats = new DayEleven();
                        seats.Process();
                        break;
                    case 12:
                        var ship = new DayTwelve();
                        ship.Process();
                        break;
                    case 13:
                        var bus = new DayThirteen();
                        bus.Process();
                        break;
                    case 14:
                        var bitmask = new DayFourteen();
                        bitmask.Process();
                        break;
                    case 15:
                        var spoken = new DayFifteen();
                        spoken.Process();
                        break;
                    case 16:
                        var sixteen = new DaySixteen();
                        sixteen.Process();
                        break;
                    case 17:
                        var seventeen = new DaySeventeen();
                        seventeen.Process();
                        break;
                    case 18:
                        var eighteen = new DayEighteen();
                        eighteen.Process();
                        break;
                    case 19:
                        var regex = new DayNineteen();
                        regex.Process();
                        break;
                    case 20:
                        var twenty = new DayTwenty();
                        twenty.Process();
                        break;
                    case 21:
                        var allergens = new DayTwentyone();
                        allergens.Process();
                        break;
                    case 22:
                        var cards = new DayTwentytwo();
                        cards.Process();
                        break;
                    case 23:
                        var twentythree = new DayTwentythree();
                        twentythree.Process();
                        break;
                    case int n when n > 19 && n <= 25:
                        Console.WriteLine("Coming soon");
                        break;
                    case 0:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid day. Please try again.");
                        break;
                }
                input = -1;
            }
        }

        static int AskForDay()
        {
            Console.WriteLine("Please enter in a day (1-25), 0 to exit:");
            return ParseInput(Console.ReadLine());
        }

        static int ParseInput(string input)
        {
            var isValid = int.TryParse(input, out var number);
            if (!isValid)
                return -1;
            return number;
        }
    }
}
