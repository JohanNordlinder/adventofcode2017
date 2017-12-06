using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace adventofcode2017
{
    [TestClass]
    public class Day3Part1
    {
        [TestMethod]
        public void TestRun()
        {
            Assert.AreEqual(3, RunChallenge(12));
            Assert.AreEqual(2, RunChallenge(23));
            Assert.AreEqual(31, RunChallenge(1024));
        }

        [TestMethod]
        public void RealRun()
        {
            var result = RunChallenge(347991);
            Console.Write("Result: " + result);
        }

        private static decimal RunChallenge(int input)
        {
            var currentNumberOfCirles = 0;
            var maxNumberWithTheCurrentNumberOrCircles = 1;

            while (input >= maxNumberWithTheCurrentNumberOrCircles)
            {
                currentNumberOfCirles++;
                maxNumberWithTheCurrentNumberOrCircles = getMaxNumberByCirleNumber(currentNumberOfCirles);
            }

            Console.WriteLine("Cirkel: " + currentNumberOfCirles + " Max number for this circle: " + maxNumberWithTheCurrentNumberOrCircles);

            var antalStegInSomKrävs = currentNumberOfCirles;
            var langdFranSlutetAvEnCirkel = maxNumberWithTheCurrentNumberOrCircles - input;
            var landPaSidan = (1 + currentNumberOfCirles * 2);
            var mittenPaSidan = landPaSidan / 2 + 1;
            var landFramSlutetAvEnRad = langdFranSlutetAvEnCirkel % (landPaSidan - 1);
            var langFramMittenAvRaden = Math.Abs(mittenPaSidan - 1 - landFramSlutetAvEnRad);
            var distans = antalStegInSomKrävs + langFramMittenAvRaden;

            return distans;
        }

        private static int getMaxNumberByCirleNumber(int cirleNo)
        {
            var sidan = 1 + cirleNo * 2;
            return sidan * sidan;
        }
    }
}
