using System.IO;

namespace AdventOfCode2020
{
    public interface IChallenge
    {
        long RunFirst();
        long RunSecond();
        
        public static string[] GetAllLines(string file)
        {
            return File.ReadAllLines("challenges/" + file);
        }

    }
}