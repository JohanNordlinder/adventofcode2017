using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace adventofcode2017
{
    [TestClass]
    public class Day4Part2
    {
        [TestMethod]
        public void TestRun()
        {
            Assert.AreEqual(1, RunChallenge(new string []{ "abcde fghij" }));
            Assert.AreEqual(0, RunChallenge(new string[] { "abcde xyz ecdab" }));
            Assert.AreEqual(1, RunChallenge(new string[] { "a ab abc abd abf abj" }));
            Assert.AreEqual(1, RunChallenge(new string[] { "iiii oiii ooii oooi oooo" }));
            Assert.AreEqual(0, RunChallenge(new string[] { "oiii ioii iioi iiio" }));
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
            var validLines = input
                .Select(line => line.Split(' '))
                // Sök rader där alla
                .Where(line => line.All(
                    // Ord bara har ett ord inom samma rad
                    word => 1 == line.Count(
                        // Som är samma ord
                        otherWord => word == otherWord ||
                        // Eller
                        (
                            // Där ordets alla bokstäver
                            word.ToCharArray().GroupBy(bokstav => bokstav).All(group =>
                            // Finns bland det andra ordets alla bokstäver
                            otherWord.ToCharArray().GroupBy(bokstav => bokstav).Any(otherGroup =>
                            // Med samma antal förekomster
                            group.Key == otherGroup.Key &&
                            group.Count() == otherGroup.Count()
                            )
                        ) &&
                        // Och längden är samma
                        word.Length == otherWord.Length
                    )
                )
            ));
            return validLines.Count();
        }
    }
}
