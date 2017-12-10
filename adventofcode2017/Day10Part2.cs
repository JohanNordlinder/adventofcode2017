using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Collections.Generic;

namespace adventofcode2017
{
    [TestClass]
    public class Day10Part2
    {
        [TestMethod]
        public void TestRun()
        {
            Assert.AreEqual("a2582a3a0e66e6e86e3812dcb672a272", new Program().RunChallenge(""));
            Assert.AreEqual("33efeb34ea91902bb2f59c9920caa6cd", new Program().RunChallenge("AoC 2017"));
            Assert.AreEqual("3efbe78a8d82f29979031a4aa0b16a9d", new Program().RunChallenge("1,2,3"));
            Assert.AreEqual("63960835bcdc130f0b66d7ff4f6a5a8e", new Program().RunChallenge("1,2,4"));
        }

        [TestMethod]
        public void RealRun()
        {
            var result = new Program().RunChallenge("31,2,85,1,80,109,35,63,98,255,0,13,105,254,128,33");
            Console.Write("Result: " + result);
        }

        private class Program
        {
            public string RunChallenge(string rawInput)
            {
                var input = rawInput.Select(z => (int)z).ToList();
                input.AddRange(new int[] { 17, 31, 73, 47, 23 });

                var list = Enumerable.Range(0, 256).ToArray();
                var index = 0;
                var skipCount = 0;

                for (var x = 0; x < 64; x++) { 
                    input.ToList().ForEach(z =>
                    {
                        var toRevese = new int[z];

                        Console.WriteLine("Before: " + string.Join(",", list));

                        for (var i = 0; i < z; i++)
                        {
                            toRevese[i] = list[(index + i) % list.Length];
                        }

                        for (var i = 0; i < z; i++)
                        {
                            list[(index + i) % list.Length] = toRevese.Reverse().ElementAt(i);
                        }

                        Console.WriteLine("After: " + string.Join(",", list));
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
