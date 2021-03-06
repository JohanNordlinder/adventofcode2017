﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace adventofcode2017
{
    [TestClass]
    public class Day19Part2
    {
        [TestMethod]
        public void TestRun()
        {
            var input = System.IO.File.ReadAllLines("Day19TestInput.txt").ToList();
            Assert.AreEqual(38, new Program().RunChallenge(input));
        }

        [TestMethod]
        public void RealRun()
        {
            var input = System.IO.File.ReadAllLines("Day19Input.txt").ToList();
            Console.WriteLine("Result: " + new Program().RunChallenge(input));
        }

        public class Register
        {
            public long value { get; set; }
            public char name { get; set; }
        }

        public class Program
        {

            enum Direction
            {
                DOWN, UP, LEFT, RIGHT
            }

            class Coordinate
            {
                public int x { get; set; }
                public int y { get; set; }
            }

            private List<string> Input { get; set; }

            public int RunChallenge(List<string> input)
            {
                Input = input;

                var currentCoordinate = new Coordinate { x = input[0].IndexOf('|'), y = 0 };
                var direction = Direction.DOWN;
                var steps = 0;

                while (true)
                {
                    steps++;
                    var nextCoordinate = GetNextCoordinate(currentCoordinate, direction);
                    var nextPathChar = GetPathCharAtCoordinate(nextCoordinate);

                    if(nextPathChar == '|')
                    {
                        currentCoordinate = nextCoordinate;
                    }
                    else if (nextPathChar == '-')
                    {
                        currentCoordinate = nextCoordinate;
                    }
                    else if (nextPathChar == '+')
                    {
                        if(direction == Direction.UP || direction == Direction.DOWN) {
                            var canMoveRight = GetPathCharAtCoordinate(GetNextCoordinate(nextCoordinate, Direction.RIGHT)) != ' ';
                            if(canMoveRight)
                            {
                                currentCoordinate = GetNextCoordinate(nextCoordinate, Direction.RIGHT);
                                direction = Direction.RIGHT;
                            }
                            else
                            {
                                currentCoordinate = GetNextCoordinate(nextCoordinate, Direction.LEFT);
                                direction = Direction.LEFT;
                            }
                            steps++;
                        } else
                        {
                            var canMoveUp = GetPathCharAtCoordinate(GetNextCoordinate(nextCoordinate, Direction.UP)) != ' ';
                            if (canMoveUp)
                            {
                                currentCoordinate = GetNextCoordinate(nextCoordinate, Direction.UP);
                                direction = Direction.UP;
                            }
                            else
                            {
                                currentCoordinate = GetNextCoordinate(nextCoordinate, Direction.DOWN);
                                direction = Direction.DOWN;
                            }
                            steps++;
                        }
                    }
                    else if (nextPathChar == ' ')
                    {
                        break;
                    }
                    else {
                        currentCoordinate = nextCoordinate;
                    }

                    System.Diagnostics.Trace.WriteLine("Current cord x=" + currentCoordinate.x + " y=" + currentCoordinate.y);
                }

                return steps;
            }

            private char GetPathCharAtCoordinate(Coordinate cord)
            {
                return Input[cord.y][cord.x];
            }

            private Coordinate GetNextCoordinate(Coordinate last, Direction direction)
            {
                switch(direction)
                {
                    case Direction.UP:
                        return new Coordinate { x = last.x, y = last.y - 1 };
                    case Direction.DOWN:
                        return new Coordinate { x = last.x, y = last.y + 1 };
                    case Direction.LEFT:
                        return new Coordinate { x = last.x - 1, y = last.y };
                    case Direction.RIGHT:
                        return new Coordinate { x = last.x + 1, y = last.y };
                    default:
                        throw new Exception("Invalid direction");
                }
            }
        }
    }
}
