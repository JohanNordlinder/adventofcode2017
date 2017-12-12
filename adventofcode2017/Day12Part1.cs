using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Collections.Generic;

namespace adventofcode2017
{
    [TestClass]
    public class Day12Part1
    {
        [TestMethod]
        public void TestRun()
        {
            var input = System.IO.File.ReadAllLines("Day12TestInput.txt");
            Assert.AreEqual(6, new Program().RunChallenge(input));
        }

        [TestMethod]
        public void RealRun()
        {
            var input = System.IO.File.ReadAllLines("Day12Input.txt");
            Console.WriteLine("Result: " + new Program().RunChallenge(input));
        }

        private class Program
        {
            private class PipeProgram
            {
                public int name { get; set; }
                public List<int> connectedPrograms { get; set; }
            }

            private List<PipeProgram> programs { get; set; } = new List<PipeProgram>();

            private List<PipeProgram> programsInGroup { get; set; } = new List<PipeProgram>();


            public int RunChallenge(string[] input)
            {
                programs = input.ToList().Select(z => new PipeProgram
                {
                    name = int.Parse(z.Substring(0, z.IndexOf('<') - 1)),
                    connectedPrograms = z.Substring(z.IndexOf('>') + 1).Split(',').ToList().Select(x => int.Parse(x.Trim())).ToList()
                }).ToList();

                programsInGroup.Add(programs.First());

                FindSubPrograms(programs.First());

                return programsInGroup.Count();
            }

            private void FindSubPrograms(PipeProgram parent)
            {
                parent.connectedPrograms.ForEach( z => {
                    if(!programsInGroup.Any(p => p.name == z)) {
                        var childProgram = programs.First(x => x.name == z);
                        programsInGroup.Add(childProgram);
                        FindSubPrograms(childProgram);
                    }
                });
            }
        }
    }
}
