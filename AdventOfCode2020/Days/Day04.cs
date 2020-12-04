using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2020
{
    public static class Day04
    {
        public static void RunDay()
        {
            Console.WriteLine("Day 4");

            Part1();
            Part2();
            Console.WriteLine("**************");
            Console.WriteLine(Environment.NewLine);
        }

        public static List<string> fields = new List<string>()
        {
            "byr",
            "iyr",
            "eyr",
            "hgt",
            "hcl",
            "ecl",
            "pid"
        };

        public static void Part1()
        {
            var lines = Utilities.GetLinesFromFile("day4.txt");

            var passports = new List<string>();

            var passport = "";

            var validpassport = 0;

            foreach (var line in lines)
            {
                passport += " " + line;

                if (line == string.Empty)
                {
                    passports.Add(passport);
                    passport = "";
                }
            }

            foreach (var pass in passports)
            {
                if (isvalid(pass))
                {
                    validpassport++;
                }
            }

            Console.WriteLine(validpassport);
        }

        public static void Part2()
        {
            var lines = Utilities.GetLinesFromFile("day4.txt");

            var passports = new List<string>();

            var passport = "";

            var validpassport = 0;

            foreach (var line in lines)
            {
                passport += " " + line;

                if (line == string.Empty)
                {
                    passports.Add(passport.Trim());
                    passport = "";
                }
            }

            foreach (var pass in passports)
            {
                if (isvalidfields(pass))
                {
                    validpassport++;
                }
            }

            Console.WriteLine(validpassport);
        }

        public static bool isvalid(string passport)
        {
            foreach (var field in fields)
            {
                if (!passport.Contains(field + ":") && field != "cid")
                {
                    return false;
                }
            }

            return true;
        }

        public static bool isvalidfields(string passport)
        {
            var passportfields = passport.Split(" ");

            foreach (var field in fields)
            {
                if (!passport.Contains(field + ":"))
                {
                    return false;
                }
                else
                {
                    var ppf = passportfields.First(x => x.Contains(field + ":")).Split(":")[1];
                    switch (field)
                    {
                        case "byr":
                            var byr = 0;
                            if (!int.TryParse(ppf, out byr))
                                return false;

                            if (!(byr >= 1920 && byr <= 2002))
                            {
                                return false;
                            }
                            break;
                        case "iyr":
                            var iyr = 0;
                            if (!int.TryParse(ppf, out iyr))
                                return false;

                            if (!(iyr >= 2010 && iyr <= 2020))
                            {
                                return false;
                            }
                            break;
                        case "eyr":
                            var eyr = 0;
                            if (!int.TryParse(ppf, out eyr))
                                return false;

                            if (!(eyr >= 2020 && eyr <= 2030))
                            {
                                return false;
                            }
                            break;
                        case "hgt":
                            var hgt = 0;
                            if (!int.TryParse(ppf.Substring(0, ppf.Length - 2), out hgt))
                                return false;

                            if (ppf.Contains("cm"))
                            {
                                if (!(hgt >= 150 && hgt <= 193))
                                {
                                    return false;
                                }
                            }
                            else if (ppf.Contains("in"))
                            {
                                if (!(hgt >= 59 && hgt <= 76))
                                {
                                    return false;
                                }
                            }
                            else
                            {
                                return false;
                            }
                            break;
                        case "hcl":
                            if (!Regex.IsMatch(ppf, "^#(?:[0-9a-fA-F]{6})$"))
                            {
                                return false;
                            }
                            break;
                        case "ecl":
                            if (!(ppf.Contains("amb") || ppf.Contains("blu") || ppf.Contains("brn") || ppf.Contains("gry") || ppf.Contains("grn") || ppf.Contains("hzl") || ppf.Contains("oth")))
                            {
                                return false;
                            }
                            break;
                        case "pid":
                            if (ppf.Length != 9)
                            {
                                return false;
                            }
                            break;
                        default:
                            break;
                    }
                }
            }

            return true;
        }
    }
}
