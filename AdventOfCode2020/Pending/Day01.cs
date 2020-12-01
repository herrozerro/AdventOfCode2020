using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2020
{
    public static class Day01
    {
        public static void RunDay()
        {
            var lines = Utilities.GetLinesFromFile("day1.txt").ToList().Select(x=> int.Parse(x)).ToList();

            foreach (var line in lines)
            {
                foreach (var secondLine in lines)
                {
                    if (line + secondLine == 2020)
                    {
                        Console.WriteLine($"line 1: {line} line 2: {secondLine} multiplied = {line * secondLine}");
                    }
                }
            }

            foreach (var line in lines)
            {
                foreach (var secondLine in lines)
                {
                    foreach (var thirdLine in lines)
                    {
                        if (line + secondLine + thirdLine == 2020)
                        {
                            Console.WriteLine($"line 1: {line} line 2: {secondLine} line 3: {thirdLine} multiplied = {line * secondLine * thirdLine}");
                        }
                    }
                    
                }
            }



            Console.WriteLine("**************");
            Console.WriteLine(Environment.NewLine);
        }

        
    }
}
