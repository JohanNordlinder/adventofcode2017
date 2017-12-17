using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Collections.Generic;

namespace adventofcode2017
{
    [TestClass]
    public class Day17Part1
    {
        [TestMethod]
        public void TestRun()
        {
            Assert.AreEqual(638, new Program().RunChallenge(3));
        }

        [TestMethod]
        public void RealRun()
        {
            Console.WriteLine("Result: " + new Program().RunChallenge(314));
        }

        public class Program
        {
            public int RunChallenge(int input)
            {
                var numbers = new List<int>();
                var currentPosition = 0;
                numbers.Add(0);
                for(int i = 1; i <= 2017; i++)
                {
                    currentPosition = ((currentPosition + input) % numbers.Count()) + 1;
                    numbers.Insert(currentPosition, i);
                }

                var result = numbers[numbers.IndexOf(2017) + 1];
                return result;
            }
        }
    }
}
