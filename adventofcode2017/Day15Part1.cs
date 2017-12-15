using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Collections.Generic;

namespace adventofcode2017
{
    [TestClass]
    public class Day15Part1
    {
        [TestMethod]
        public void TestRun()
        {
            Assert.AreEqual(588, new Program().RunChallenge(65, 8921));
        }

        [TestMethod]
        public void RealRun()
        {
            Console.WriteLine("Result: " + new Program().RunChallenge(873, 583));
        }

        private class Program
        {
            public int RunChallenge(int genAStart, int genBStart)
            {
                long genA = genAStart;
                long genB = genBStart;

                var total = 0;

                for(int i = 0; i < 40000000; i++)
                {
                    genA = GenerateNext(genA, 16807);
                    genB = GenerateNext(genB, 48271);

                    var binA = Convert.ToString(genA, 2).PadLeft(16, '0');
                    var binB = Convert.ToString(genB, 2).PadLeft(16, '0');

                    if (binA.Substring(binA.Length - 16) == binB.Substring(binB.Length - 16))
                    {
                        total++;
                    }
                }

                return total;
            }

            public long GenerateNext(long input, int multiplier)
            {
                var next = input * multiplier;
                var result = next % 2147483647;
                return result;
            }
        }
    }
}
