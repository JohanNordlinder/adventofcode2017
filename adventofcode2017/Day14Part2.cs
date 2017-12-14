using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Collections.Generic;

namespace adventofcode2017
{
    [TestClass]
    public class Day14Part2
    {
        [TestMethod]
        public void TestRun()
        {
            var program = new Program();
            Assert.AreEqual(1242, new Program().RunChallenge("flqrgnkx"));
        }

        [TestMethod]
        public void RealRun()
        {
            Console.WriteLine("Result: " + new Program().RunChallenge("hfdlxzhv"));
        }

        private class Program
        {
            class Square
            {
                public int column { get; set; }
                public int row { get; set; }
                public int region { get; set; }
            }

            public int RunChallenge(string input)
            {
                var grid = new List<List<int>>();

                var regions = new List<Square>();

                for(int row = 0; row < 128; row++)
                {
                    var mask = ToBinary(KnotHash(input + "-" + row)).ToCharArray();
                    for(int column = 0; column < 128; column++)
                    {
                        if(mask[column] == '1') {
                            var square = regions.FirstOrDefault(z =>
                                (z.column == column && (z.row == row - 1 || z.row == row + 1)) || 
                                (z.row == row && (z.column == column - 1 || z.column == column + 1))
                            );
                            if(square != null)
                            {
                                regions.Add(new Square { row = row, column = column, region = square.region });
                            } else
                            {
                                var max = regions.Any() ? regions.Max(z => z.region) : 0;
                                regions.Add(new Square { row = row, column = column, region = max + 1});
                            }
                        }

                    }
                }

                return regions.Select(z => z.region).Distinct().Count();
            }

            public string ToBinary(String input)
            {
                string binarystring = String.Join(String.Empty,
                      input.Select(
                        c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')
                      )
                    );
                return binarystring;
            }

            public string KnotHash(string rawInput)
            {
                var input = rawInput.Select(z => (int)z).ToList();
                input.AddRange(new int[] { 17, 31, 73, 47, 23 });

                var list = Enumerable.Range(0, 256).ToArray();
                var index = 0;
                var skipCount = 0;

                for (var x = 0; x < 64; x++)
                {
                    input.ToList().ForEach(z =>
                    {
                        var toRevese = new int[z];

                        for (var i = 0; i < z; i++)
                        {
                            toRevese[i] = list[(index + i) % list.Length];
                        }

                        for (var i = 0; i < z; i++)
                        {
                            list[(index + i) % list.Length] = toRevese.Reverse().ElementAt(i);
                        }

                        index = (index + skipCount + z) % list.Length;
                        skipCount++;
                    });
                }

                var sparseHash = list.ToList();
                var denseHash = "";

                for (var x = 0; x < 16; x++)
                {
                    var hashPart = sparseHash.Take(16).Aggregate((current, next) => current ^ next);
                    denseHash = denseHash + hashPart.ToString("X2");
                    sparseHash.RemoveRange(0, 16);
                }

                return denseHash.ToLower();
            }
        }


    }
}
