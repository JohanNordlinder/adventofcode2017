using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace adventofcode2017
{
    [TestClass]
    public class Day18Part2
    {
        [TestMethod]
        public void TestRun()
        {
            var input = System.IO.File.ReadAllLines("Day18TestInput2.txt").ToList();
            var programs = BootstrapPrograms(input);
            while (programs.Any(z => z.Run())) {}
            Assert.AreEqual(3, programs.First(z => z.Name == "Program 1").TotalSent);
        }

        [TestMethod]
        public void RealRun()
        {
            var input = System.IO.File.ReadAllLines("Day18Input.txt").ToList();

            var programs = BootstrapPrograms(input);
            while (programs.Any(z => z.Run())) {
                //System.Diagnostics.Trace.WriteLine(program0.Name + " " + program0.CurrentInstuction);
            }

            Console.WriteLine("Result: " + programs.First(z => z.Name == "Program 1").TotalSent);
        }

        public class Register
        {
            public long Value { get; set; }
            public char Name { get; set; }
        }

        public List<Program> BootstrapPrograms(List<string> input)
        {
            var program0 = new Program { Input = input, Name = "Program 0" };
            var program1 = new Program { Input = input, Name = "Program 1" };
            program0.SendQueue = program1.RecivedQueue;
            program1.SendQueue = program0.RecivedQueue;
            program0.GetRegister('p').Value = 0;
            program1.GetRegister('p').Value = 1;
            return new List<Program>() { program0, program1 };
        }

        public class Program
        {

            public String Name { get; set; }

            public List<Register> Registers { get; set; } = new List<Register>();

            public Queue<long> RecivedQueue { get; set; } = new Queue<long>();

            public Queue<long> SendQueue { get; set; }

            public long TotalSent { get; set; } = 0;

            public int CurrentInstuction { get; set; } = 0;

            public List<string> Input { get; set; }

            public Boolean Run()
            {
                if (CurrentInstuction < 0 || CurrentInstuction >= Input.Count())
                    return false;

                var currentInstruction = Input[CurrentInstuction];
                var move = currentInstruction.Substring(0, 3);
                var target = GetRegister(currentInstruction[4]);
                var value = -1L;

                if (currentInstruction.Length > 5) { 
                    value = GetValue(currentInstruction, 6);
                }

                if (move == "snd")
                {
                    var valueToSend = GetValue(currentInstruction, 4);
                    SendQueue.Enqueue(valueToSend);
                    TotalSent++;
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

                    if (RecivedQueue.Any())
                    {
                        target.Value = RecivedQueue.Dequeue();
                    } else
                    {
                        return false;
                    }
                }
                else if (move == "jgz")
                {
                    var valueToCheck = GetValue(currentInstruction, 4, 1);
                    if (valueToCheck > 0)
                    {
                        CurrentInstuction = (int) (CurrentInstuction + value - 1);
                    }
                }

                CurrentInstuction++;
                return true;
            }

            private long GetValue(string instruction, int index, int? maxLength = null)
            {
                var value = maxLength != null ? instruction.Substring(index, (int) maxLength) : instruction.Substring(index);
                return long.TryParse(value, out var n) ? long.Parse(value) : GetRegister(value.First()).Value;
            }

            public Register GetRegister(char name)
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
