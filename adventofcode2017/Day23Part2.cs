using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace adventofcode2017
{
    [TestClass]
    public class Day23Part2
    {
        [TestMethod]
        public void RealRun()
        {
            Console.WriteLine("Result: " + new Program().RunChallenge());
        }

        public class Program
        {

            public long RunChallenge()
            {
                // See Day23Part2Analyzed.txt

                var primes = 0;
                var b = 81 * 100 + 100000;
                var c = b;
                c += 17000;
                var loops = 1;

                do
                {
                    loops++;
                    if (IsPrime(b))
                    {
                        primes++;
                    }
                    b += 17;
                } while (c != b);

                return loops - primes;
            }

            public static bool IsPrime(int number)
            {
                if (number <= 1)
                    return false;
                else if (number % 2 == 0)
                    return number == 2;

                var N = (Math.Sqrt(number) + 0.5);

                for (int i = 3; i <= N; i += 2)
                    if (number % i == 0)
                        return false;

                return true;
            }
        }
    }
}
