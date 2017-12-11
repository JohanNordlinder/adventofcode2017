using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Collections.Generic;

namespace adventofcode2017
{
    [TestClass]
    public class Day11Part1
    {
        [TestMethod]
        public void TestRun()
        {
            Assert.AreEqual(3, new Program().RunChallenge("ne,ne,ne"));
            Console.WriteLine("-----------");
            Assert.AreEqual(0, new Program().RunChallenge("ne,ne,sw,sw"));
            Console.WriteLine("-----------");
            Assert.AreEqual(2, new Program().RunChallenge("ne,ne,s,s"));
            Console.WriteLine("-----------");
            Assert.AreEqual(3, new Program().RunChallenge("se,sw,se,sw,sw"));
        }

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
                    Console.WriteLine("x: " + x + " y: " + y);
                });

                Console.WriteLine("Moving back");

                var steps = 0;
                while(x != 0 || y != 0)
                {
                    steps++;

                    // South or North
                    if(x == 0)
                    {
                        if(y > 0)
                        {
                            y = y - 2;
                        } else
                        {
                            y = y + 2;
                        }
                    }
                    // West
                    else if(x < 0)
                    {
                        // Northwest
                        if(y > 0)
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
                        if(y < 0)
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
                    Console.WriteLine("x: " + x + " y: " + y);
                }

                return steps;
            }
        }
    }
}
