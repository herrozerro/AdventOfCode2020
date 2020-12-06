using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2020
{
    public static class Day06
    {
        public static void RunDay()
        {
            Console.WriteLine("Day 6");

            Part1();
            Part2();

            Console.WriteLine("**************");
            Console.WriteLine(Environment.NewLine);
        }

        public static void Part1()
        {
            var lines = Utilities.GetLinesFromFile("day6.txt");

            var groups = new List<group>();

            var forms = new List<string>();

            foreach (var line in lines)
            {
                if (line == string.Empty)
                {
                    groups.Add(new group() { groupid = groups.Count(), FormAnswers = forms.ToList() }); ;
                    forms.Clear();
                }
                else
                {
                    forms.Add(line);
                }
            }

            Console.WriteLine(groups.Sum(x => x.GetScore()));
        }

        public class group
        {
            public int groupid { get; set; }
            public List<string> FormAnswers { get; set; }
            
            public int GetScore()
            {
                return FormAnswers.SelectMany(x => x.Select(y => y)).ToList().Distinct().Count();
            }

            public int GetCommonScores()
            {
                var alpha = "abcdefghijklmnopqrstuvwxyz";

                int count = 0;

                foreach (var letter in alpha)
                {
                    count += FormAnswers.All(l => l.Contains(letter)) ? 1 : 0;
                }

                return count;
            }
        }

        public static void Part2()
        {
            var lines = Utilities.GetLinesFromFile("day6.txt");

            var groups = new List<group>();

            var forms = new List<string>();

            foreach (var line in lines)
            {
                if (line == string.Empty)
                {
                    groups.Add(new group() { groupid = groups.Count(), FormAnswers = forms.ToList() }); ;
                    forms.Clear();
                }
                else
                {
                    forms.Add(line);
                }
            }

            Console.WriteLine(groups.Sum(x => x.GetCommonScores()));
        }
    }
}
