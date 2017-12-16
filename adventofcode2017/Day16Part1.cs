using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Collections.Generic;

namespace adventofcode2017
{
    [TestClass]
    public class Day16Part1
    {
        [TestMethod]
        public void TestRun()
        {
            var input = System.IO.File.ReadAllLines("Day16TestInput.txt").First().Split(',');
            var programs = "abcde".ToCharArray().Select(z => new Program.DancingProgram { name = z }).ToList();
            Assert.AreEqual("baedc", new Program().RunChallenge(programs, input));
        }

        [TestMethod]
        public void RealRun()
        {
            var input = System.IO.File.ReadAllLines("Day16Input.txt").First().Split(',');
            var programs = "abcdefghijklmnop".ToCharArray().Select(z => new Program.DancingProgram { name = z }).ToList();
            Console.WriteLine("Result: " + new Program().RunChallenge(programs, input));
        }

        public class Program
        {
            public class DancingProgram
            {
                public char name { get; set; }
            }

            public String RunChallenge(List<DancingProgram> programs, string[] input)
            {
                input.ToList().ForEach(z =>
                {
                    var move = z.Substring(0, 1);
                    switch (move)
                    {
                        case "s":
                            var spinCount = int.Parse(z.Substring(1));
                            var programsToSpin = programs.Take(programs.Count() - spinCount).ToList();
                            programs.RemoveRange(0, programs.Count() - spinCount);
                            programs.AddRange(programsToSpin);
                            break;
                        case "x":
                            var swapIndex1 = int.Parse(z.Substring(1).Substring(0, z.IndexOf('/') - 1));
                            var swapIndex2 = int.Parse(z.Substring(1).Substring(z.IndexOf('/')));
                            var programToSwap1 = programs[swapIndex1];
                            var programToSwap2 = programs[swapIndex2];
                            var temp = programToSwap1.name;
                            programToSwap1.name = programToSwap2.name;
                            programToSwap2.name = temp;
                            break;
                        case "p":
                            var swapName1 = z.Substring(1, 1);
                            var swapName2 = z.Substring(3, 1);
                            var programToPartner1 = programs.First(x => x.name.ToString() == swapName1);
                            var programToPartner2 = programs.First(x => x.name.ToString() == swapName2);
                            var temp2 = programToPartner1.name;
                            programToPartner1.name = programToPartner2.name;
                            programToPartner2.name = temp2;
                            break;
                    }
                });

                return string.Join("", programs.Select(z => z.name).ToArray());
            }
        }
    }
}
