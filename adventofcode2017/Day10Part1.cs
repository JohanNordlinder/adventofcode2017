using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Collections.Generic;

namespace adventofcode2017
{
    [TestClass]
    public class Day10Part1
    {
        [TestMethod]
        public void TestRun()
        {
            Assert.AreEqual(12, new Program().RunChallenge(5, new int[] { 3, 4, 1, 5 }));
        }

        [TestMethod]
        public void RealRun()
        {
            var result = new Program().RunChallenge(256, new int[] { 31, 2, 85, 1, 80, 109, 35, 63, 98, 255, 0, 13, 105, 254, 128, 33 });
            Console.Write("Result: " + result);
        }

        private class Program
        {
            public int RunChallenge(int arraySize, int[] input)
            {
                var list = Enumerable.Range(0, arraySize).ToArray();
                var index = 0;
                var skipCount = 0;

                input.ToList().ForEach(z =>
                {
                    var toRevese = new int[z];

                    Console.WriteLine("Before: " + string.Join(",", list));

                    for (int i = 0; i < z; i++)
                    {
                        toRevese[i] = list[(index + i) % list.Length];
                    }

                    for (int i = 0; i < z; i++)
                    {
                        list[(index + i) % list.Length] = toRevese.Reverse().ElementAt(i);
                    }

                    Console.WriteLine("After: " + string.Join(",", list));
                    index = (index + skipCount + z) % list.Length;
                    skipCount++;
                });

                return list[0] * list[1];
            }
        }
    }
}
