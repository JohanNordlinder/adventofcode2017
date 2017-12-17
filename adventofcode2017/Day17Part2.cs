using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Collections.Generic;

namespace adventofcode2017
{
    [TestClass]
    public class Day17Part2
    {
        [TestMethod]
        public void RealRun()
        {
            Console.WriteLine("Result: " + new Program().RunChallenge(314));
        }

        public class Program
        {
            public int RunChallenge(int input)
            {
                var currentPosition = 0;
                var count = 1;
                var lastFirst = -1;
                for(int i = 1; i <= 50000000; i++)
                {
                    currentPosition = ((currentPosition + input) % count) + 1;
                    count++;
                    if (currentPosition == 1) {
                        lastFirst = i;
                    }
                }

                return lastFirst;
            }
        }
    }
}
