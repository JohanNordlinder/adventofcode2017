using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace adventofcode2017
{
    [TestClass]
    public class Day5Part2
    {
        private class Instruction
        {
            public int index { get; set; }
            public int offset { get; set; }
        }

        [TestMethod]
        public void TestRun()
        {
            var input = "0,3,0,1,-3".Split(',');
            Assert.AreEqual(10, RunChallenge(input));
        }

        [TestMethod]
        public void RealRun()
        {
            var input = System.IO.File.ReadAllLines("Day5Input.txt");
            var result = RunChallenge(input);
            Console.Write("Result: " + result);
        }

        private static int RunChallenge(string[] input)
        {
            var instructions = input.Select((instruction, index) => new Instruction { offset = int.Parse(instruction.ToString()), index = index }).ToList();

            var currentPosition = 0;
            var numberOfJumps = 0;
            do
            {
                numberOfJumps++;
                var instruction = instructions[currentPosition];
                currentPosition = currentPosition + instruction.offset;

                if (instruction.offset >= 3)
                {
                    instruction.offset--;
                }
                else
                {
                    instruction.offset++;
                }

            } while (currentPosition >= 0 && currentPosition < instructions.Count());

            return numberOfJumps;
        }
    }
}
