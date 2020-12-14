using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AdventOfCode2020
{
    public static class Day14
    {
        public static void RunDay()
        {
            Console.WriteLine("Day 14");

            Part1();
            Part2();

            Console.WriteLine("**************");
            Console.WriteLine(Environment.NewLine);
        }

        public static void Part1()
        {
            var lines = Utilities.GetLinesFromFile("day14.txt");

            string mask = "";

            Dictionary<long, long> memory = new();

            foreach (var line in lines)
            {
                if (line.StartsWith("mask"))
                {
                    mask = line.Split("=")[1].Trim();
                }
                else
                {
                    string assignedNumber = Convert.ToString(long.Parse(line.Split("=")[1].Trim()),2).PadLeft(36,'0');
                    long index = long.Parse(line.Remove(0,4).Split("]")[0].ToString());
                    for (int i = 0; i < mask.Length; i++)
                    {
                        if (mask[i] != 'X')
                        {
                            assignedNumber = assignedNumber.Remove(i, 1).Insert(i, mask[i].ToString());
                        }
                    }

                    if (memory.ContainsKey(index))
                    {
                        memory[index] = Convert.ToInt64(assignedNumber, 2);
                    }
                    else
                    {
                        memory.Add(index, Convert.ToInt64(assignedNumber, 2));
                    }
                }

            }

            Console.WriteLine(memory.Sum(x => x.Value));
        }

        public static void Part2()
        {
            var lines = Utilities.GetLinesFromFile("day14.txt");

            string mask = "";

            Dictionary<long, long> memory = new();

            foreach (var line in lines)
            {
                if (line.StartsWith("mask"))
                {
                    mask = line.Split("=")[1].Trim();
                }
                else
                {
                    var index = long.Parse(line.Remove(0, 4).Split("]")[0]);
                    string Indexes = Convert.ToString(index, 2).PadLeft(36, '0');
                    long value = long.Parse(line.Split("=")[1].Trim());
                    for (int i = 0; i < mask.Length; i++)
                    {
                        if (mask[i] == '1')
                        {
                            Indexes = Indexes.Remove(i, 1).Insert(i, mask[i].ToString());
                        }
                    }

                    int floats = mask.Count(x => x == 'X');
                    var masks = new List<string>();

                    for (int i = 0; i < Math.Pow(2,floats); i++)
                    {
                        masks.Add(Convert.ToString(i, 2).PadLeft(floats, '0'));
                    }

                    foreach (var m in masks)
                    {
                        var address = Indexes;
                        var j = 0;
                        for (int i = 0; i < mask.Length; i++)
                        {
                            if (mask[i] == 'X')
                            {
                                address = address.Remove(i, 1).Insert(i, m[j].ToString());
                                j++;
                            }
                        }

                        if (memory.ContainsKey(Convert.ToInt64(address, 2)))
                        {
                            memory[Convert.ToInt64(address, 2)] = value;
                        }
                        else
                        {
                            memory.Add(Convert.ToInt64(address, 2), value);
                        }
                    }
                }

            }

            Console.WriteLine(memory.Sum(x => x.Value));
        }
    }
}
