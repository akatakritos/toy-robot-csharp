using System;
using System.Collections.Generic;
using System.Linq;

using NFluent;

using Xunit;

namespace ToyRobot.Tests
{
    public class RobotTests
    {
        [Fact]
        public void HandlesPlaceCommand()
        {
            var subject = new Robot(5, 5);
            subject.ExecuteCommand(new PlaceCommand(1, 2, Direction.North));

            Check.That(subject.IsPlaced).IsTrue();
            Check.That(subject.X).IsEqualTo(1);
            Check.That(subject.Y).IsEqualTo(2);
            Check.That(subject.Facing).IsEqualTo(Direction.North);
        }

        [Fact]
        public void IgnoresInvalidPlaceCommand()
        {
            var subject = new Robot(5, 5);
            subject.ExecuteCommand(new PlaceCommand(10, -1, Direction.East));

            Check.That(subject.IsPlaced).IsFalse();
        }

        [Fact]
        public void IgnoresCommandsUntilPlaced()
        {
            var subject = new Robot(5, 5);

            Check.That(subject.ExecuteCommand(new ReportCommand())).IsFalse();
            Check.That(subject.ExecuteCommand(new MoveCommand())).IsFalse();
            Check.That(subject.ExecuteCommand(new TurnCommand(TurnDirection.Left))).IsFalse();

            Check.That(subject.ExecuteCommand(new PlaceCommand(0, 0, Direction.North))).IsTrue();
        }

        [Fact]
        public void ReportCommand()
        {
            var subject = new Robot(5, 5);
            string report = null;
            subject.OnReport = s => report = s;

            subject.ExecuteCommand(new PlaceCommand(1, 2, Direction.West));
            subject.ExecuteCommand(new ReportCommand());

            Check.That(report).IsEqualTo("1,2,WEST");
        }

        [Theory]
        [InlineData(Direction.North, 2, 3)]
        [InlineData(Direction.East, 3, 2)]
        [InlineData(Direction.South, 2, 1)]
        [InlineData(Direction.West, 1, 2)]
        public void Moves(Direction facing, int expectedX, int expectedY)
        {
            var subject = new Robot(5, 5);
            subject.ExecuteCommand(new PlaceCommand(2, 2, facing));

            subject.ExecuteCommand(new MoveCommand());

            Check.That(subject.X).IsEqualTo(expectedX);
            Check.That(subject.Y).IsEqualTo(expectedY);
        }

        [Theory]
        [InlineData(0, 0, Direction.South)]
        [InlineData(0, 0, Direction.West)]
        [InlineData(4, 4, Direction.North)]
        [InlineData(4, 4, Direction.East)]
        public void MoveDoesNotFall(int x, int y, Direction facing)
        {
            var subject = new Robot(5, 5);
            subject.ExecuteCommand(new PlaceCommand(x, y, facing));

            var moved = subject.ExecuteCommand(new MoveCommand());

            Check.That(moved).IsFalse();
        }

        [Theory]
        [InlineData(Direction.North, TurnDirection.Left, Direction.West)]
        [InlineData(Direction.North, TurnDirection.Right, Direction.East)]
        [InlineData(Direction.East, TurnDirection.Left, Direction.North)]
        [InlineData(Direction.East, TurnDirection.Right, Direction.South)]
        [InlineData(Direction.South, TurnDirection.Left, Direction.East)]
        [InlineData(Direction.South, TurnDirection.Right, Direction.West)]
        [InlineData(Direction.West, TurnDirection.Left, Direction.South)]
        [InlineData(Direction.West, TurnDirection.Right, Direction.North)]
        public void Turns(Direction facing, TurnDirection turn, Direction expected)
        {
            var subject = new Robot(5, 5);
            subject.ExecuteCommand(new PlaceCommand(0, 0, facing));

            subject.ExecuteCommand(new TurnCommand(turn));

            Check.That(subject.Facing).IsEqualTo(expected);
        }
    }
}