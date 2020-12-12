using System;
using System.Collections.Generic;
using System.Linq;
namespace AdventOfCode2020
{
    public class Challenge9 : IChallenge
    {
        public long RunFirst()
        {
            //var allLines = IChallenge.GetAllLines("test_9.txt");
            var allLines = IChallenge.GetAllLines("9_1.txt");
            var first = FindFirstInvalidXMAS(allLines.Select(long.Parse).ToArray(), 25);
            return long.Parse(allLines[first]);
        }

        private long FindFirstInvalidXMAS(long[] allLines, int preambleLength)
        {
            long index = -1;
            for (int i = preambleLength; i < allLines.Length; i++)
            {
                var foundMatch = false;
                for (int j = i - preambleLength; j < i-1; j++)
                {
                    for (int k = j + 1; k < i; k++)
                    {
                        if (allLines[j] + allLines[k] == allLines[i])
                        {
                            foundMatch = true;
                        }
                    }
                }

                if (!foundMatch)
                {
                    return i;
                }
            }

            return index;
        }

        public long RunSecond()
        {
            //var allLines = IChallenge.GetAllLines("test_9.txt").Select(long.Parse).ToArray();
            //var first = FindFirstInvalidXMAS(allLines, 5);
            
            var allLines = IChallenge.GetAllLines("9_1.txt").Select(long.Parse).ToArray();
            var first = FindFirstInvalidXMAS(allLines, 25);
            
            var value = allLines[first];

            var tuple = FindContignuousSequence(allLines, value);
            var segment = allLines.Skip(tuple.i).Take(tuple.j - tuple.i + 1).ToArray();
            //var segment = new ArraySegment<long>(allLines, tuple.i, tuple.j-1).ToArray();
            Array.Sort(segment);

            return segment[0]+segment[^1];
        }

        private static (int i, int j) FindContignuousSequence(long[] allLines, long value)
        {
            for (int i = 0; i < allLines.Length - 1; i++)
            {
                var sum = allLines[i];
                for (var j = i + 1; j < allLines.Length; j++)
                {
                    sum += allLines[j];
                    if (sum == value)
                    {
                        return (i, j);
                    }

                    if (sum > value)
                    {
                        break;
                    }
                }
            }

            return (-1, -1);
        }
    }
}