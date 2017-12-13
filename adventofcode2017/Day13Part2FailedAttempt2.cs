using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Collections.Generic;

namespace adventofcode2017
{
    [TestClass]
    public class Day13Part2FailedAttempt2
    {
        [TestMethod]
        public void TestRun()
        {
            var input = System.IO.File.ReadAllLines("Day13TestInput.txt");
            Assert.AreEqual(10, new Program().RunChallenge(input));
        }

        // [TestMethod] // Disabled as performance is too low for it to ever complete
        public void RealRun()
        {
            var input = System.IO.File.ReadAllLines("Day13Input.txt");
            Console.WriteLine("Result: " + new Program().RunChallenge(input));
        }

        private class Program
        {
          
            private class Scanner
            {
                public enum Direction
                {
                    UP, DOWN
                }

                public int depth { get; set; }
                public int range { get; set; }
                public int currentPosition { get; set; } = 1;
                public Direction direction { get; set; } = Direction.DOWN;
            }

            public int RunChallenge(string[] input)
            {

                var scanners = input.ToList().Select(z => new Scanner
                {
                    depth = int.Parse(z.Substring(0, z.IndexOf(':'))),
                    range = int.Parse(z.Substring(z.IndexOf(':') + 1).Trim())
                }).ToList();

                var maxDepth = scanners.Max(z => z.depth) + 1;
                var scannerPositions = new int[maxDepth];
                var scannerLevels = new int[maxDepth];
                var scannerDirectionsUp = new bool[maxDepth];

                for (int i = 0; i < maxDepth; i++)
                {
                    var scanner = scanners.FirstOrDefault(z => z.depth == i);
                    scannerLevels[i] = scanner != null ? scanner.range : -1;
                    scannerPositions[i] = scanner != null ? 1 : -1;
                    scannerDirectionsUp[i] = true;
                }

                var initialDelay = 0;

                while (RunAttempt(ref scannerPositions, ref scannerLevels, ref scannerDirectionsUp, initialDelay))
                {
                    initialDelay++;
                    for(int i = 0; i < scannerPositions.Length; i++)
                    {
                        if(scannerLevels[i] != -1) {
                            scannerPositions[i] = 1;
                            scannerDirectionsUp[i] = true;
                        }
                    }
                }

                return initialDelay;
            }

            private bool RunAttempt(ref int[] scannerPositions, ref int[] scannerLevels, ref bool[] scannerDirectionsUp, int initialDelay)
            {
                var caught = false;

                for(int i = 0; i < initialDelay; i++)
                {
                    moveScanners(ref scannerPositions, ref scannerLevels, ref scannerDirectionsUp);
                }

                for (int i = 0; i < scannerPositions.Length; i++)
                {
                    if (scannerPositions[i] == 1)
                    {
                        caught = true;
                        break;
                    }
                    moveScanners(ref scannerPositions, ref scannerLevels, ref scannerDirectionsUp);
                }

                return caught;
            }

            private void moveScanners(ref int[] scannerPositions, ref int[] scannerLevels, ref bool[] scannerDirectionsUp)
            {
                for(int i = 0; i < scannerPositions.Length; i++)
                {
                    if(scannerPositions[i] != -1)
                    {
                        scannerPositions[i] = scannerDirectionsUp[i] ? scannerPositions[i] + 1 : scannerPositions[i] - 1;

                        if (scannerPositions[i] == scannerLevels[i])
                        {
                            scannerDirectionsUp[i] = false;
                        }
                        else if (scannerPositions[i] == 1)
                        {
                            scannerDirectionsUp[i] = true;
                        }
                    }
                }
            }
        }
    }
}
