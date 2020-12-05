using System;
using System.IO;

namespace AdventOfCode2020
{
    public class Challenge3 : IChallenge
    {
        private readonly string[] _map;
        private int MapWidth => _map[0].Length;
        private int MapHeight => _map.Length;
        
        public Challenge3()
        {
            //_map = File.ReadAllLines("challenges/test_3_1.txt");
            _map = File.ReadAllLines("challenges/3_1.txt");
        }

        public long RunFirst()
        {
            var route = (1, 3);
            return GetTreeCountForRoute(route);
        }

        private long GetTreeCountForRoute((int, int) route)
        {
            var row = 1;
            var column = 1;
            var treeCounter = 0;
            while (row <= MapHeight)
            {
                if (IsTreePosition(column, row)) treeCounter++;
                row += route.Item1;
                column += route.Item2;
            }

            return treeCounter;
        }

        private bool IsTreePosition(int column, int row)
        {
            var x = (column-1) % MapWidth;
            var y = row - 1;
            
            var c = _map[y][x];
            return c switch
            {
                '.' => false,
                '#' => true,
                _ => throw new ArgumentException("Not a valid terrain type")
            };
        }


        public long RunSecond()
        {
            var totalTrees = GetTreeCountForRoute((1,1));
            totalTrees *= GetTreeCountForRoute((1, 3));
            totalTrees *= GetTreeCountForRoute((1, 5));
            totalTrees *= GetTreeCountForRoute((1, 7));
            totalTrees *= GetTreeCountForRoute((2, 1));

            return totalTrees;
        }
    }
}