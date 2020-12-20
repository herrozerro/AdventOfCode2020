using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    public static class Day17
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
            var cubes = new List<conwaycube4d>();


            for (int i = -10; i < 15; i++)
            {
                for (int j = -10; j < 15; j++)
                {
                    for (int k = -10; k < 10; k++)
                    {
                        for (int l = -10; l < 10; l++)
                        {
                            cubes.Add(new conwaycube4d() { X = i, Y = j, Z = k, W = l, IsActive = false, Grid4d = cubes });
                        }
                    }
                }
            }
            var list = new List<Task>();

            for (int i = 0; i < lines.Length; i++)
            {
                for (int j = 0; j < lines[i].Length; j++)
                {
                    var cube = cubes.First(x => x.Z == 0 && x.X == i && x.Y == j && x.W == 0);
                    cube.IsActive = lines[i][j] == '#';
                }
            }

            for (int i = 0; i < 6; i++)
            {


                var activeCubes = cubes.Where(x => x.IsActive).ToList();

                var updateCubes = activeCubes.SelectMany(x => x.Neighbors).Distinct().ToList();
                updateCubes.AddRange(activeCubes);

                foreach (var cube in updateCubes)
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

        public class conwaycube3d
        {
            public int X { get; set; }
            public int Y { get; set; }
            public int Z { get; set; }


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

        public class conwaycube4d : conwaycube3d
        {
            public int W { get; set; }

            private List<conwaycube4d> _neighbors { get; set; }
            public List<conwaycube4d> Neighbors
            {
                get
                {
                    if (_neighbors == null)
                    {
                        _neighbors = GetNeighbors();
                    }
                    return _neighbors;
                }
            }

            public List<conwaycube4d> Grid4d { get; set; }

            public override void GetNextState()
            {
                //var neighboors = new List<(int, int, int, int)>();

                //var activeneighbors = 0;

                //for (int i = -1; i < 2; i++)
                //{
                //    for (int j = -1; j < 2; j++)
                //    {
                //        for (int k = -1; k < 2; k++)
                //        {
                //            for (int l = -1; l < 2; l++)
                //            {
                //                if (!(i == 0 && j == 0 && k == 0 && l == 0))
                //                {
                //                    neighboors.Add((X + i, Y + j, Z + k, W + l));
                //                }
                //            }

                //        }
                //    }
                //}

                //foreach (var n in neighboors)
                //{
                //    if (Grid4d.FirstOrDefault(x => x.X == n.Item1 && x.Y == n.Item2 && x.Z == n.Item3 && n.Item4 == x.W)?.IsActive == true)
                //    {
                //        activeneighbors++;
                //    }
                //}

                var activeneighbors = Neighbors.Count(x => x.IsActive);

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

            public List<conwaycube4d> GetNeighbors()
            {
                var neighboors = new List<(int, int, int, int)>();

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
                                    neighboors.Add((X + i, Y + j, Z + k, W + l));
                                }
                            }

                        }
                    }
                }

                var actn = new List<conwaycube4d>();

                foreach (var n in neighboors)
                {
                    actn.Add(Grid4d.FirstOrDefault(x => x.X == n.Item1 && x.Y == n.Item2 && x.Z == n.Item3 && n.Item4 == x.W));
                }

                return actn;
            }
        }
    }
}
