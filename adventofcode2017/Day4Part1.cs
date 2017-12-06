using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace adventofcode2017
{
    [TestClass]
    public class Day4Part1
    {
        [TestMethod]
        public void TestRun()
        {
            Assert.AreEqual(1, RunChallenge(new string []{ "aa bb cc dd" }));
            Assert.AreEqual(0, RunChallenge(new string[] { "aa bb cc dd aa" }));
            Assert.AreEqual(1, RunChallenge(new string[] { "aa bb cc dd aaa" }));
        }

        [TestMethod]
        public void RealRun()
        {
            var input = System.IO.File.ReadAllLines("Day4Input.txt");
            var result = RunChallenge(input);
            Console.Write("Result: " + result);
        }

        private static int RunChallenge(string[] input)
        {
            var validLines = input.Select(line => line.Split(' ')).Where(line => line.All(word => line.Count(otherWord => word == otherWord) == 1)).Count();
            return validLines;
        }
    }
}
