using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace AdventOfCode2020
{
    public static class Day12
    {
        public static void RunDay()
        {
            Console.WriteLine("Day 12");

            //Part1();
            Part2();
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
                            case 270:
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

        public class Waypoint
        {
            public PointF Location { get; set; }

            public void MoveWaypoint(int x, int y)
            {
                Location = new PointF(Location.X + x, Location.Y + y);
            }

            public void RotatePoint(float angle)
            {
                var a = angle * System.Math.PI / 180.0;
                float cosa = (float)Math.Cos(a);
                float sina = (float)Math.Sin(a);
                PointF newPoint = new PointF((Location.X * cosa - Location.Y * sina), (Location.X * sina + Location.Y * cosa));
                Location = newPoint;
            }

        }

        public static void Part2()
        {
            var lines = Utilities.GetLinesFromFile("day12.txt");

            int x = 0;
            int y = 0;
            Waypoint waypoint = new()
            {
                Location = new PointF(10, 1)
            };

            foreach (var line in lines)
            {
                int movement = int.Parse(line[1..]);
                switch (line[0])
                {
                    case 'N':
                        waypoint.MoveWaypoint(0, movement);
                        Console.Write($"Moved North {movement} ");
                        break;
                    case 'S':
                        waypoint.MoveWaypoint(0, movement*-1);
                        Console.Write($"Moved South {movement} ");
                        break;
                    case 'E':
                        waypoint.MoveWaypoint(movement,0);
                        Console.Write($"Moved East {movement} ");
                        break;
                    case 'W':
                        waypoint.MoveWaypoint(movement * -1,0);
                        Console.Write($"Moved West {movement} ");
                        break;
                    case 'L':
                        waypoint.RotatePoint(movement);
                        break;
                    case 'R':
                        waypoint.RotatePoint(movement * -1);
                        break;
                    case 'F':
                        x += (int)waypoint.Location.X * movement;
                        y += (int)waypoint.Location.Y * movement;
                        break;
                    default:
                        break;
                }
                Console.WriteLine($"Current Position: X:{x}, Y:{y}");
            }
            Console.WriteLine(Math.Abs(x) + Math.Abs(y));
        }
    }
}
