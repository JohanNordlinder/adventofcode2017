﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace adventofcode2017
{
    [TestClass]
    public class Day2Part2
    {
        [TestMethod]
        public void TestRun()
        {
            string[] input = {
                "5 9 2 8",
                "9 4 7 3",
                "3 8 6 5"
            };
            Assert.AreEqual(RunChallenge(input), 9);
        }

        [TestMethod]
        public void RealRun()
        {
            string[] input = {
                "4347 3350 196 162 233 4932 4419 3485 4509 4287 4433 4033 207 3682 2193 4223",
                "648 94 778 957 1634 2885 1964 2929 2754 89 972 112 80 2819 543 2820",
                "400 133 1010 918 1154 1008 126 150 1118 117 148 463 141 940 1101 89",
                "596 527 224 382 511 565 284 121 643 139 625 335 657 134 125 152",
                "2069 1183 233 213 2192 193 2222 2130 2073 2262 1969 2159 2149 410 181 1924",
                "1610 128 1021 511 740 1384 459 224 183 266 152 1845 1423 230 1500 1381",
                "5454 3936 250 5125 244 720 5059 202 4877 5186 313 6125 172 727 1982 748",
                "3390 3440 220 228 195 4525 1759 1865 1483 5174 4897 4511 5663 4976 3850 199",
                "130 1733 238 1123 231 1347 241 291 1389 1392 269 1687 1359 1694 1629 1750",
                "1590 1394 101 434 1196 623 1033 78 890 1413 74 1274 1512 1043 1103 84",
                "203 236 3001 1664 195 4616 2466 4875 986 1681 152 3788 541 4447 4063 5366",
                "216 4134 255 235 1894 5454 1529 4735 4962 220 2011 2475 185 5060 4676 4089",
                "224 253 19 546 1134 3666 3532 207 210 3947 2290 3573 3808 1494 4308 4372",
                "134 130 2236 118 142 2350 3007 2495 2813 2833 2576 2704 169 2666 2267 850",
                "401 151 309 961 124 1027 1084 389 1150 166 1057 137 932 669 590 188",
                "784 232 363 316 336 666 711 430 192 867 628 57 222 575 622 234"
            };
            var result = RunChallenge(input);
            Console.Write("Result: " + result);
        }

        private static decimal RunChallenge(string[] rawInput)
        {
            var formatted = rawInput
                .Select(z => z.Split(' '))
                .Select(z => z.Select(x => int.Parse(x.Trim())));

            var result = formatted.Select(
                // För varje rad...
                rad => rad
                // Plocka ut...
                .Select(nummer => new
                {
                    // Varje nummer
                    nummer,
                    // Och eventuellt tillhörande nummer inom raden som inte är samma nummer men som är jämnt delbart
                    annatnummer = rad.FirstOrDefault(annatNummer => nummer != annatNummer && (Decimal.Divide(nummer, annatNummer) % 1 == 0))
                })
                // Filtrera ut de par där inget annat nummer hittades
                .Where(z => z.annatnummer != 0)
                // Ta kvoten
                .Select(z => Decimal.Divide(z.nummer, z.annatnummer))
                // Det borde bara finnas ett sådant nummer per rad
                .First()
            )
            // Summera
            .Sum();

            return result;
        }
    }
}
