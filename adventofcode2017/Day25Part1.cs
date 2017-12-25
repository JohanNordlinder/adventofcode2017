using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace adventofcode2017
{
    [TestClass]
    public class Day25Part1
    {
        [TestMethod]
        public void TestRun()
        {
            var states = new Dictionary<char, State>();

            states.Add('A', new State
            {
                Name = 'A',
                RunIfCurrentValueIsX = 0,
                SetValueIfX = 1,
                MoveOffsetIfX = 1,
                NextStateIfX = 'B',
                SetValueIfY = 0,
                MoveOffsetIfY = -1,
                NextStateIfY = 'B',
            });

            states.Add('B', new State
            {
                Name = 'B',
                RunIfCurrentValueIsX = 0,
                SetValueIfX = 1,
                MoveOffsetIfX = -1,
                NextStateIfX = 'A',
                SetValueIfY = 1,
                MoveOffsetIfY = 1,
                NextStateIfY = 'A',
            });

            Assert.AreEqual(3, new Program().RunChallenge(states, 6));
        }

        [TestMethod]
        public void RealRun()
        {
            var states = new Dictionary<char, State>();

            states.Add('A', new State
            {
                Name = 'A',
                RunIfCurrentValueIsX = 0,
                SetValueIfX = 1,
                MoveOffsetIfX = 1,
                NextStateIfX = 'B',
                SetValueIfY = 0,
                MoveOffsetIfY = 1,
                NextStateIfY = 'F',
            });

            states.Add('B', new State
            {
                Name = 'B',
                RunIfCurrentValueIsX = 0,
                SetValueIfX = 0,
                MoveOffsetIfX = -1,
                NextStateIfX = 'B',
                SetValueIfY = 1,
                MoveOffsetIfY = -1,
                NextStateIfY = 'C',
            });

            states.Add('C', new State
            {
                Name = 'C',
                RunIfCurrentValueIsX = 0,
                SetValueIfX = 1,
                MoveOffsetIfX = -1,
                NextStateIfX = 'D',
                SetValueIfY = 0,
                MoveOffsetIfY = 1,
                NextStateIfY = 'C',
            });

            states.Add('D', new State
            {
                Name = 'D',
                RunIfCurrentValueIsX = 0,
                SetValueIfX = 1,
                MoveOffsetIfX = -1,
                NextStateIfX = 'E',
                SetValueIfY = 1,
                MoveOffsetIfY = 1,
                NextStateIfY = 'A',
            });

            states.Add('E', new State
            {
                Name = 'E',
                RunIfCurrentValueIsX = 0,
                SetValueIfX = 1,
                MoveOffsetIfX = -1,
                NextStateIfX = 'F',
                SetValueIfY = 0,
                MoveOffsetIfY = -1,
                NextStateIfY = 'D',
            });

            states.Add('F', new State
            {
                Name = 'F',
                RunIfCurrentValueIsX = 0,
                SetValueIfX = 1,
                MoveOffsetIfX = 1,
                NextStateIfX = 'A',
                SetValueIfY = 0,
                MoveOffsetIfY = -1,
                NextStateIfY = 'E',
            });

            Console.WriteLine("Result: " + new Program().RunChallenge(states, 12425180));
        }

        public class State
        {
            public char Name { get; set; }
            public int RunIfCurrentValueIsX { get; set; }
            public int MoveOffsetIfX { get; set; }
            public int MoveOffsetIfY { get; set; }
            public int SetValueIfX { get; set; }
            public int SetValueIfY { get; set; }
            public char NextStateIfX { get; set; }
            public char NextStateIfY { get; set; }
        }

        public class Program
        {
            public Dictionary<long, bool> Values { get; set; } = new Dictionary<long, bool>();

            public long RunChallenge(Dictionary<char, State> states, int steps)
            {
                var currentPosition = 0;
                var currentState = states['A'];

                for (int i = 0; i < steps; i++)
                {
                    var currentValue = GetValue(currentPosition);
                    if (currentValue == currentState.RunIfCurrentValueIsX)
                    {
                        SetValue(currentPosition, currentState.SetValueIfX);
                        currentPosition += currentState.MoveOffsetIfX;
                        currentState = states[currentState.NextStateIfX];
                    } else
                    {
                        SetValue(currentPosition, currentState.SetValueIfY);
                        currentPosition += currentState.MoveOffsetIfY;
                        currentState = states[currentState.NextStateIfY];
                    }
                }

                return Values.Count(z => z.Value);
            }

            private void SetValue(long index, int value)
            {
                if (Values.TryGetValue(index, out var temp))
                {
                    Values.Remove(index);
                }

                Values.Add(index, value == 1 ? true : false);
            }

            private int GetValue(long index)
            {
                var found = Values.TryGetValue(index, out var value);
                if (!found)
                {
                    value = false;
                    Values.Add(index, value);
                }
                return value ? 1 : 0;
            }
        }
    }
}
