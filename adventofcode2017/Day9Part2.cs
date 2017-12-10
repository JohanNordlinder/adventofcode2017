using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Collections.Generic;

namespace adventofcode2017
{
    [TestClass]
    public class Day9Part2
    {
        [TestMethod]
        public void TestRun()
        {
            Assert.AreEqual(0, new Program().RunChallenge("{<>}").TotalGarbage);
            Assert.AreEqual(17, new Program().RunChallenge("{<random characters>}").TotalGarbage);
            Assert.AreEqual(3, new Program().RunChallenge("{<<<<>}").TotalGarbage);
            Assert.AreEqual(2, new Program().RunChallenge("{<{!>}>}").TotalGarbage);
            Assert.AreEqual(0, new Program().RunChallenge("{<!!>}").TotalGarbage);
            Assert.AreEqual(0, new Program().RunChallenge("{<!!!>>}").TotalGarbage);
            Assert.AreEqual(10, new Program().RunChallenge("{<{o\"i!a,<{i<a>}").TotalGarbage);
        }

        [TestMethod]
        public void RealRun()
        {
            var input = System.IO.File.ReadAllLines("Day9Input.txt");
            var result = new Program().RunChallenge(input.First());
            Console.Write("Result: " + result.TotalGarbage);
        }

        private class Result
        {
            public int TotalNumberOfGroups { get; set; }
            public int TotalScore { get; set; }
            public int TotalGarbage { get; set; }
        }

        private class Part
        {
            public int StartIndex { get; set; }
            public int EndIndex { get; set; }
        }

        private class Group : Part
        {
            public Group Parent { get; set; }
            public List<Part> Children { get; set; }
            public int Score { get; set; }
        }

        private class Garbage : Part
        {
            public Group Parent { get; set; }
        }

        private class Program
        {
            public string Input { get; set; }
            public int TotalNumberOfGroups { get; set; } = 1;
            public int TotalScore { get; set; } = 1;
            public int TotalGarbage { get; set; } = 0;

            public Result RunChallenge(string input)
            {
                Input = input;
                var topGroup = new Group { StartIndex = 0, EndIndex = input.Length - 1, Score = 1 };
                topGroup.Children = findChildrenGroups(topGroup);

                return new Result { TotalNumberOfGroups = TotalNumberOfGroups, TotalScore = TotalScore, TotalGarbage = TotalGarbage};
            }

            private List<Part> findChildrenGroups(Group parent)
            {
                var childrenGroups = new List<Part>();

                for (int i = parent.StartIndex + 1; i < Input.Length; i++)
                {
                    if(Input[i] == '{')
                    {
                        var group = new Group { StartIndex = i, Score = parent.Score + 1};
                        group.Children = findChildrenGroups(group);
                        group.EndIndex = group.Children.Any() ? group.Children.Max(z => z.EndIndex) + 1 : group.StartIndex + 1;
                        childrenGroups.Add(group);
                        
                        TotalScore = TotalScore + group.Score;
                        TotalNumberOfGroups++;

                        i = group.EndIndex;
                        continue;
                    }
                    if(Input[i] == '}')
                    {
                        break;
                    }
                    if (Input[i] == '<')
                    {
                        var garbage = new Garbage { StartIndex = i };

                        for(int j = garbage.StartIndex + 1; j < Input.Length; j++)
                        {
                            if(Input[j] == '!')
                            {
                                j++;
                                continue;
                            }
                            if(Input[j] == '>')
                            {
                                garbage.EndIndex = j;
                                break;
                            }
                            TotalGarbage++;
                        }
                        childrenGroups.Add(garbage);
                        i = garbage.EndIndex;
                        continue;
                    }
                }

                return childrenGroups;
            }
        }
    }
}
