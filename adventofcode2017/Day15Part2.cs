using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Collections.Generic;

namespace adventofcode2017
{
    [TestClass]
    public class Day15Part2
    {
        [TestMethod]
        public void TestRun()
        {
            Assert.AreEqual(309, new Program().RunChallenge(65, 8921));
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
                var genAValues = FindValues(genAStart, 16807, 4);
                var genBValues = FindValues(genBStart, 48271, 8);

                var total = 0;

                for (int i = 0; i < 5000000; i++)
                {
                    var binA = Convert.ToString(genAValues[i], 2).PadLeft(16, '0');
                    var binB = Convert.ToString(genBValues[i], 2).PadLeft(16, '0');

                    if (binA.Substring(binA.Length - 16) == binB.Substring(binB.Length - 16))
                    {
                        total++;
                    }
                }

                return total;
            }

            private List<long> FindValues(int startValue, int multiplier, int mask)
            {
                var values = new List<long>();

                long val = startValue;

                while (values.Count() < 5000000)
                {
                    val = GenerateNext(val, multiplier);

                    if (val % mask == 0)
                    {
                        values.Add(val);

                    }
                }

                return values;
            }

            private long GenerateNext(long input, int multiplier)
            {
                var next = input * multiplier;
                var result = next % 2147483647;
                return result;
            }
        }
    }
}
