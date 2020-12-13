using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AdventOfCode2020
{
    public static class Day13
    {
        public static void RunDay()
        {
            Console.WriteLine("Day 13");

            //Part1();
            Part2();

            Console.WriteLine("**************");
            Console.WriteLine(Environment.NewLine);
        }

        public static void Part1()
        {
            var lines = Utilities.GetLinesFromFile("day13.txt");

            long timestamp = long.Parse(lines[0]);
            int minutes = 0;
            int busID = 0;
            List<int> Ids = lines[1].Split(",").Where(x => x != "x").Select(x => int.Parse(x)).ToList();

            while (busID == 0)
            {
                minutes++;
                foreach (var id in Ids)
                {
                    if ((timestamp + minutes) % id == 0)
                    {
                        busID = id;
                        break;
                    }
                }
            }

            Console.WriteLine(minutes * busID);
        }

        public static void Part2()
        {

            //copied from https://topaz.github.io/paste/#XQAAAQBuBQAAAAAAAAAQapjOhiOG6pmxfg/hFRrKWY6tEUGvb7nlnkLnmIhD6RDBRyTO2FEYvG29i13yFz4NLJYv5HMmuZ38w7EC5Icyr2EI8VPjByYKMFiZs4qX9qiIGytFNMusi+fhl9/vtAUqDrl3xDfFBcqE6AO39wvs3qNOrNNKs12KWZkOBp+avnCN6IylCDiXxNepCDXER+vcnGmOVT3m+4PfcWh385ZC6dAt1X9MnTrLOGh+bLyjpjhTLag81hOrLatlveTF5TjqDDHd2g6bxRqp2+X8NHL+7hOGX+Jrxs3cKFlbe+lSGB+2142jGcgUKzyWMGJ5taU5Bg+4Cfgd4zOr05lQBNigjt+MoKcPVVLaL7CtNvupzLPDKxziMV/HapYrtZSkjyjN2769a5rDPqGpaMDf9MqdosXXGCISTc0I9bi/T5gzj0ib/NwzAIlDO3ncPkaT0JGhYHnn1vOVcabDEACtTh9OGGGyNijXaqMi7u9WC5w8SvbewW6zfXQvJcHndAtTZyJiWlYTDGK3JORTbZEes24C9o3tqO4jWuxmbkTF8qyYBDo7GTAP92BkkU7p6HEoA/lpgaQNV/UxuNPFxfb+wzfRgp3BAtxjDUMpUUNjM5VdzXzxA/TQd97QmXKUcejLVSuZlDAs91YFKJZwFCrQ/KvkNAnpu3GY+v15TL03LcayECITQAN3HEHvPy1C+mSveHb8OE+r18X5LB2QgWsULDOQbzwhrx05nmyQNvM5u4O0OJRkdukrBc59pLPMTsTjb7bIiQeMtYd1Oat/hCODbiiI3BKJrRkkbmeinFvB0mKXESdObiHFlBdTmKD5D4Ue9kWUmkp0qYEgCVlGpg7QDi1b2ACPRC3/oKidYw==
            var lines = Utilities.GetLinesFromFile("day13.txt");
            int start = int.Parse(lines[0]);
            var buses = lines[1].Split(',', StringSplitOptions.RemoveEmptyEntries).Where(s => !s.Contains("x")).Select(s => int.Parse(s)).ToArray();

            var deps = new Dictionary<int, int>(); //bus ID to departure time

            foreach (var b in buses)
            {
                int dep = (start % b == 0) ? start : b * (start / b + 1);
                deps.Add(b, dep);
            }

            var best = deps.First(k => k.Value == deps.Min(t => t.Value));
            Console.WriteLine("Part 1: " + best.Key * (best.Value - start));
            Console.WriteLine();

            var sp = lines[1].Split(',');
            char[] variable = new[] { 'a', 'b', 'c', 'd', 'f', 'g', 'h', 'j', 'k' };
            int varI = 0;
            string query = "";
            for (int i = 0; i < sp.Length; i++)
            {
                if (sp[i] == "x") continue;
                query += $"{int.Parse(sp[i])}{variable[varI++]}-{i}=T,";
            }
            string url = "https://www.wolframalpha.com/input/?i=" + query.TrimEnd(',');
            Console.WriteLine("Opening " + url);
            //OpenBrowser(url);
            Console.WriteLine("Find the part that says T = an + b");
            Console.WriteLine("The answer is b");


            //Console.WriteLine(solution);
        }
    }
}
