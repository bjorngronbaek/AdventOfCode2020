using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020
{
    public class Challenge6 : IChallenge
    {
        public long RunFirst()
        {
            //var allLines= IChallenge.GetAllLines("test_6_1.txt");
            var allLines = IChallenge.GetAllLines("6_1.txt");
            var totalAnswers = GetDistinctAnswers(allLines);

            return totalAnswers;
        }

        private static int GetDistinctAnswers(string[] allLines)
        {
            var totalAnswers = 0;
            var group = new HashSet<char>();
            foreach (var line in allLines)
            {
                if (string.IsNullOrEmpty(line))
                {
                    totalAnswers += group.Count;
                    group.Clear();
                }
                else
                {
                    var chars = line.ToCharArray();
                    foreach (var c in chars)
                    {
                        group.Add(c);
                    }
                }
            }
            
            //Handle dangling line with no linebreak at the end
            totalAnswers += group.Count;

            return totalAnswers;
        }
        
        private static int GetSharedAnswers(string[] allLines)
        {
            var totalAnswers = 0;
            var group = new HashSet<char>();
            var isInGroup = false;
            foreach (var line in allLines)
            {
                if (string.IsNullOrEmpty(line))
                {
                    totalAnswers += group.Count;
                    group.Clear();
                    isInGroup = false;
                }
                else
                {
                    var chars = line.ToCharArray();
                    if (isInGroup)
                    {
                        var sharedKeys = new HashSet<char>();
                        foreach (var c in chars)
                        {
                            if (group.Contains(c)) sharedKeys.Add(c);
                        }

                        group = sharedKeys;
                        
                    }
                    else
                    {
                        foreach (var c in chars)
                        {
                            group.Add(c);
                        }

                        isInGroup = true;
                    }
                }
            }
            
            //Handle dangling line with no linebreak at the end
            totalAnswers += group.Count;

            return totalAnswers;
        }

        public long RunSecond()
        {
            var allLines = IChallenge.GetAllLines("6_1.txt");
            //var allLines= IChallenge.GetAllLines("test_6_1.txt");
            return GetSharedAnswers(allLines);
        }
    }
}