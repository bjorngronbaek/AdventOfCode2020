using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2020
{
    
    public class Challenge7 : IChallenge
    {

        private readonly Regex _subBags = new Regex(@"(\d+)\s(\w+\s\w+)\sbags?,?",RegexOptions.Compiled);
        private readonly Regex _topBag = new Regex(@"^(\w+\s\w+)\sbags",RegexOptions.Compiled);
        
        public long RunFirst()
        {

            //var allLines = IChallenge.GetAllLines("test_7_1.txt");
            var allLines = IChallenge.GetAllLines("7_1.txt");
            var containerDictionary = GetDictionaryOfContainers(allLines);
            
            containerDictionary.TryGetValue("shiny gold", out var containers);
            var allNodes = TraverseContainers(containers, containerDictionary);

            return allNodes.Count;
        }

        private Dictionary<string, List<string>> GetDictionaryOfContainers(string[] allLines)
        {
            var containerDictionary = new Dictionary<string, List<string>>();
            foreach (var line in allLines)
            {
                var split = line.Split("contain");
                var topBag = _topBag.Match(split[0]).Groups[1].Value;
                var contents = MatchBagContent(split[1]);
                foreach (var content in contents)
                {
                    if (containerDictionary.ContainsKey(content.Item1))
                    {
                        containerDictionary[content.Item1].Add(topBag);
                    }
                    else
                    {
                        containerDictionary.Add(content.Item1, new List<string> {topBag});
                    }
                }
            }

            return containerDictionary;
        }

        private Dictionary<string, List<(string, int)>> GetDictionaryOfContents(string[] allLines)
        {
            var contentsDictionary = new Dictionary<string, List<(string, int)>>();
            foreach (var line in allLines)
            {
                var split = line.Split("contain");
                var topBag = _topBag.Match(split[0]).Groups[1].Value;
                var contents = MatchBagContent(split[1]);
                contentsDictionary.Add(topBag,contents);
            }
            
            return contentsDictionary;
        }

        private static HashSet<string> TraverseContainers(List<string> containers, Dictionary<string, List<string>> contentsDictionary)
        {
            var allNodes = new HashSet<string>();
            foreach (var container in containers)
            {
                allNodes.Add(container);
                if (contentsDictionary.TryGetValue(container, out var c))
                {
                    allNodes.UnionWith(TraverseContainers(c,contentsDictionary));
                }
            }

            return allNodes;
        }

        private List<(string, int)> MatchBagContent(string line)
        {
            var bagNames = new List<(string,int)>();
            var matchCollection = _subBags.Matches(line);
            foreach (Match match in matchCollection)
            {
                bagNames.Add((match.Groups[2].Value,int.Parse(match.Groups[1].Value)));
                //Console.WriteLine($"Contains: {match.Groups[1]} bags of color {match.Groups[2]}");
            }

            return bagNames;
        }

        public long RunSecond()
        {
            //var allLines = IChallenge.GetAllLines("test_simple_7_2.txt");
            //var allLines = IChallenge.GetAllLines("test_7_2.txt");
            var allLines = IChallenge.GetAllLines("7_1.txt");
            var contents = GetDictionaryOfContents(allLines);
            var bagCount = TraverseBags("shiny gold", contents) - 1; // do not count the shiny bag in it self

            return bagCount;
        }

        private long TraverseBags(string bag, Dictionary<string, List<(string, int)>> contents)
        {
            if (contents.TryGetValue(bag, out var bags))
            {
                if (bags.Count > 0)
                {
                    long sum = 0;
                    foreach (var b in bags)
                    {
                        var subBagCount = TraverseBags(b.Item1, contents);
                        sum += b.Item2 * subBagCount;
                    }

                    return sum + 1;
                }

                return 1;
            }
            
            return 0;
        }
    }
}