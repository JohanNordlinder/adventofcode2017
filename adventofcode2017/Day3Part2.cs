using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Collections.Generic;

namespace adventofcode2017
{
    [TestClass]
    public class Day3Part2
    {
        [TestMethod]
        public void TestRun()
        {
            Assert.AreEqual(25, RunChallenge(24));
        }

        [TestMethod]
        public void RealRun()
        {
            var result = RunChallenge(347991);
            Console.Write("Result: " + result);
        }

        private static decimal RunChallenge(int input)
        {
            var squares = new List<Square>();

            var firstSquare = new Square();
            firstSquare.value = 1;
            firstSquare.x = 0;
            firstSquare.y = 0;
            firstSquare.belongsToCircle = 0;
            firstSquare.pointIndex = 1;
            squares.Add(firstSquare);

            do
            {
                var lastSquare = squares.Last();
                var newSquare = new Square();
                newSquare.pointIndex = squares.Last().pointIndex + 1;
                newSquare.belongsToCircle = IsLastSquareInCircle(lastSquare) ? lastSquare.belongsToCircle + 1 : lastSquare.belongsToCircle;

                var nextPos = findNextPosition(squares);
                newSquare.x = nextPos.x;
                newSquare.y = nextPos.y;

                newSquare.value = findAdjacentSquares(newSquare, squares).Sum(z => z.value);

                squares.Add(newSquare);
            } while (squares.Last().value < input);

            return squares.Last().value;
        }

        class Point
        {
            public int x { get; set; }
            public int y { get; set; }
        }

        class Square
        {
            public int value { get; set; }
            public int x { get; set; }
            public int y { get; set; }
            public int belongsToCircle { get; set; }
            public int pointIndex { get; set; }
        }

        private static bool IsLastSquareInCircle(Square lastSquare)
        {
            var isLastPoint = lastSquare.pointIndex == Math.Pow((1 + lastSquare.belongsToCircle * 2), 2);
            return isLastPoint;
        }

        private static Point findNextPosition(List<Square> squares)
        {
            var lastSquare = squares.Last();

            var isLastSquareInCirle = IsLastSquareInCircle(lastSquare);
            var isAnotherSquarePlacedToTheRightAlready = squares.Any(z => z.y == lastSquare.y && z.x == lastSquare.x + 1);
            var canPlaceToTheRight = isLastSquareInCirle || (!isAnotherSquarePlacedToTheRightAlready && (Math.Abs(lastSquare.x + 1) <= lastSquare.belongsToCircle));
            if (canPlaceToTheRight)
            {
                return new Point
                {
                    x = lastSquare.x + 1,
                    y = lastSquare.y
                };
            }

            var isAnotherSquarePlacedToTheLeftAlready = squares.Any(z => z.y == lastSquare.y && z.x == lastSquare.x - 1);
            var canPlaceToTheLeft = !isAnotherSquarePlacedToTheLeftAlready && (Math.Abs(lastSquare.x - 1) <= lastSquare.belongsToCircle);
            if (canPlaceToTheLeft)
            {
                return new Point
                {
                    x = lastSquare.x - 1,
                    y = lastSquare.y
                };
            }

            var isAnotherSquarePlacedAboveAlready = squares.Any(z => z.y == lastSquare.y + 1 && z.x == lastSquare.x);
            var canPlaceAbove = !isAnotherSquarePlacedAboveAlready && (Math.Abs(lastSquare.y + 1) <= lastSquare.belongsToCircle);
            if (canPlaceAbove)
            {
                return new Point
                {
                    x = lastSquare.x,
                    y = lastSquare.y + 1
                };
            }

            var isAnotherSquarePlacedBelowAlready = squares.Any(z => z.y == lastSquare.y - 1 && z.x == lastSquare.x);
            var canPlaceBelow = !isAnotherSquarePlacedBelowAlready && (Math.Abs(lastSquare.y - 1) <= lastSquare.belongsToCircle);
            if (canPlaceBelow)
            {
                return new Point
                {
                    x = lastSquare.x,
                    y = lastSquare.y - 1
                };
            }

            throw new SystemException("Kunde inte hitta nästa positon");
        }

        private static List<Square> findAdjacentSquares(Square square, List<Square> squares)
        {
            var andra = squares.FindAll(otherSquare =>
                (
                    square.x - 1 == otherSquare.x ||
                    square.x + 1 == otherSquare.x ||
                    square.x == otherSquare.x
                ) &&
                (
                    square.y - 1 == otherSquare.y ||
                    square.y + 1 == otherSquare.y ||
                    square.y == otherSquare.y
                )
            );
            return andra;
        }
    }
}
