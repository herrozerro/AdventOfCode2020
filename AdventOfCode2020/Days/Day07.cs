using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AdventOfCode2020
{
    public static class Day07
    {
        public static void RunDay()
        {
            Console.WriteLine("Day 7");

            Part2();

            Console.WriteLine("**************");
            Console.WriteLine(Environment.NewLine);
        }

        public static void Part1()
        {
            var lines = Utilities.GetLinesFromFile("day7.txt");

            var bagrules = new List<BagRule>();

            List<Bag> bags = new();

            foreach (var line in lines)
            {
                BagRule rule = new();

                rule.Name = line.Split(" contain ")[0].Replace("bags","bag");

                var contains = line.Split(" contain ")[1].Trim().Split(",");

                foreach (var containedBag in contains)
                {
                    if (!containedBag.Contains("no other bags."))
                    {
                        rule.ContainedBags.Add(new KeyValuePair<string, int>(containedBag.Trim().Substring(2).Replace(".","").Replace("bags","bag"), int.Parse(containedBag.Trim().Substring(0, 1))));
                    }
                }

                bagrules.Add(rule);
            }

            foreach (var bagRule in bagrules)
            {
                bags.Add(new Bag(bagRule.Name, bagrules));
            }

            int goldbags = 0;

            foreach (var bag in bags)
            {
                var allbags = bag.ContainedBags.SelectManyRecursive(x => x.ContainedBags).ToList();
                allbags.AddRange(bag.ContainedBags);
                if (allbags.Any(x=>x.Name == "shiny gold bag"))
                {
                    goldbags++;
                }
            }

            Console.WriteLine(goldbags);
        }

        public static void Part2()
        {
            var lines = Utilities.GetLinesFromFile("day7.txt");

            var bagrules = new List<BagRule>();

            List<Bag> bags = new();

            foreach (var line in lines)
            {
                BagRule rule = new();

                rule.Name = line.Split(" contain ")[0].Replace("bags", "bag");

                var contains = line.Split(" contain ")[1].Trim().Split(",");

                foreach (var containedBag in contains)
                {
                    if (!containedBag.Contains("no other bags."))
                    {
                        rule.ContainedBags.Add(new KeyValuePair<string, int>(containedBag.Trim().Substring(2).Replace(".", "").Replace("bags", "bag"), int.Parse(containedBag.Trim().Substring(0, 1))));
                    }
                }

                bagrules.Add(rule);
            }

            Bag goldbag = new("shiny gold bag", bagrules);

            int bagcount = goldbag.ContainedBags.Count();
            bagcount += goldbag.ContainedBags.SelectManyRecursive(x => x.ContainedBags).Count();

            Console.WriteLine(bagcount);
        }

        public class BagRule
        {
            public string Name { get; set; }

            public List<KeyValuePair<string, int>> ContainedBags { get; set; } = new();
        }

        public class Bag
        {
            public string Name { get; set; }
            public List<Bag> ContainedBags { get; set; } = new();

            public Bag(string name, List<BagRule> rules)
            {
                BagRule rule = rules.FirstOrDefault(x => x.Name == name);
                Name = name;

                if (rule != null)
                {
                    foreach (var item in rule.ContainedBags)
                    {
                        //string bagname = item.Key.Trim().Replace(".", "").Replace("bags", "bag");
                        //ContainedBags.Add(new Bag(bagname, rules));

                        for (int i = 0; i < item.Value; i++)
                        {
                            string bagname = item.Key.Trim().Replace(".", "").Replace("bags", "bag");

                            ContainedBags.Add(new Bag(bagname, rules));
                        }
                    }
                }

            }
        }
    }
}
