using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AdventOfCode2020
{
    public class Challenge4 : IChallenge
    {
        public long RunFirst()
        {
            var file = "4_1.txt";
            //var file = "test_4_1.txt";
            var _allLines = IChallenge.GetAllLines(file);
            var passPorts = new List<string>();
            long validPassports = 0;

            var passportBuilder = new StringBuilder();
            foreach (var line in _allLines)
            {
                if (!string.IsNullOrEmpty(line) && !string.IsNullOrWhiteSpace(line))
                {
                    if (passportBuilder.Length > 0)
                    {
                        passportBuilder.Append(' ');
                    }
                    passportBuilder.Append(line);
                }
                else
                {
                    passPorts.Add(passportBuilder.ToString());
                    passportBuilder.Clear();
                }
            }
            //add dangling line from StringBuilder, because the was no blank line at the end of the file.
            passPorts.Add(passportBuilder.ToString());

            foreach (var passPort in passPorts)
            {
                var split = passPort.Split(' ');
                if (split.Length == 8 || (split.Length == 7 && !passPort.Contains("cid")))
                {
                    validPassports++;
                }
                else
                {
                    //Console.WriteLine($"Not valid. Lenght: {split.Length}. cid: {passPort.Contains("cid")}\t {passPort}");
                }
            }

            return validPassports;
        }


        public long RunSecond()
        {
            throw new System.NotImplementedException();
        }
    }
}