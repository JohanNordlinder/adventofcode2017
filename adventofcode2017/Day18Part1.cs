using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace adventofcode2017
{
    [TestClass]
    public class Day18Part1
    {
        [TestMethod]
        public void TestRun()
        {
            var input = System.IO.File.ReadAllLines("Day18TestInput.txt").ToList();
            Assert.AreEqual(4, new Program().RunChallenge(input));
        }

        [TestMethod]
        public void RealRun()
        {
            var input = System.IO.File.ReadAllLines("Day18Input.txt").ToList();
            Console.WriteLine("Result: " + new Program().RunChallenge(input));
        }

        public class Register
        {
            public long Value { get; set; }
            public char Name { get; set; }
        }

        public class Program
        {

            public List<Register> Registers { get; set; } = new List<Register>();

            public long RunChallenge(List<string> input)
            {
                var lastSound = -1L;

                for(int i = 0; i < input.Count(); i++) {
                    var instruction = input[i];
                    var move = instruction.Substring(0, 3);
                    var target = GetRegister(instruction[4]);
                    var value = -1L;
                    if (instruction.Length > 5) { 
                        value = long.TryParse(instruction.Substring(6), out var n) ? long.Parse(instruction.Substring(6)) : GetRegister(instruction.Substring(6).First()).Value;
                    }

                    if (move == "snd")
                    {
                        lastSound = GetRegister(instruction.Substring(4).First()).Value;
                    } else if(move == "set")
                    {
                        target.Value = value;

                    }
                    else if (move == "add")
                    {
                        target.Value = target.Value + value;

                    }
                    else if (move == "mul")
                    {
                        target.Value = target.Value * value;

                    }
                    else if (move == "mod")
                    {
                        target.Value = target.Value % value;

                    }
                    else if (move == "rcv")
                    {
                        if (target.Value != 0)
                        {
                            break;
                        }
                    }
                    else if (move == "jgz")
                    {
                        if (target.Value > 0)
                        {
                            i = (int) (i + value - 1);
                        }
                    }
                }

                return lastSound;
            }

            private Register GetRegister(char name)
            {
                var register = Registers.FirstOrDefault(z => z.Name == name);

                if (register == null)
                {
                    register = new Register { Name = name, Value = 0 };
                    Registers.Add(register);
                }
                return register;
            }
        }
    }
}
