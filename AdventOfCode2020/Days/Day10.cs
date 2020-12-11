using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AdventOfCode2020
{
    public static class Day10
    {
        public static void RunDay()
        {
            Console.WriteLine("Day 10");

            Part1();
            Part2();

            Console.WriteLine("**************");
            Console.WriteLine(Environment.NewLine);
        }

        public static void Part1()
        {
            var lines = Utilities.GetLinesFromFile("day10.txt").Select(x => int.Parse(x)).ToList();

            var onejump = 0;
            var twojump = 0;
            var threejump = 1;

            var currentjolt = 0;

            foreach (var line in lines.OrderBy(x => x))
            {
                switch (line - currentjolt)
                {
                    case 1:
                        onejump++;
                        break;
                    case 2:
                        twojump++;
                        break;
                    case 3:
                        threejump++;
                        break;
                    case 0:
                        break;
                    default:
                        break;
                }
                currentjolt = line;
            }
            Console.WriteLine(onejump * threejump);
        }

        public static void Part2()
        {
            var lines = Utilities.GetLinesFromFile("day10.txt").Select(x => long.Parse(x)).OrderBy(x => x).ToList();
            lines.Add(lines.Max() + 3);
            lines.Insert(0, 0);
            Console.WriteLine(SolvePartTwo(lines.ToArray()));
        }

        static Dictionary<long, long> resultSet = new Dictionary<long, long>();
        static int counter = 0;

        private static long SolvePartTwo(long[] inputs)
        {
            counter++;
            if (resultSet.ContainsKey(inputs.Length))
            {
                return resultSet[inputs.Length];
            }

            if (inputs.Length == 1)
            {
                return 1;
            }

            long total = 0;
            long temp = inputs[0];
            for (int i = 1; i < inputs.Length; i++)
            {
                if (inputs[i] - temp <= 3)
                {
                    total += SolvePartTwo(inputs[i..]);
                }
                else
                {
                    break;
                }
            }

            resultSet.Add(inputs.Length, total); 
            foreach (var result in resultSet)
            {
                Console.WriteLine($"{result.Key} {result.Value}");
            }


            return total;
        }
    }
}
