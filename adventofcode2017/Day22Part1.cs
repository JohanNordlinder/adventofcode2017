using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace adventofcode2017
{
    [TestClass]
    public class Day22Part1
    {
        [TestMethod]
        public void TestRun()
        {
            var input = System.IO.File.ReadAllLines("Day22TestInput.txt").ToList();
            Assert.AreEqual(5587, new Program().RunChallenge(input));
        }

        [TestMethod]
        public void RealRun()
        {
            var input = System.IO.File.ReadAllLines("Day22Input.txt").ToList();
            Console.WriteLine("Result: " + new Program().RunChallenge(input));
        }

        public class Program
        {
            public class Node
            {
                public bool Infected { get; set; }
                public Coordinate Position { get; set; }
                public Boolean BeganInfected { get; set; }
            }

            public class Coordinate
            {
                public int X { get; set; }
                public int Y { get; set; }
            }

            public List<Node> Nodes { get; set; } = new List<Node>();

            enum Direction
            {
                DOWN, UP, LEFT, RIGHT
            }

            public int RunChallenge(List<string> input)
            {
                var offset = ((input.Count() - 1) / 2);
                for(int i = 0; i < input.Count(); i++)
                {
                    for (int j = 0; j < input.Count(); j++)
                    {
                        Nodes.Add(new Node {
                            Infected = input[i][j] == '#',
                            Position = new Coordinate { Y = offset - i, X = j - offset },
                            BeganInfected = input[i][j] == '#'
                        });
                    }
                }

                var currentPosition = new Coordinate { X = 0, Y = 0 };
                var currentDirection = Direction.UP;

                var infection = 0;

                for (int i = 0; i < 10000; i++)
                {
                    var node = GetNode(currentPosition);
                    currentDirection = node.Infected ? TurnRight(currentDirection) : TurnLeft(currentDirection);
                    node.Infected = !node.Infected;
                    if (node.Infected)
                    {
                        infection++;
                    }
                    currentPosition = Move(currentPosition, currentDirection);
                }

                return infection;
            }

            private Coordinate Move(Coordinate lastPosition, Direction direction)
            {
                switch (direction)
                {
                    case Direction.UP:
                        return new Coordinate { X = lastPosition.X, Y = lastPosition.Y + 1 };
                    case Direction.RIGHT:
                        return new Coordinate { X = lastPosition.X + 1, Y = lastPosition.Y };
                    case Direction.DOWN:
                        return new Coordinate { X = lastPosition.X, Y = lastPosition.Y - 1 };
                    case Direction.LEFT:
                        return new Coordinate { X = lastPosition.X - 1, Y = lastPosition.Y};
                    default:
                        throw new Exception("Invalid Move");
                }
            }

            private Direction TurnRight(Direction oldDirection)
            {
                switch(oldDirection)
                {
                    case Direction.UP:
                        return Direction.RIGHT;
                    case Direction.RIGHT:
                        return Direction.DOWN;
                    case Direction.DOWN:
                        return Direction.LEFT;
                    case Direction.LEFT:
                        return Direction.UP;
                    default:
                        throw new Exception("Invalid turn");
                }
            }

            private Direction TurnLeft(Direction oldDirection)
            {
                switch (oldDirection)
                {
                    case Direction.UP:
                        return Direction.LEFT;
                    case Direction.RIGHT:
                        return Direction.UP;
                    case Direction.DOWN:
                        return Direction.RIGHT;
                    case Direction.LEFT:
                        return Direction.DOWN;
                    default:
                        throw new Exception("Invalid turn");
                }
            }

            private Node GetNode(Coordinate cord)
            {
                var node = Nodes.FirstOrDefault(z => z.Position.X == cord.X && z.Position.Y == cord.Y);
                if (node == null)
                {
                    node = new Node { Infected = false, BeganInfected = false, Position = new Coordinate { X = cord.X, Y = cord.Y } };
                    Nodes.Add(node);
                }
                return node;
            }
        }
    }
}
