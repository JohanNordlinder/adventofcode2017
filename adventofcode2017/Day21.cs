using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace adventofcode2017
{
    [TestClass]
    public class Day21
    {
        [TestMethod]
        public void TestRun()
        {
            var input = System.IO.File.ReadAllLines("Day21TestInput.txt").ToList();
            Assert.AreEqual(12, new Program().RunChallenge(input, 2));
        }

        [TestMethod]
        public void RealRun()
        {
            var input = System.IO.File.ReadAllLines("Day21Input.txt").ToList();
            Console.WriteLine("Result Part 1: " + new Program().RunChallenge(input, 5));
            Console.WriteLine("Result Part 2: " + new Program().RunChallenge(input, 18));

        }

        public class Program
        {

            class Image
            {
                public int Size { get; set; }
                public List<string> Rows { get; set; }
            }

            class Rule
            {
                public string[] InputPattern { get; set; }
                public string[] OutputPattern { get; set; }
            }

            public int RunChallenge(List<string> rules, int iterations)
            {
                var parsedRules = rules.Select(z => {
                    var parsed = Regex.Split(z, " => ");
                    return new Rule
                    {
                        InputPattern = parsed[0].Split('/'),
                        OutputPattern = parsed[1].Split('/')
                    };
                });

                var rawImage = System.IO.File.ReadAllLines("Day21Start.txt").ToList();
                var image = new Image { Size = rawImage.Count(), Rows = rawImage};

                for (int i = 0; i < iterations; i++)
                {
                    if (image.Size % 2 == 0)
                    {
                        var transformed = SplitAndTransform(2, image, parsedRules);
                        image = JoinImages(transformed, image.Size / 2, image.Size + image.Rows.Count() / 2);
                    } else if (image.Size % 3 == 0)
                    {
                        var transformed = SplitAndTransform(3, image, parsedRules);
                        image = JoinImages(transformed, image.Size / 3, image.Size + image.Rows.Count() / 3);
                    }
                    else {
                        throw new Exception("Cannot divide");
                    }
                }

                var result = image.Rows.Sum(z => z.Replace(".", String.Empty).Length);
                return result;
            }

            private Image JoinImages(List<Image> squares, int partSize, int newSize)
            {
                if (squares.Count() == 1)
                {
                    return squares.First();
                }

                var batchSize = squares.Count() / partSize;
                var newImage = new Image { Size = newSize, Rows = new List<string>()};

                while (squares.Any())
                {
                    var squaresForThisRow = squares.Take(batchSize).ToList();
                    squaresForThisRow.ForEach(z => squares.Remove(z));

                    for (int thisRowSquare = 0; thisRowSquare < squaresForThisRow.First().Rows.Count; thisRowSquare++)
                    {
                        newImage.Rows.Add(String.Join("", squaresForThisRow.Select(z => z.Rows[thisRowSquare])));
                    }
                }
          
                return newImage;
            }

            private List<Image> SplitAndTransform(int splitSize, Image image, IEnumerable<Rule> rules)
            {
                var subImages = new List<Image>();

                for (int row = 0; row < image.Size / splitSize; row++)
                {
                    for (int column = 0; column < image.Size / splitSize; column++)
                    {
                        var subImage = new Image { Size = splitSize + 1, Rows = image.Rows.Select(z => z.Substring(column * splitSize, splitSize)).Skip(row * splitSize).Take(splitSize).ToList() };
                        var matchingRule = rules.Select((r, index) => new { rule = r, index = index }).First(r => RuleMatch(r.rule, subImage, r.index));
                        subImage.Rows = matchingRule.rule.OutputPattern.ToList();
                        subImages.Add(subImage);
                    }
                }

                return subImages;
            }

            private bool RuleMatch(Rule rule, Image image, int index)
            {
                var normal = rule.InputPattern;
                var degre90 = rule.InputPattern.Select((z, i) => String.Join(String.Empty, rule.InputPattern.Select(x => x[i]).Reverse()));
                var degre180 = rule.InputPattern.Reverse().Select(z => z.Reverse());
                var degre270 = rule.InputPattern.Select((z, i) => String.Join(String.Empty, rule.InputPattern.Select(x => x[i]))).Reverse();
                var flip1 = rule.InputPattern.Select(z => z.Reverse());
                var flip2 = rule.InputPattern.Reverse();
                var flip3 = rule.InputPattern.Reverse().Select(z => z.Reverse());
                var degre90Flip1 = degre90.Reverse();

                var rotations = new [] { normal, degre90, degre270, degre270};
                var flips = new [] { flip1, flip2, flip3 };

                var match = RuleMatch(normal, image.Rows) ||
                            RuleMatch(degre90.ToArray(), image.Rows) ||
                            RuleMatch(degre270.ToArray(), image.Rows) ||
                            RuleMatch(degre180.ToArray(), image.Rows) ||
                            RuleMatch(flip1.ToArray(), image.Rows) ||
                            RuleMatch(flip2.ToArray(), image.Rows) ||
                            RuleMatch(flip3.ToArray(), image.Rows) ||
                            RuleMatch(flip3.ToArray(), image.Rows) ||
                            RuleMatch(degre90Flip1.ToArray(), image.Rows);

                if (match) { 
                    System.Diagnostics.Trace.WriteLine("Rulematch: " + index);
                }

                return match;
            }

            private bool RuleMatch(string[] rule, List<string> image)
            {
                for (int i = 0; i < image.Count; i++)
                {
                    if (image[i] != rule[i])
                    {
                        return false;
                    }
                }

                return true;
            }

        }
    }

    static class StringExtensions
    {
        public static string Reverse(this string input)
        {
            return new string(input.ToCharArray().Reverse().ToArray());
        }
    }
}
