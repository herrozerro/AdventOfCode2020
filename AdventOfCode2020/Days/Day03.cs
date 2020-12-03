using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2020
{
    public static class Day03
    {
        public static void RunDay()
        {
            Console.WriteLine("Day 3");

            Part1();


            Console.WriteLine(Part2(1, 1) * Part2(1, 3) * Part2(1, 5) * Part2(1, 7) * Part2(2, 1));

            Console.WriteLine("**************");
            Console.WriteLine(Environment.NewLine);
        }

        public static void Part1()
        {
            var lines = Utilities.GetLinesFromFile("day3.txt");

            var j = 0;

            var treeshit = 0;

            for (int i = 0; i < lines.Count(); i++)
            {
                var space = lines[i][j];

                if (space == '#')
                {
                    treeshit++;
                }

                j += 3;

                if (j > 30)
                {
                    j -= 31;
                }
            }


            Console.WriteLine(treeshit);
            

        }

        public static int Part2(int slopedown, int sloperight)
        {
            var lines = Utilities.GetLinesFromFile("day3.txt");

            var j = 0;

            var treeshit = 0;

            for (int i = 0; i < lines.Count(); i += slopedown)
            {
                var space = lines[i][j];

                if (space == '#')
                {
                    treeshit++;
                }

                j += sloperight;

                if (j > 30)
                {
                    j -= 31;
                }
            }


            return treeshit;
        }
    }
}
