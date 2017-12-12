using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Collections.Generic;

namespace adventofcode2017
{
    [TestClass]
    public class Day12Part2
    {
        [TestMethod]
        public void TestRun()
        {
            var input = System.IO.File.ReadAllLines("Day12TestInput.txt");
            Assert.AreEqual(2, new Program().RunChallenge(input));
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

            private List<List<PipeProgram>> groups { get; set; } = new List<List<PipeProgram>>();


            public int RunChallenge(string[] input)
            {
                programs = input.ToList().Select(z => new PipeProgram
                {
                    name = int.Parse(z.Substring(0, z.IndexOf('<') - 1)),
                    connectedPrograms = z.Substring(z.IndexOf('>') + 1).Split(',').ToList().Select(x => int.Parse(x.Trim())).ToList()
                }).ToList();

                // While there are still programs not yet assigned to a group
                while(programs.Any())
                {
                    var newGroup = new List<PipeProgram>();
                    groups.Add(newGroup);
                    var firstInGroup = programs.First();
                    newGroup.Add(firstInGroup);
                    programs.Remove(firstInGroup);
                    FindConnectedProgram(firstInGroup, newGroup);
                }

                return groups.Count();
            }

            private void FindConnectedProgram(PipeProgram parent, List<PipeProgram> group)
            {
                parent.connectedPrograms.ForEach( z => {
                    if(!group.Any(p => p.name == z)) {
                        var childProgram = programs.First(x => x.name == z);
                        group.Add(childProgram);
                        programs.Remove(childProgram);
                        FindConnectedProgram(childProgram, group);
                    }
                });
            }
        }
    }
}
