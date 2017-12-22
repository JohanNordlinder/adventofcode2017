using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace adventofcode2017
{
    [TestClass]
    public class Day22Part2
    {
        [TestMethod]
        public void TestRun()
        {
            var input = System.IO.File.ReadAllLines("Day22TestInput.txt").ToList();
            Assert.AreEqual(2511944, new Program().RunChallenge(input));
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
                public State State { get; set; }
                public Coordinate Position { get; set; }
                public Boolean BeganInfected { get; set; }
            }

            public enum State
            {
                CLEAN, WEAKENED, INFECTED, FLAGGED
            }

            public class Coordinate
            {
                public long X { get; set; }
                public long Y { get; set; }
            }

            public Dictionary<string, Node> Nodes { get; set; } = new Dictionary<string, Node>();

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
                        var node = new Node {
                            State = input[i][j] == '#' ? State.INFECTED : State.CLEAN,
                            Position = new Coordinate { Y = offset - i, X = j - offset },
                        };
                        node.BeganInfected = node.State == State.INFECTED;
                        Nodes.Add(node.Position.X.ToString() + "," + node.Position.Y.ToString(), node);
                    }
                }

                var currentPosition = new Coordinate { X = 0, Y = 0 };
                var currentDirection = Direction.UP;

                var infection = 0;

                for (int i = 0; i < 10000000; i++)
                {
                    var node = GetNode(currentPosition);
                    currentDirection = GetNewDirection(node.State, currentDirection);
                    node.State = GetNewState(node.State);
                    if (node.State == State.INFECTED)
                    {
                        infection++;
                    }
                    currentPosition = Move(currentPosition, currentDirection);
                }

                return infection;
            }

            private Direction GetNewDirection(State state, Direction previousDirection)
            {
                switch (state)
                {
                    case State.CLEAN:
                        return TurnLeft(previousDirection);
                    case State.WEAKENED:
                        return previousDirection;
                    case State.INFECTED:
                        return TurnRight(previousDirection);
                    case State.FLAGGED:
                        return ReverceDirection(previousDirection);
                    default:
                        throw new Exception("Invalid state");
                }
            }

            private State GetNewState(State currentState)
            {
                switch (currentState)
                {
                    case State.CLEAN:
                        return State.WEAKENED;
                    case State.WEAKENED:
                        return State.INFECTED;
                    case State.INFECTED:
                        return State.FLAGGED;
                    case State.FLAGGED:
                        return State.CLEAN;
                    default:
                        throw new Exception("Invalid state");
                }
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

            private Direction ReverceDirection(Direction oldDirection)
            {
                switch (oldDirection)
                {
                    case Direction.UP:
                        return Direction.DOWN;
                    case Direction.RIGHT:
                        return Direction.LEFT;
                    case Direction.DOWN:
                        return Direction.UP;
                    case Direction.LEFT:
                        return Direction.RIGHT;
                    default:
                        throw new Exception("Invalid reverse");
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
                var found = Nodes.TryGetValue(cord.X.ToString() + "," + cord.Y.ToString(), out var node);
                if (!found)
                {
                    node = new Node { State = State.CLEAN, BeganInfected = false, Position = new Coordinate { X = cord.X, Y = cord.Y } };
                    Nodes.Add(cord.X.ToString() + "," + cord.Y.ToString(), node);
                }
                return node;
            }
        }
    }
}
