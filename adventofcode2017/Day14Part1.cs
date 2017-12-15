using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Collections.Generic;

namespace adventofcode2017
{
    [TestClass]
    public class Day14Part1
    {
        [TestMethod]
        public void TestRun()
        {
            var program = new Program();
            Assert.AreEqual("10100000110000100000000101110000", program.ToBinary("a0c20170"));
            Assert.AreEqual(8108, new Program().RunChallenge("flqrgnkx"));
        }

        [TestMethod]
        public void RealRun()
        {
            Console.WriteLine("Result: " + new Program().RunChallenge("hfdlxzhv"));
        }

        private class Program
        {
            public int RunChallenge(string input)
            {
                var grid = new List<List<int>>();

                var used = 0;
                for(int i = 0; i < 128; i++)
                {
                    var mask = ToBinary(KnotHash(input + "-" + i));
                    used = used + (mask.Replace("0", "")).Length;
                }

                return used;
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

            // Copy from day 10 part 2
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
