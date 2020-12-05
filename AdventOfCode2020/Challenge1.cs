using System;
using System.IO;
using System.Linq;

namespace AdventOfCode2020
{
    public interface IChallenge
    {
        long RunFirst();
        long RunSecond();
    }

    public class Challenge1 : IChallenge
    {
        public long RunFirst()
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
        
        public long RunSecond()
        {
            var allLines = File.ReadAllLines("challenges/1_1.txt");
            var values = Array.ConvertAll<string, int>(allLines, int.Parse);
            Array.Sort(values);
            for (int i = 0; i < values.Length; i++)
            {
                for (int j = i+1; j < values.Length; j++)
                {
                    for (int k = j+1; k < values.Length; k++)
                    {
                        var first = values[i];
                        var second = values[j];
                        var third = values[k];
                        if (first + second + third == 2020)
                        {
                            Console.WriteLine($"Values are {first} and {second} and {third}");
                            return first * second * third;
                        }

                        if (first + second + third > 2020)
                        {
                            break;
                        }
                    }
                }
            }
            
            throw new Exception("No such value");
        }
    }
}