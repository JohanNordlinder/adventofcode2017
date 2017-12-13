using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Collections.Generic;

namespace adventofcode2017
{
    [TestClass]
    public class Day13Part2FailedAttempt1
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

                var initialDelay = 0;

                var numberOfSteps = scanners.Max(z => z.depth);

                while (RunAttempt(scanners, initialDelay, numberOfSteps))
                {
                    initialDelay++;
                    scanners.ForEach(z =>
                    {
                        z.currentPosition = 1;
                        z.direction = Scanner.Direction.DOWN;
                    });
                }

                return initialDelay;
            }

            private bool RunAttempt(List<Scanner> scanners, int initialDelay, int numberOfSteps)
            {
                var caught = false;

                for(int i = 0; i < initialDelay; i++)
                {
                    moveScanners(scanners);
                }

                for (int i = 0; i <= numberOfSteps; i++)
                {
                    var scannerAtThisDept = scanners.FirstOrDefault(z => z.depth == i);
                    if (scannerAtThisDept != null && scannerAtThisDept.currentPosition == 1)
                    {
                        caught = true;
                        break;
                    }
                    moveScanners(scanners);
                }

                return caught;
            }

            private void moveScanners(List<Scanner> scanners)
            {
                scanners.ForEach(z =>
                {
                    z.currentPosition = z.direction == Scanner.Direction.DOWN ? z.currentPosition + 1 : z.currentPosition - 1;

                    if (z.currentPosition == z.range)
                    {
                        z.direction = Scanner.Direction.UP;
                    }
                    else if (z.currentPosition == 1)
                    {
                        z.direction = Scanner.Direction.DOWN;
                    }
                });
            }
        }
    }
}
