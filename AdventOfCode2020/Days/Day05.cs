using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AdventOfCode2020
{
    public static class Day05
    {
        public static void RunDay()
        {
            Console.WriteLine("Day 5");

            Part1();
            Part2();

            Console.WriteLine("**************");
            Console.WriteLine(Environment.NewLine);
        }

        public static void Part1()
        {
            var lines = Utilities.GetLinesFromFile("day5.txt");

            var passes = new List<BoardingPass>();

            foreach (var line in lines)
            {
                passes.Add(new BoardingPass(line));
            }


            Console.WriteLine(passes.Max(x=>x.Id));
        }

        public static void Part2()
        {
            var lines = Utilities.GetLinesFromFile("day5.txt");

            var passes = new List<BoardingPass>();

            foreach (var line in lines)
            {
                passes.Add(new BoardingPass(line));
            }


            var manifest = Enumerable.Range(0, 859).Skip(12);

            var exceptions = manifest.Except(passes.Select(x => x.Id));



            foreach (var item in exceptions)
            {
                Console.WriteLine(item);
            }
        }

        public class BoardingPass
        {
            public string Pass { get; set; }
            public int Row { get; set; }
            public int Seat { get; set; }
            public int Id { get; set; }

            public BoardingPass(string pass)
            {
                Pass = pass;
                this.ParsePass();
            }

            private void ParsePass()
            {
                string rowCode = Pass.Substring(0, 7);
                string seatCode = Pass.Substring(7);

                var rows = Enumerable.Range(0, 128);

                foreach (var item in rowCode)
                {
                    var half = rows.Count() / 2;
                    rows = item == 'F' ? rows.Take(half) : rows.Skip(half);
                }

                Row = rows.First();

                var seats = Enumerable.Range(0, 8);

                foreach (var item in seatCode)
                {
                    var half = seats.Count() / 2;
                    seats = item == 'L' ? seats.Take(half) : seats.Skip(half);
                }

                Seat = seats.First();

                Id = Row * 8 + Seat;
            }
        }
    }
}
