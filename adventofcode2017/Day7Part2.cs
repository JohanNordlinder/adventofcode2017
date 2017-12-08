using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Collections.Generic;

namespace adventofcode2017
{
    [TestClass]
    public class Day7Part2
    {
        private class Program
        {
            public string name { get; set; }
            public int ownWeight { get; set; }
            public int weightWithParents { get; set; }
            public List<string> parentPrograms { get; set; }
        }

        [TestMethod]
        public void TestRun()
        {
            var input = System.IO.File.ReadAllLines("Day7TestInput.txt");
            RunChallenge(input);
        }

        [TestMethod]
        public void RealRun()
        {
            var input = System.IO.File.ReadAllLines("Day7Input.txt");
            RunChallenge(input);
        }

        private static void RunChallenge(string[] input)
        {
            var programs = input.Select(z => new Program
            {
                name = z.Substring(0, z.IndexOf(' ')),
                ownWeight = int.Parse(z.Substring(z.IndexOf('(') + 1, z.LastIndexOf(')') - z.IndexOf('(') - 1)),
                parentPrograms = !z.Contains(">") ? new List<string>() : z.Substring(z.IndexOf('>') + 2).Split(',').Select(x => x.Trim()).ToList()
            }).ToList();


            var currentProgram = programs.First();
            while (programs.Any(z => z.parentPrograms.Any(x => x == currentProgram.name)))
            {
                Console.WriteLine("Current Program is: " + currentProgram.name);
                currentProgram = programs.First(z => z.parentPrograms.Any(x => x == currentProgram.name));
            }

            Console.WriteLine("Bottom of the tower: " + currentProgram.name);

            var totalSumma = programs.Sum(z => z.ownWeight);

            // Find the one that is unbalanced
            CalulateWeightsWithParents(currentProgram, programs);

            if (totalSumma != currentProgram.weightWithParents)
            {
                throw new Exception("Nu är det fel");
            }

            FindNotBalanced(currentProgram, programs);
        }

        private static Program FindNotBalanced(Program program, List<Program> programs)
        {
            var parentPrograms = program.parentPrograms.Select(z => programs.First(x => x.name == z)).ToList();

            var missBalanced = parentPrograms.Where(z => parentPrograms.Count(x => x.weightWithParents == z.weightWithParents) == 1);

            if (!missBalanced.Any())
            {
                // If there are not missbalanced parents return current program
                return program;
            }

            var notBalanced = FindNotBalanced(missBalanced.First(), programs);

            // If there were no missbalanced parents
            if (notBalanced == missBalanced.First())
            {
                Console.WriteLine("Found not balanced: " + notBalanced.name);
                Console.WriteLine("It has own weight: " + notBalanced.ownWeight);
                Console.WriteLine("It has weight with children: " + notBalanced.weightWithParents);
                Console.WriteLine("The other parent programs has weight with children: " + parentPrograms.First(z => z.name != notBalanced.name).weightWithParents);
                Console.WriteLine("So this program need to have weight: " + (notBalanced.ownWeight - Math.Abs(parentPrograms.First(z => z.name != notBalanced.name).weightWithParents - notBalanced.weightWithParents)));
            }

            return notBalanced;
        }
 
        private static int CalulateWeightsWithParents(Program program, List<Program> programs)
        {
            var parentPrograms = program.parentPrograms.Select(z => programs.First(x => x.name == z)).ToList();

            if (parentPrograms.Any())
            {
                program.weightWithParents = program.ownWeight + parentPrograms.Select(p => CalulateWeightsWithParents(p, programs)).Sum();
                return program.weightWithParents;
            }

            return program.ownWeight;
        }
    }
}
