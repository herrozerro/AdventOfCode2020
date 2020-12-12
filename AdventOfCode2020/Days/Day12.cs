﻿using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2020
{
    public static class Day12
    {
        public static void RunDay()
        {
            Console.WriteLine("Day 12");

            Part1();

            Console.WriteLine("**************");
            Console.WriteLine(Environment.NewLine);
        }

        public static void Part1()
        {
            var lines = Utilities.GetLinesFromFile("day12.txt");

            int x = 0;
            int y = 0;
            ShipDirection shipDirection = new();

            foreach (var line in lines)
            {
                int movement = int.Parse(line[1..]);
                switch (line[0])
                {
                    case 'N':
                        y += movement;
                        Console.Write($"Moved North {movement} ");
                        break;
                    case 'S':
                        y -= movement;
                        Console.Write($"Moved South {movement} ");
                        break;
                    case 'E':
                        x += movement;
                        Console.Write($"Moved East {movement} ");
                        break;
                    case 'W':
                        x -= movement;
                        Console.Write($"Moved West {movement} ");
                        break;
                    case 'L':
                        shipDirection.ChangeHeading('L', movement);
                        break;
                    case 'R':
                        shipDirection.ChangeHeading('R', movement);
                        break;
                    case 'F':
                        switch (shipDirection.bearing)
                        {
                            case 0:
                                y += movement;
                                Console.Write($"Moved Forward North {movement} ");
                                break;
                            case 90:
                                x += movement;
                                Console.Write($"Moved Forward East {movement} ");
                                break;
                            case 180:
                                y -= movement;
                                Console.Write($"Moved Forward South {movement} ");
                                break;
                            case 360:
                                x -= movement;
                                Console.Write($"Moved Forward West {movement} ");
                                break;
                            default:
                                break;
                        }
                        break;
                    default:
                        break;
                }
                Console.WriteLine($"Current Position: X:{x}, Y:{y}");
            }

            Console.WriteLine(Math.Abs(x) + Math.Abs(y));
        }

        public class ShipDirection
        {
            public int bearing { get; set; } = 90;

            public void ChangeHeading(char dir, int movement)
            {
                Console.Write($"Current Bearing: {bearing} ");
                if (dir == 'L')
                {
                    if (bearing - movement < 0)
                    {
                        bearing += 360;
                    }

                    bearing = bearing - movement % 360;
                    Console.Write($"Turning Left {movement} new bearing: {bearing}");
                }
                else
                {
                    bearing = Math.Abs(bearing + movement) % 360;
                    Console.Write($"Turning Right {movement} new bearing: {bearing}");
                }
            }
        }

        public static void Part2()
        {

        }
    }
}
