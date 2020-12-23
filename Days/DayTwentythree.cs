using System;
using System.Collections.Generic;
using System.Linq;
using static AdventOfCode2020.Common;

namespace AdventOfCode2020.Days
{
    public class DayTwentythree
    {
        private long[] _input;

        public DayTwentythree()
        {
            _input = ReadRaw("daytwentythree.txt").Select(x => long.Parse(x.ToString())).ToArray();
        }

        public void Process()
        {
            Console.WriteLine(string.Join("", PartOne()));
            
            Console.WriteLine($"Part 1: ");
            Console.WriteLine($"Part 2: ");
            PartTwo();
        }

        private long[] PartOne()
        {
            var current = _input[0];
            for (int i = 0; i < 100; i++)
            {
                var max = _input.Max();
                var destination = current.Value - 1;
                var current2 = current;

                var range = new List<LinkedListNode<long>>();

                Enumerable.Range(0, 3).ToList().ForEach(x => {
                    current2 = GetNext(current2);
                    range.Add(current2);
                    range.Remove(current2);
                });

                var rangeValues = range.Select(x => x.Value);

                while (rangeValues.Contains(destination) || destination < 1)
                {
                    destination--;
                    if (destination < 1)
                    {
                        destination = max;
                    }
                }
                
                //range.ForEach(x => _input.Remove(x));
                range.Reverse();
                var dest = _input.Find(destination);

                range.ForEach(x => _input.AddAfter(dest, x));

                current = GetNext(current);
            }

            var oneIndex = _input.Find(1);
            var result = new List<long>();

            var currentNode = GetNext(oneIndex);
            while (currentNode.Value != 1)
            {
                result.Add(currentNode.Value);
                currentNode = GetNext(currentNode);
            }
            return result.ToArray();// _input.ToArray()[(oneIndex+1)..].Concat(_input.ToArray()[..oneIndex]).ToArray();
        }

        private LinkedListNode<long> GetNext(LinkedListNode<long> current)
        {
            return current.Next ?? _input.First;
        }

        private int GetIndex(long value)
        {
            var count = 0;
            for (var node = _input.First; node != null; node = node.Next, count++)
            {
                if (_input.Equals(node.Value))
                    return count;
            }
            return -1;
        }

        private void PartTwo()
        {
            _input = new LinkedList<long>();
            ReadRaw("daytwentythree.txt").Select(x => long.Parse(x.ToString())).ToList().ForEach(x => _input.AddLast(x));

            var max = _input.Max();

            while (max < 1000000)
            {
                max++;
                _input.AddLast(max);
            }
            max = 1000000;

            for (int i = 0; i < 100000; i++)
            {
                PartOne();
            }

            var e = PartOne();
        }
    }
}
