using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Collections.Generic;

namespace adventofcode2017
{
    [TestClass]
    public class Day11Part2
    {
        [TestMethod]
        public void RealRun()
        {
            var input = System.IO.File.ReadAllLines("Day11Input.txt").First();
            Console.WriteLine("Result: " + new Program().RunChallenge(input));
        }

        private class Program
        {

            public int RunChallenge(string input)
            {
                var instructions = input.Split(',');
                var largestNumberOfSteps = 0;

                var x = 0;
                var y = 0;

                instructions.ToList().ForEach(z =>
                {
                    switch(z)
                    {
                        case "ne":
                            x++;
                            y++;
                            break;
                        case "se":
                            x++;
                            y--;
                            break;
                        case "sw":
                            x--;
                            y--;
                            break;
                        case "nw":
                            x--;
                            y++;
                            break;
                        case "n":
                            y = y + 2;
                            break;
                        case "s":
                            y = y - 2;
                            break;
                    }

                    var stepsBack = countNumberOfStepsBack(x, y);

                    if (stepsBack > largestNumberOfSteps)
                    {
                        largestNumberOfSteps = stepsBack;
                    }
                });
               
                return largestNumberOfSteps;
            }

            private int countNumberOfStepsBack(int startingX, int startingY)
            {
                var x = startingX;
                var y = startingY;

                var steps = 0;
                while (x != 0 || y != 0)
                {
                    steps++;

                    // South or North
                    if (x == 0)
                    {
                        if (y > 0)
                        {
                            y = y - 2;
                        }
                        else
                        {
                            y = y + 2;
                        }
                    }
                    // West
                    else if (x < 0)
                    {
                        // Northwest
                        if (y > 0)
                        {
                            // Go Southeast
                            y--;
                            x++;
                        }
                        // Southwest
                        else
                        {
                            // Go Northeast
                            y++;
                            x++;
                        }
                    }
                    // East
                    else
                    {
                        // Southeast
                        if (y < 0)
                        {
                            // Go Northwest
                            y++;
                            x--;

                        }
                        // Northeast
                        else
                        {
                            // Go southwest
                            y--;
                            x--;
                        }
                    }
                }

                return steps;
            }
        }
    }
}
