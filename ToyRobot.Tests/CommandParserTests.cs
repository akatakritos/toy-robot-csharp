using System;
using System.Collections.Generic;
using System.Linq;

using NFluent;

using Xunit;

namespace ToyRobot.Tests
{
    public class CommandParserTests
    {
        [Theory]
        [InlineData("PLACE 0,0,NORTH", 0, 0, Direction.North)]
        [InlineData("PLACE 0,1,SOUTH", 0, 1, Direction.South)]
        [InlineData("PLACE 1,0,EAST", 1, 0, Direction.East)]
        [InlineData("PLACE 12,11,WEST", 12, 11, Direction.West)]
        public void PlaceCommand(string input, int expectedX, int expectedY, Direction expectedDirection)
        {
            var cmd = CommandParser.Parse(input);
            Check.That(cmd)
                .IsInstanceOf<PlaceCommand>()
                .And.HasFieldsWithSameValues(new PlaceCommand(expectedX, expectedY, expectedDirection));
        }

        [Fact]
        public void PlaceCommand_BadInput()
        {
            Check.ThatCode(() => CommandParser.Parse("PLACE 0,0,FOO"))
                .Throws<FormatException>();
        }

        [Fact]
        public void MoveCommand()
        {
            var cmd = CommandParser.Parse("MOVE");
            Check.That(cmd).IsInstanceOf<MoveCommand>();
        }

        [Theory]
        [InlineData("LEFT", TurnDirection.Left)]
        [InlineData("RIGHT", TurnDirection.Right)]
        public void TurnCommand(string input, TurnDirection expectedDirection)
        {
            var cmd = CommandParser.Parse(input);
            Check.That(cmd)
                .IsInstanceOf<TurnCommand>()
                .And.HasFieldsWithSameValues(new TurnCommand(expectedDirection));
        }

        [Fact]
        public void ReportCommand()
        {
            var cmd = CommandParser.Parse("REPORT");
            Check.That(cmd)
                .IsInstanceOf<ReportCommand>();
        }
    }
}