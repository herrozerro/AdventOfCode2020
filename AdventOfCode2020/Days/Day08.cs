using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2020
{
    public static class Day08
    {
        public static void RunDay()
        {
            Console.WriteLine("Day 8");

            Part1();

            Part2();

            Console.WriteLine("**************");
            Console.WriteLine(Environment.NewLine);
        }

        public static void Part1()
        {
            var lines = Utilities.GetLinesFromFile("day8.txt");

            int accum = 0;
            int linenumber = 0;
            Dictionary<int, int> linesused = new();

            foreach (var line in lines)
            {

            }

            while (true)
            {
                var line = lines[linenumber];
                if (linesused.ContainsKey(linenumber))
                {
                    break;
                }
                linesused.Add(linenumber, 0);

                switch (line.Split(" ")[0])
                {
                    case "acc":
                        accum += int.Parse(line.Split(" ")[1]);
                        linenumber++;
                        break;
                    case "nop":
                        linenumber++;
                        break;
                    case "jmp":
                        linenumber += int.Parse(line.Split(" ")[1]);
                        break;
                    default:
                        break;
                }
            }

            Console.WriteLine(accum);
        }

        public static void Part2()
        {
            var lines = Utilities.GetLinesFromFile("day8.txt");



            for (int i = 0; i < lines.Length; i++)
            {
                if (!lines[i].Contains("acc"))
                {
                    switch (lines[i].Split(" ")[0])
                    {
                        case "nop":
                            lines[i] = lines[i].Replace("nop", "jmp");
                            break;
                        case "jmp":
                            lines[i] = lines[i].Replace("jmp", "nop");
                            break;
                        default:
                            break;
                    }

                    int accum = 0;
                    int linenumber = 0;
                    Dictionary<int, int> linesused = new();
                    bool stop = false;

                    while (true)
                    {
                        if (linenumber == lines.Count())
                        {
                            Console.WriteLine(accum);
                            return;
                        }
                        var line = lines[linenumber];
                        if (linesused.ContainsKey(linenumber))
                        {
                            switch (lines[i].Split(" ")[0])
                            {
                                case "nop":
                                    lines[i] = lines[i].Replace("nop","jmp");
                                    break;
                                case "jmp":
                                    lines[i] = lines[i].Replace("jmp","nop");
                                    break;
                                default:
                                    break;
                            }
                            break;
                        }
                        linesused.Add(linenumber, 0);

                        

                        switch (line.Split(" ")[0])
                        {
                            case "acc":
                                accum += int.Parse(line.Split(" ")[1]);
                                linenumber++;
                                break;
                            case "nop":
                                linenumber++;
                                break;
                            case "jmp":
                                linenumber += int.Parse(line.Split(" ")[1]);
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
        }
    }
}
