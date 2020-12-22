using System;
using System.Collections.Generic;
using System.Linq;
using static AdventOfCode2020.Common;

namespace AdventOfCode2020.Days
{
    public class DayTwentytwo
    {
        private readonly List<string> _input;
        private Dictionary<string, List<int>> _decks;

        public DayTwentytwo()
        {
            _input = ReadRaw("daytwentytwo.txt").Split("\n\n").ToList();
            InitDecks();
        }

        public void Process()
        {
            Console.WriteLine($"Part 1: {PartOne(_decks).Sum}");
            InitDecks();
            Console.WriteLine($"Part 2: {PartTwo(_decks).Sum}");
        }

        public (string Winner, int Sum) PartOne(Dictionary<string, List<int>> decks)
        {
            var players = decks.Keys.ToList();
            while (players.Select(x => decks[x]).All(x => x.Count() > 0))
            {
                var card1 = decks["Player 1"].First();
                var card2 = decks["Player 2"].First();
                decks["Player 2"].RemoveAt(0);
                decks["Player 1"].RemoveAt(0);

                if (card1 > card2)
                {
                    decks["Player 1"].AddRange(GetNextCards(card1, card2, "Player 1"));
                }
                else
                {
                    decks["Player 2"].AddRange(GetNextCards(card1, card2, "Player 2"));
                }
            }
            var winner = decks.First(x => x.Value.Count() > 0).Key;
            decks[winner].Reverse();
            var output = decks[winner].Select((x, i) => x * (i + 1)).Sum();
            return (winner, output);
        }

        public (string Winner, int Sum) PartTwo(Dictionary<string, List<int>> decks)
        {
            var players = decks.Keys.ToList();
            var p1History = new List<string>();
            var p2History = new List<string>();

            while (players.Select(x => decks[x]).All(x => x.Count() > 0))
            {
                var player1Cards = string.Join(",", decks["Player 1"]);
                var player2Cards = string.Join(",", decks["Player 2"]);

                if (p1History.Contains(player1Cards) || p2History.Contains(player2Cards))
                {
                    return ("Player 1", 0);
                }

                p1History.Add(player1Cards);
                p2History.Add(player2Cards);

                var card1 = decks["Player 1"].First();
                var card2 = decks["Player 2"].First();
                decks["Player 2"].RemoveAt(0);
                decks["Player 1"].RemoveAt(0);

                if (card1 <= decks["Player 1"].Count() && card2 <= decks["Player 2"].Count())
                {
                    var newDeck = new Dictionary<string, List<int>>()
                    {
                        {"Player 1", decks["Player 1"].GetRange(0, card1) },
                        {"Player 2", decks["Player 2"].GetRange(0, card2) }
                    };
                    var innerWinner = PartTwo(newDeck).Winner;
                    var nextCards = GetNextCards(card1, card2, innerWinner);

                    if (innerWinner == "Player 1")
                    {
                        decks["Player 1"].AddRange(nextCards);
                    }
                    else
                    {
                        decks["Player 2"].AddRange(nextCards);
                    }
                    continue;
                }

                if (card1 > card2)
                {
                    decks["Player 1"].AddRange(GetNextCards(card1, card2, "Player 1"));
                }
                else
                {
                    decks["Player 2"].AddRange(GetNextCards(card1, card2, "Player 2"));
                }

            }
            var winner = decks.First(x => x.Value.Count() > 0).Key;
            decks[winner].Reverse();
            var output = decks[winner].Select((x, i) => x * (i + 1)).Sum();
            return (winner, output);
        }

        private List<int> GetNextCards(int card1, int card2, string winner)
        {
            return (winner == "Player 1")
                ? new List<int> { card1, card2 }
                : new List<int> { card2, card1 };
        }

        private void InitDecks()
        {
            _decks = _input.Select(x => (Player: x.Split("\n")[0].Replace(":", ""), Cards: x.Split("\n")[1..].Select(int.Parse).ToList())).ToDictionary(x => x.Player, x => x.Cards);
        }
    }
}
