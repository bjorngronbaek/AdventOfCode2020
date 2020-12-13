using System;
using System.Collections.Generic;
using System.Linq;
namespace AdventOfCode2020
{
    public class Challenge10 : IChallenge
    {
        public long RunFirst()
        {
            var ratings = IChallenge.GetAllLines("10_1.txt").Select(long.Parse).ToList();
            ratings.Add(0);
            ratings.Sort();

            long oneGabCount = 0;
            long threeGabCount = 1;

            for (var i = 0; i < ratings.Count-1; i++)
            {
                if (ratings[i] + 1 == ratings[i + 1]) oneGabCount++;
                if (ratings[i] + 3 == ratings[i + 1]) threeGabCount++;
            }

            return oneGabCount * threeGabCount;
        }

        public long RunSecond()
        {
            var dictionaryRatingsPathsToZero = new Dictionary<long,long>();

            var ratings = IChallenge.GetAllLines("10_1.txt").Select(long.Parse).ToList();
            ratings.Add(0);
            ratings.Sort();
            var endRating = ratings[^1]+3;
            ratings.Add(endRating);

            for (int i = 0; i < ratings.Count; i++)
            {
                TraverseSubTree(ratings,dictionaryRatingsPathsToZero, i);
            }

            dictionaryRatingsPathsToZero.TryGetValue(endRating, out var totalPaths);
            return totalPaths;
        }
        
        private void TraverseSubTree(List<long> ratings, Dictionary<long, long> dictionaryRatingsPathsToZero,
            int position)
        {
            long myPathsToZero;
            var current = ratings[position];
            if (current == 0)
            {
                myPathsToZero = 1;
            }
            else
            {
                var reachables = ratings.Where(r => r < current && r >= current-3).ToArray();
                myPathsToZero = 0;
                foreach (var reachable in reachables)
                {
                    dictionaryRatingsPathsToZero.TryGetValue(reachable, out var pathsToZero);
                    myPathsToZero += pathsToZero;
                }
            }
            dictionaryRatingsPathsToZero.Add(current,myPathsToZero);
        }
        
        private class TreeNode
        {
        }
    }
}