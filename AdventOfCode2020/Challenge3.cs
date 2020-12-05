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

        public int RunFirst()
        {
            int row = 1;
            int column = 1;

            int treeCounter = 0;
            while (row <= MapHeight)
            {
                if (IsTreePosition(column, row)) treeCounter++;
                row += 1;
                column += 3;
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


        public int RunSecond()
        {
            throw new System.NotImplementedException();
        }
    }
}