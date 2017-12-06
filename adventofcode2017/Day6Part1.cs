using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Collections.Generic;

namespace adventofcode2017
{
    [TestClass]
    public class Day6Part1
    {
        private class Instruction
        {
            public int index { get; set; }
            public int offset { get; set; }
        }

        [TestMethod]
        public void TestRun()
        {
            var banks = new int[] { 0, 2, 7, 0 };
            Assert.AreEqual(5, RunChallenge(banks));
        }

        [TestMethod]
        public void RealRun()
        {
            var banks = new int[] { 2, 8, 8, 5, 4, 2, 3, 1, 5, 5, 1, 2, 15, 13, 5, 14 };
            var result = RunChallenge(banks);
            Console.Write("Result: " + result);
        }

        private static int RunChallenge(int[] banks)
        {
            List<string> alreadySeenConfigurations = new List<string>();
            int numberOfJumps = 0;
            do
            {
                Console.WriteLine("CurrentMemory: " + string.Join(",", banks));

                var biggestBank = new { blocks = banks.Max(), index = banks.ToList().IndexOf(banks.Max()) };
                Console.WriteLine("Biggest: " + biggestBank.index);

                var blockToDistribute = biggestBank.blocks;
                banks[biggestBank.index] = 0;
                for (int i = 1; i <= blockToDistribute; i++)
                {
                    banks[(biggestBank.index + i) % banks.Length]++;
                }
                Console.WriteLine("Reallocation complete: " + string.Join(",", banks));

                alreadySeenConfigurations.Add(string.Join(",", banks));
                numberOfJumps++;

            } while (alreadySeenConfigurations.Distinct().Count() == alreadySeenConfigurations.Count());

            return numberOfJumps;
        }
    }
}
