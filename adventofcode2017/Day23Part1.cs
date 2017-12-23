using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace adventofcode2017
{
    [TestClass]
    public class Day23Part1
    {
        [TestMethod]
        public void RealRun()
        {
            var input = System.IO.File.ReadAllLines("Day23Input.txt").ToList();
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
                var mulCount = 0L;

                for (long i = 0; (i < input.Count()) && (i >= 0); i++)
                {
                    var instruction = input[(int) i];
                    var move = instruction.Substring(0, 3);

                    if (move == "set")
                    {
                        var target = GetRegister(instruction[4]);
                        var value = GetValue(instruction, 6);

                        target.Value = value;

                    }
                    else if (move == "sub")
                    {
                        var target = GetRegister(instruction[4]);
                        var value = GetValue(instruction, 6);

                        target.Value = target.Value - value;

                    }
                    else if (move == "mul")
                    {
                        var target = GetRegister(instruction[4]);
                        var value = GetValue(instruction, 6);

                        target.Value = target.Value * value;
                        mulCount++;
                    }
                    else if (move == "jnz")
                    {
                        var target = GetRegister(instruction[4]);
                        var valueToCheck = GetValue(instruction, 4, 1);
                        var value = GetValue(instruction, 6);

                        if (valueToCheck != 0L)
                        {
                            i = (i + value - 1);
                        }
                    }
                }

                return mulCount;
            }

            private long GetValue(string instruction, int index, int? maxLength = null)
            {
                var value = maxLength != null ? instruction.Substring(index, (int)maxLength) : instruction.Substring(index);
                return long.TryParse(value, out var n) ? long.Parse(value) : GetRegister(value.First()).Value;
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
