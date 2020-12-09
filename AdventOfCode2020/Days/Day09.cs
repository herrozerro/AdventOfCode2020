using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2020
{
    public static class Day09
    {
        public static void RunDay()
        {
            Console.WriteLine("Day 9");

            Part1();
            Part2();

            Console.WriteLine("**************");
            Console.WriteLine(Environment.NewLine);
        }

        public static void Part1()
        {
            var lines = Utilities.GetLinesFromFile("day9.txt").Select(x => long.Parse(x)).ToList();

            var preamble = 25;

            var index = 0;

            long lastgood = 0;

            foreach (var line in lines.Skip(preamble))
            {
                var prevInts = lines.GetRange(index, preamble);

                foreach (var prevint in prevInts)
                {
                    if (prevInts.Where(x => x != prevint).Any(x => x + prevint == line))
                    {
                        lastgood = line;
                        break;
                    }
                }

                if (lastgood != line)
                {
                    Console.WriteLine(line);
                    return;
                }

                index++;
            }
        }

        public static void Part2()
        {
            var lines = Utilities.GetLinesFromFile("day9.txt").Select(x => long.Parse(x)).Where(x => x < 69316178).ToList();

            long target = 69316178;

            var i = 1;
            foreach (var line in lines)
            {
                
                var start = line;
                var contigious = new List<long>() { line };
                foreach (var item in lines.Skip(i))
                {
                    contigious.Add(item);
                    if (contigious.Sum() == target)
                    {
                        Console.WriteLine($"min: {contigious.Min()} max: {contigious.Max()}  sum: {contigious.Min() + contigious.Max()}");
                        return;
                    }
                }
                i++;
            }
        }
    }
}
