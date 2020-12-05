using System;
using System.IO;
using System.Linq;

namespace AdventOfCode2020
{
    public class Challenge1
    {
        public int RunFirst()
        {
            var allLines = File.ReadAllLines("challenges/1_1.txt");
            var values = Array.ConvertAll<string, int>(allLines, int.Parse);
            Array.Sort(values);
            for (int i = 0; i < values.Length; i++)
            {
                for (int j = i+1; j < values.Length; j++)
                {
                    var first = values[i];
                    var second = values[j];
                    if (first + second == 2020)
                    {
                        Console.WriteLine($"Values are {first} and {second}");
                        return first * second;
                    }

                    if (first + second > 2020)
                    {
                        break;
                    }
                }
            }
            
            throw new Exception("No such value");
        }
    }
}