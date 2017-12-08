using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Collections.Generic;

namespace adventofcode2017
{
    [TestClass]
    public class Day7Part1
    {
        private class Program
        {
            public string name { get; set; }
            public int weight { get; set; }
            public List<string> parentPrograms { get; set; }
        }

        [TestMethod]
        public void TestRun()
        {
            var input = System.IO.File.ReadAllLines("Day7TestInput.txt");
            var result = RunChallenge(input);
            Assert.AreEqual("tknk", RunChallenge(input));
        }

        [TestMethod]
        public void RealRun()
        {
            var input = System.IO.File.ReadAllLines("Day7Input.txt");
            var result = RunChallenge(input);
            Console.Write("Result: " + result);
        }

        private static string RunChallenge(string[] input)
        {
            var programs = input.Select(z => new Program
            {
                name = z.Substring(0, z.IndexOf(' ')),
                weight = int.Parse(z.Substring(z.IndexOf('(') + 1, z.LastIndexOf(')') - z.IndexOf('(') - 1)),
                parentPrograms = !z.Contains(">") ? new List<string>() : z.Substring(z.IndexOf('>') + 2).Split(',').Select(x => x.Trim()).ToList()
            }).ToList();


            var currentProgram = programs.First();
            while (programs.Any(z => z.parentPrograms.Any(x => x == currentProgram.name)))
            {
                Console.WriteLine("Current Program is: " + currentProgram.name);
                currentProgram = programs.First(z => z.parentPrograms.Any(x => x == currentProgram.name));
            }

            return currentProgram.name;
        }
    }
}
