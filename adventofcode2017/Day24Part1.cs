using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace adventofcode2017
{
    [TestClass]
    public class Day24Part1
    {
        [TestMethod]
        public void TestRun()
        {
            var input = System.IO.File.ReadAllLines("Day24TestInput.txt").ToList();
            Assert.AreEqual(31, new Program().RunChallenge(input));
        }

        [TestMethod]
        public void RealRun()
        {
            var input = System.IO.File.ReadAllLines("Day24Input.txt").ToList();
            Console.WriteLine("Result: " + new Program().RunChallenge(input));
        }

        public class Component
        {
            public int PortA { get; set; }
            public int PortB { get; set; }
        }

        public class Program
        {

            public List<Component> ComponentsNotUsedFromStart { get; set; } = new List<Component>();
            public int Strongest { get; set; } = 0;

            public long RunChallenge(List<string> input)
            {
                input.ForEach(z => ComponentsNotUsedFromStart.Add(new Component {
                    PortA = int.Parse(Regex.Split(z, "/")[0]),
                    PortB = int.Parse(Regex.Split(z, "/")[1]),
                }));


                var start = ComponentsNotUsedFromStart.Where(z => z.PortA == 0 || z.PortB == 0).ToList();

                start.ForEach(z => {
                    var componentsNotUsed = new List<Component>(ComponentsNotUsedFromStart);
                    componentsNotUsed.Remove(z);
                    var weight = z.PortA + z.PortB;

                    FindNextComponent(z.PortA == 0 ? z.PortB : z.PortA, componentsNotUsed, weight);
                });

                return Strongest;
            }

            private void FindNextComponent(int portToMatch, List<Component> ComponentsNotUsedFromStart, int bridgeWeight)
            {
                var nextComponent = ComponentsNotUsedFromStart.Where(z => z.PortA == portToMatch || z.PortB == portToMatch).ToList();

                if(!nextComponent.Any() && bridgeWeight > Strongest)
                {
                    Strongest = bridgeWeight;
                }

                nextComponent.ForEach(z => {
                    var componentsNotUsed = new List<Component>(ComponentsNotUsedFromStart);
                    componentsNotUsed.Remove(z);
                    var newWeight = bridgeWeight + z.PortA + z.PortB;
                    var otherPort = z.PortA == portToMatch ? z.PortB : z.PortA;
                    FindNextComponent(otherPort, componentsNotUsed, newWeight);
                });
            }
        }
    }
}
