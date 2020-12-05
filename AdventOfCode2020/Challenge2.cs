using System;
using System.IO;
using System.Linq;

namespace AdventOfCode2020
{
    public class Challenge2 : IChallenge
    {
        public int RunFirst()
        {
            var validCounter = 0;
            var allLines = File.ReadAllLines("challenges/2_1.txt");
            foreach (var line in allLines)
            {
                var split = line.Split(':');
                var policy = ParsePolicy(split[0]);
                if (ValidateCountPolicy(split[1], policy)) validCounter++;
            }

            return validCounter;
        }

        private static bool ValidateCountPolicy(string password, (int, int, char) policy)
        {
            var charCount = password.Count(character => character == policy.Item3);
            var valid = policy.Item1 <= charCount && charCount <= policy.Item2;
            return valid;
        }

        public int RunSecond()
        {
            var validCounter = 0;
            var allLines = File.ReadAllLines("challenges/2_1.txt");
            foreach (var line in allLines)
            {
                var split = line.Split(':');
                var policy = ParsePolicy(split[0]);
                if (ValidatePositionPolicy(split[1], policy)) validCounter++;
            }

            return validCounter;
        }

        private bool ValidatePositionPolicy(string password, (int, int, char) policy)
        {
            return password[policy.Item1] != password[policy.Item2] &&
                   (password[policy.Item1] == policy.Item3 || password[policy.Item2] == policy.Item3);
        }

        private (int, int, char) ParsePolicy(string policyString)
        {
            var split = policyString.Split(' ');
            var bounds = split[0].Split('-');
            return (int.Parse(bounds[0]), int.Parse(bounds[1]), Convert.ToChar(split[1]));
        }
    }
}