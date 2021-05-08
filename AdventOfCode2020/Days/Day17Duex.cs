using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    public static class Day17Duex
    {
        public static void RunDay()
        {
            Console.WriteLine("Day 17");

            //Part1();
            Part2();
            Console.WriteLine("**************");
            Console.WriteLine(Environment.NewLine);
        }

        public static void Part1()
        {
            var lines = Utilities.GetLinesFromFile("day17.txt");
            var cubes = new List<conwaycube3d>();


            for (int i = -15; i < 15; i++)
            {
                for (int j = -15; j < 15; j++)
                {
                    for (int k = -15; k < 15; k++)
                    {
                        cubes.Add(new conwaycube3d() { X = i, Y = j, Z = k, IsActive = false, grid = cubes });
                    }
                }
            }

            for (int i = 0; i < lines.Length; i++)
            {
                for (int j = 0; j < lines[i].Length; j++)
                {
                    var cube = cubes.First(x => x.Z == 0 && x.X == i && x.Y == j);
                    cube.IsActive = lines[i][j] == '#';
                }
            }

            for (int i = 0; i < 6; i++)
            {
                var list = new List<Task>();
                foreach (var cube in cubes)
                {
                    var t = new Task(() =>
                    {
                        cube.GetNextState();
                    });
                    list.Add(t);
                    t.Start();
                }
                Task.WaitAll(list.ToArray());

                list = new List<Task>();
                foreach (var cube in cubes)
                {
                    var t = new Task(() =>
                    {
                        cube.Update();
                    });
                    list.Add(t);
                    t.Start();
                }
                Task.WaitAll(list.ToArray());

                Console.WriteLine(cubes.Count(x => x.IsActive));
            }

            Console.WriteLine(cubes.Count(x => x.IsActive));
        }

        public static void Part2()
        {
            var lines = Utilities.GetLinesFromFile("day17.txt");

            var cubes = new HashSet<(int x, int y, int z, int w)>();

            for (int i = 0; i < lines.Length; i++)
            {
                for (int j = 0; j < lines[i].Length; j++)
                {
                    if (lines[i][j] == '#')
                    {
                        cubes.Add((j,i,0,0));
                    }
                }
            }

            for (int i = 0; i < 6; i++)
            {
                Console.WriteLine(cubes.Count);
                //PrintGrid(cubes);
                iterate(cubes);
                

            }


            Console.WriteLine(cubes.Count);
        }


        public static void iterate(HashSet<(int x, int y, int z, int w)> cubes)
        {
            var removelist = new List<(int x, int y, int z, int w)>();
            var addlist = new List<(int x, int y, int z, int w)>();

            var globalneighbors = new List<(int x, int y, int z, int w)>();

            foreach (var cube in cubes)
            {
                var neighboors = new HashSet<(int x, int y, int z, int w)>();

                for (int i = -1; i < 2; i++)
                {
                    for (int j = -1; j < 2; j++)
                    {
                        for (int k = -1; k < 2; k++)
                        {
                            for (int l = -1; l < 2; l++)
                            {
                                if (!(i == 0 && j == 0 && k == 0 && l == 0))
                                {
                                    neighboors.Add((cube.x + i, cube.y + j, cube.z + k, cube.w + l));
                                }
                            }
                        }
                    }
                }

                var matches = cubes.Intersect(neighboors).Count();

                if (!(matches == 2 || matches == 3))
                {
                    removelist.Add(cube);
                }

                foreach (var item in cubes)
                {
                    neighboors.Remove(item);
                }

                globalneighbors.AddRange(neighboors);

            }

            foreach (var neighbor in globalneighbors.Distinct())
            {

                var actives = 0;

                for (int i = -1; i < 2; i++)
                {
                    for (int j = -1; j < 2; j++)
                    {
                        for (int k = -1; k < 2; k++)
                        {
                            for (int l = -1; l < 2; l++)
                            {
                                if (!(i == 0 && j == 0 && k == 0 && l == 0))
                                {
                                    if (cubes.Contains((neighbor.x + i, neighbor.y + j, neighbor.z + k, neighbor.w + l)))
                                    {
                                        actives++;
                                    }
                                }
                            }
                        }
                    }
                }

                if (actives == 3)
                {
                    addlist.Add(neighbor);
                }
            }

            foreach (var item in removelist)
            {
                cubes.Remove(item);
            }

            foreach (var item in addlist)
            {
                cubes.Add(item);
            }
        }

        public class conwaycube3d
        {

            public int X { get; set; }
            public int Y { get; set; }
            public int Z { get; set; }
            public int w { get; set; }

            public bool IsActive { get; set; }

            protected bool NextState { get; set; }

            public List<conwaycube3d> grid { get; set; }

            public virtual void GetNextState()
            {
                var neighboors = new List<(int, int, int)>();

                var activeneighbors = 0;

                for (int i = -1; i < 2; i++)
                {
                    for (int j = -1; j < 2; j++)
                    {
                        for (int k = -1; k < 2; k++)
                        {
                            if (!(i == 0 && j == 0 && k == 0))
                            {
                                neighboors.Add((X + i, Y + j, Z + k));
                            }
                        }
                    }
                }

                foreach (var n in neighboors)
                {
                    if (grid.FirstOrDefault(x => x.X == n.Item1 && x.Y == n.Item2 && x.Z == n.Item3)?.IsActive == true)
                    {
                        activeneighbors++;
                    }
                }

                if (IsActive)
                {
                    if (activeneighbors == 2 || activeneighbors == 3)
                    {
                        NextState = true;
                    }
                    else
                    {
                        NextState = false;
                    }
                }
                else
                {
                    if (activeneighbors == 3)
                    {
                        NextState = true;
                    }
                    else
                    {
                        NextState = false;
                    }
                }
            }

            public void Update()
            {
                IsActive = NextState;
            }
        }
    }
}
