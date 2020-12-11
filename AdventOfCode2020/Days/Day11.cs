using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace AdventOfCode2020
{
    public static class Day11
    {
        public static void RunDay()
        {
            Console.WriteLine("Day 11");

            //Part1();
            Part2();

            Console.WriteLine("**************");
            Console.WriteLine(Environment.NewLine);
        }

        public static void Part1()
        {
            var s = Utilities.GetLinesFromFile("day11.txt").Select(x => x.Replace(",", "").ToArray()).ToArray();

            var arr = new char[s.GetLength(0), s.GetLength(0)];

            for (int i = 0; i < s.GetLength(0); i++)
            {
                for (int j = 0; j < s[0].GetLength(0); j++)
                {
                    arr[i, j] = s[i][j];
                }
            }

            int counter = 0;

            while (true)
            {
                arr = RunGameOfLife(arr);

                if (arr == null)
                {
                    Console.WriteLine(counter);
                    return;
                }



                else
                {
                    for (int r = 0; r < arr.GetLength(0); r++)
                    {
                        Console.WriteLine(string.Join("", Enumerable.Range(0, arr.GetLength(1))
                            .Select(x => arr[r, x])).ToArray());
                    }
                    Console.WriteLine(string.Join("", arr.Cast<char>().ToArray()).Count(x => x == '#'));
                    Console.WriteLine("");
                }
                counter++;
            }
        }

        public static void Part2()
        {
            var s = Utilities.GetLinesFromFile("day11.txt").Select(x => x.Replace(",", "").ToArray()).ToArray();

            var arr = new char[s.GetLength(0), s.GetLength(0)];

            for (int i = 0; i < s.GetLength(0); i++)
            {
                for (int j = 0; j < s[0].GetLength(0); j++)
                {
                    arr[i, j] = s[i][j];
                }
            }

            int counter = 0;

            while (true)
            {
                arr = RunGameOfLifeAdvanced(arr);

                if (arr == null)
                {
                    Console.WriteLine(counter);
                    return;
                }
                else
                {
                    for (int r = 0; r < arr.GetLength(0); r++)
                    {
                        Console.WriteLine(string.Join("", Enumerable.Range(0, arr.GetLength(1))
                            .Select(x => arr[r, x])).ToArray());
                    }
                    Console.WriteLine(string.Join("", arr.Cast<char>().ToArray()).Count(x => x == '#'));
                    Console.WriteLine("");
                }
                counter++;
            }
        }

        public static char[,] RunGameOfLife(char[,] grid)
        {
            List<KeyValuePair<int, int>> changes = new();
            int maxrow = grid.GetLength(0);
            int maxcol = grid.GetLength(1);

            //get grid changes
            for (int row = 0; row < grid.GetLength(0); row++)
            {
                for (int column = 0; column < grid.GetLength(1); column++)
                {
                    if (grid[row, column] == '.')
                    {
                        continue;
                    }

                    int occupiedseats = 0;

                    //check adjecent seats
                    for (int r = -1; r < 2; r++)
                    {
                        for (int c = -1; c < 2; c++)
                        {
                            if ((row + r >= 0 && row + r < maxrow) && (column + c >= 0 && column + c < maxcol))
                            {
                                if (!(r == 0 && r == c))
                                {
                                    occupiedseats += grid[row + r, column + c] == '#' ? 1 : 0;
                                }
                            }
                        }
                    }

                    if (((occupiedseats == 0 && grid[row, column] == 'L') || (occupiedseats > 3 && grid[row, column] == '#')))
                    {
                        changes.Add(new KeyValuePair<int, int>(row, column));
                    }
                }
            }

            if (changes.Count == 0)
            {
                return null;
            }

            foreach (var change in changes)
            {
                grid[change.Key, change.Value] = grid[change.Key, change.Value] == '#' ? 'L' : '#';
            }

            return grid;
        }

        public static char[,] RunGameOfLifeAdvanced(char[,] grid)
        {
            List<KeyValuePair<int, int>> changes = new();
            int maxrow = grid.GetLength(0);
            int maxcol = grid.GetLength(1);

            //get grid changes
            for (int row = 0; row < grid.GetLength(0); row++)
            {
                for (int column = 0; column < grid.GetLength(1); column++)
                {
                    if (grid[row, column] == '.')
                    {
                        continue;
                    }

                    int occupiedseats = 0;

                    //raycheck
                    occupiedseats += FindOccupiedSeat(grid, row, column, -1, -1); //up left
                    occupiedseats += FindOccupiedSeat(grid, row, column, -1, 0); //up up
                    occupiedseats += FindOccupiedSeat(grid, row, column, -1, 1); //up right

                    occupiedseats += FindOccupiedSeat(grid, row, column, 0, -1); //left left
                    occupiedseats += FindOccupiedSeat(grid, row, column, 0, 1); //up up

                    occupiedseats += FindOccupiedSeat(grid, row, column, 1, -1); //down left
                    occupiedseats += FindOccupiedSeat(grid, row, column, 1, 0); //down down
                    occupiedseats += FindOccupiedSeat(grid, row, column, 1, 1); //down right

                    if (((occupiedseats == 0 && grid[row, column] == 'L') || (occupiedseats > 4 && grid[row, column] == '#')))
                    {
                        changes.Add(new KeyValuePair<int, int>(row, column));
                    }
                }
            }

            if (changes.Count == 0)
            {
                return null;
            }

            foreach (var change in changes)
            {
                grid[change.Key, change.Value] = grid[change.Key, change.Value] == '#' ? 'L' : '#';
            }

            return grid;
        }

        public static int FindOccupiedSeat(char[,] grid, int startrow, int startcol, int rowslope, int colslope)
        {
            int maxrow = grid.GetLength(0);
            int maxcol = grid.GetLength(1);

            int r = rowslope;
            int c = colslope;

            while ((startrow + r >= 0 && startrow + r < maxrow) && (startcol + c >= 0 && startcol + c < maxcol))
            {
                if (grid[startrow + r, startcol + c] != '.')
                {
                    return grid[startrow + r, startcol + c] == '#' ? 1 : 0 ;
                }

                r += rowslope;
                c += colslope;
            }

            return 0;
        }
    }
}
