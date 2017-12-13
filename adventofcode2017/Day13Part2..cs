using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Collections.Generic;

namespace adventofcode2017
{
    [TestClass]
    public class Day13Part
    {
        [TestMethod]
        public void TestRun()
        {
            var input = System.IO.File.ReadAllLines("Day13TestInput.txt");
            Assert.AreEqual(10, new Program().RunChallenge(input));
        }

        [TestMethod]
        public void RealRun()
        {
            var input = System.IO.File.ReadAllLines("Day13Input.txt");
            Console.WriteLine("Result: " + new Program().RunChallenge(input));
        }

        private class Program
        {
          
            private class Scanner
            {
                public int depth { get; set; }
                public int range { get; set; }
            }

            public int RunChallenge(string[] input)
            {

                var scanners = input.ToList().Select(z => new Scanner
                {
                    depth = int.Parse(z.Substring(0, z.IndexOf(':'))),
                    range = int.Parse(z.Substring(z.IndexOf(':') + 1).Trim())
                }).ToList();

                var maxDepth = scanners.Max(z => z.depth) + 1;
                var scannerLevels = new int[maxDepth];

                for (int i = 0; i < maxDepth; i++)
                {
                    var scanner = scanners.FirstOrDefault(z => z.depth == i);
                    scannerLevels[i] = scanner != null ? scanner.range : -1;
                }

                var initialDelay = 0;

                while (RunAttempt(maxDepth, scannerLevels, initialDelay))
                {
                    initialDelay++;
                }

                return initialDelay;
            }

            private bool RunAttempt(int maxDepth, int[] scannerLevels, int initialDelay)
            {
                var caught = false;

                for (int i = 0; i < maxDepth; i++)
                {
                    if (scannerLevels[i] != -1 && (initialDelay + i) % ((scannerLevels[i] - 1) * 2) == 0)
                    {
                        caught = true;
                        break;
                    }
                }

                return caught;
            }
        }
    }
}
