using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace adventofcode2017
{
    [TestClass]
    public class Day20Part2
    {
        [TestMethod]
        public void TestRun()
        {
            var input = System.IO.File.ReadAllLines("Day20TestInput2.txt").ToList();
            Assert.AreEqual(1, new Program().RunChallenge(input));
        }

        [TestMethod]
        public void RealRun()
        {
            var input = System.IO.File.ReadAllLines("Day20Input.txt").ToList();
            Console.WriteLine("Result: " + new Program().RunChallenge(input));
        }

        public class Program
        {
            class Particle
            {
                public int Name { get; set; }
                public int X { get; set; }
                public int Y { get; set; }
                public int Z { get; set; }
                public int VX { get; set; }
                public int VY { get; set; }
                public int VZ { get; set; }
                public int AX { get; set; }
                public int AY { get; set; }
                public int AZ { get; set; }
            }

            private List<Particle> Particles{ get; set; }

            public int RunChallenge(List<string> input)
            {
                Particles = input.Select((z, index) => {
                    var p = ParseParticle(z);
                    p.Name = index;
                    return p;
                }).ToList();

                var iteractionsSinceLastCollision = 0;

                while(true)
                {
                    iteractionsSinceLastCollision++;

                    Particles.ForEach(z =>
                    {
                        z.VX += z.AX;
                        z.VY += z.AY;
                        z.VZ += z.AZ;
                        z.X += z.VX;
                        z.Y += z.VY;
                        z.Z += z.VZ;
                    });

                    var collisions = Particles.Select(p1 => new {
                        self = p1,
                        collidesWith = Particles.Where(p2 => p1.Name != p2.Name && (p1.X == p2.X && p1.Y == p2.Y && p1.Z == p2.Z))
                    }).Where(c => c.collidesWith.Any()).ToList();

                    if (collisions.Any())
                    {
                        iteractionsSinceLastCollision = 0;

                        collisions.ForEach(z => {
                            z.collidesWith.ToList().ForEach(x => Particles.Remove(x));
                            Particles.Remove(z.self);
                        });
                    }

                    if (iteractionsSinceLastCollision > 10000)
                    {
                        break;
                    }
                }
               
                return Particles.Count();
            }

            private Particle ParseParticle(string input)
            {
                var particle = new Particle();

                var split = Regex.Split(input, ", "); ;

                var position = ParsePart(split, 0);
                particle.X = int.Parse(position[0]);
                particle.Y = int.Parse(position[1]);
                particle.Z = int.Parse(position[2]);

                var velocity = ParsePart(split, 1);
                particle.VX = int.Parse(velocity[0]);
                particle.VY = int.Parse(velocity[1]);
                particle.VZ = int.Parse(velocity[2]);

                var acceleration = ParsePart(split, 2);
                particle.AX = int.Parse(acceleration[0]);
                particle.AY = int.Parse(acceleration[1]);
                particle.AZ = int.Parse(acceleration[2]);

                return particle;
            }

            private string[] ParsePart(String[] part, int partIndex)
            {
                var regex = new Regex("<(.*?)>");
                var v = regex.Match(part[partIndex]).Groups[1].Value.Split(',');
                return v;
            }
        }
    }
}
