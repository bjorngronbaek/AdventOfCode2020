using System;
using System.IO;
using System.Linq;

namespace AdventOfCode2020
{
    public class Challenge2
    {
        public int RunFirst()
        {
            var validCounter = 0;
            var allLines = File.ReadAllLines("challenges/2_1.txt");
            foreach (var line in allLines)
            {
                var split = line.Split(':');
                var policy = ParsePolicy(split[0]);

                var charCount = split[1].Count(character => character == policy.Item3);
                if (policy.Item1 <= charCount && charCount <= policy.Item2) validCounter++;
            }

            return validCounter;
        }

        private (int, int, char) ParsePolicy(string policyString)
        {
            var split = policyString.Split(' ');
            var bounds = split[0].Split('-');
            return (int.Parse(bounds[0]), int.Parse(bounds[1]), Convert.ToChar(split[1]));
        }
    }
}