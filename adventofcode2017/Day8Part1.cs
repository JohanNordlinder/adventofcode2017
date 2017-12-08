using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Collections.Generic;

namespace adventofcode2017
{
    [TestClass]
    public class Day8Part1
    {
        [TestMethod]
        public void TestRun()
        {
            var input = System.IO.File.ReadAllLines("Day8TestInput.txt");
            var result = new Program().RunChallenge(input);
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void RealRun()
        {
            var input = System.IO.File.ReadAllLines("Day8Input.txt");
            var result = new Program().RunChallenge(input);
            Console.Write("Result: " + result);
        }

        private class Program
        {
            private enum CompareOperation
            {
                GreaterThan, GreaterOrEqualThan, LessThan, LessOrEqualThan, Equal, NotEqual
            }

            private enum ModifyOperation
            {
                Increase, Decrease,
            }

            private List<Register> Registers { get; set; } = new List<Register>();
        
            private class Register
            {
                public string name { get; set; }
                public int value { get; set; }
            }

            public int RunChallenge(string[] input)
            {
                var instructions = input.Select(z => z.Split(' ')).Select(z => new
                {
                    target = z.First(),
                    operation = z.ElementAt(1),
                    value = int.Parse(z.ElementAt(2)),
                    compareTarget = z.ElementAt(4),
                    compareOperation = parseOperation(z.ElementAt(5)),
                    compareValue = int.Parse(z.Last())
                }).ToList();

                instructions.ForEach(z => {
                    if (EvaluateCompareExpression(GetRegister(z.compareTarget), z.compareValue, z.compareOperation))
                    {
                        var target = GetRegister(z.target);
                        EvaluateModifyExpression(target, z.value, ParseModifyOperation(z.operation));
                    }
                });

                return Registers.Max(z => z.value);
            }

            private void EvaluateModifyExpression(Register target, int modifyValue, ModifyOperation operation)
            {
                switch (operation)
                {
                    case ModifyOperation.Increase:
                        target.value = target.value + modifyValue;
                        break;
                    case ModifyOperation.Decrease:
                        target.value = target.value - modifyValue;
                        break;
                    default:
                        throw new Exception("Unknown Operation");
                }
            }

            private bool EvaluateCompareExpression(Register compareTarget, int compareValue, CompareOperation operation)
            {
                switch (operation)
                {
                    case CompareOperation.Equal:
                        return compareTarget.value == compareValue;
                    case CompareOperation.NotEqual:
                        return compareTarget.value != compareValue;
                    case CompareOperation.GreaterThan:
                        return compareTarget.value > compareValue;
                    case CompareOperation.GreaterOrEqualThan:
                        return compareTarget.value >= compareValue;
                    case CompareOperation.LessThan:
                        return compareTarget.value < compareValue;
                    case CompareOperation.LessOrEqualThan:
                        return compareTarget.value <= compareValue;
                    default:
                        throw new Exception("Unknown Operation");
                }
            }

            private Register GetRegister(string registerName)
            {
                var register = Registers.FirstOrDefault(z => z.name == registerName);
                if (register == null)
                {
                    var newRegister = new Register { name = registerName, value = 0 };
                    Registers.Add(newRegister);
                    return newRegister;
                }
                else
                {
                    return register;
                }
            }

            private ModifyOperation ParseModifyOperation(string modifyOperation)
            {
                switch (modifyOperation)
                {
                    case "inc":
                        return ModifyOperation.Increase;
                    case "dec":
                        return ModifyOperation.Decrease;
                    default:
                        throw new Exception("Unknown Operation");
                }
            }

            private CompareOperation parseOperation(string operation)
            {
                switch (operation)
                {
                    case ">":
                        return CompareOperation.GreaterThan;
                    case ">=":
                        return CompareOperation.GreaterOrEqualThan;
                    case "<":
                        return CompareOperation.LessThan;
                    case "<=":
                        return CompareOperation.LessOrEqualThan;
                    case "!=":
                        return CompareOperation.NotEqual;
                    case "==":
                        return CompareOperation.Equal;
                    default:
                        throw new Exception("Unknown Operation");
                }
            }

        }
    }
}
