using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode2020
{
    public class Challenge4 : IChallenge
    {
        private readonly Regex _hclRegEx = new Regex(@"^#[a-z0-9]{6}$",RegexOptions.Compiled);
        private readonly Regex _eclRegEx = new Regex(@"^(amb|blu|brn|gry|grn|hzl|oth)$",RegexOptions.Compiled);
        private readonly Regex _pidRegEx = new Regex(@"^\b\d{9}$",RegexOptions.Compiled);
        
        public long RunFirst()
        {
            var validPassports = GetValidPassports();
            return validPassports.Count();
        }

        private static IEnumerable<string> GetValidPassports()
        {
            var file = "4_1.txt";
            //var file = "test_4_1.txt";
            //var file = "test_4_2_invalid.txt";
            //var file = "test_4_2_valid.txt";
            var _allLines = IChallenge.GetAllLines(file);
            var passPorts = new List<string>();

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

            var validPassports = passPorts.Where(p =>
            {
                var split = p.Split(' ');
                return (split.Length == 8 || (split.Length == 7 && !p.Contains("cid")));
            });
            return validPassports;
        }


        public long RunSecond()
        {
            var validPassports = GetValidPassports();
            return validPassports.Count(passport =>
            {
                var fields = passport.Split(' ');
                foreach (var field in fields)
                {
                    var fieldPair = field.Split(':');
                    if (!ValidateField(fieldPair[0], fieldPair[1]))
                    {
                        return false;
                    }
                }

                return true;
            });
        }

        private bool ValidateField(string key, string value)
        {
            switch (key)
            {
                case "byr":
                    if (int.TryParse(value, out int byr))
                    {
                        return 1920 <= byr && byr <= 2002;
                    }

                    return false;
                case "iyr":
                    if (int.TryParse(value, out int iyr))
                    {
                        return 2010 <= iyr && iyr <= 2020;
                    }

                    return false;
                case "eyr":
                    if (int.TryParse(value, out int eyr))
                    {
                        return 2020 <= eyr && eyr <= 2030;
                    }

                    return false;
                case "hgt":
                    if (value.EndsWith("cm"))
                    {
                        var indexOf = value.IndexOf('c');
                        var s = value.Substring(0, indexOf);
                        if(int.TryParse(s,out var cm))
                        {
                            return 150 <= cm && cm <= 193;
                        }
                    }
                    
                    if (value.EndsWith("in"))
                    {
                        var indexOf = value.IndexOf('i');
                        var s = value.Substring(0, indexOf);
                        if(int.TryParse(s,out var inches))
                        {
                            return 59 <= inches && inches <= 76;
                        }
                    }

                    return false;
                case "hcl":
                    return _hclRegEx.IsMatch(value);
                case "ecl":
                    return _eclRegEx.IsMatch(value);
                case "pid":
                    return _pidRegEx.IsMatch(value);
                case "cid":
                    return true;
                default:
                    return false;
            }
        }
    }
}