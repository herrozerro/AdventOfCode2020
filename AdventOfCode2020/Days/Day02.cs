using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AdventOfCode2020
{
    public static class Day02
    {
        public static void RunDay()
        {
            Console.WriteLine("Day 2");

            Part1();
            Part2();

            Console.WriteLine("**************");
            Console.WriteLine(Environment.NewLine);
        }

        public static void Part1()
        {
            var ls = Utilities.GetLinesFromFile("day2.txt");

            var count = 0;

            foreach (var line in ls)
            {
                var split = line.Split(' ');

                var lower = int.Parse(split[0].Split('-')[0]);
                var upper = int.Parse(split[0].Split('-')[1]);

                var target = split[1][0];

                var pwd = split[2];

                if(pwd.Count(x=>x == target) <= upper && pwd.Count(x => x == target) >= lower)
                    count++;
            }

            Console.WriteLine(count);


        }

        public static void Part2()
        {
            var ls = Utilities.GetLinesFromFile("day2.txt");

            var count = 0;

            foreach (var line in ls)
            {
                var split = line.Split(' ');

                var first = int.Parse(split[0].Split('-')[0]) - 1;
                var second = int.Parse(split[0].Split('-')[1]) - 1;

                var target = split[1][0];

                var pwd = split[2];

                if ((pwd[first] == target || pwd[second] == target) && pwd[first] != pwd[second])
                    count++;
            }

            Console.WriteLine(count);
        }
    }
}
