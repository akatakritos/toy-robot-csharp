using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ToyRobot
{
    public static class CommandParser
    {
        public static Command Parse(string input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            if (input.StartsWith("PLACE ", StringComparison.InvariantCultureIgnoreCase))
            {
                return ParsePlaceCommand(input);
            }

            if (input == "MOVE")
                return new MoveCommand();

            if (input == "LEFT")
                return new TurnCommand(TurnDirection.Left);

            if (input == "RIGHT")
                return new TurnCommand(TurnDirection.Right);

            if (input == "REPORT")
                return new ReportCommand();

            throw new FormatException($"Unrecognized command '{input}'");
        }

        private static readonly Regex PlaceCommandExtractionRegex = new Regex(@"^PLACE (?<x>\d+),(?<y>\d+),(?<direction>NORTH|EAST|WEST|SOUTH)$");

        private static PlaceCommand ParsePlaceCommand(string input)
        {
            var match = PlaceCommandExtractionRegex.Match(input);
            if (!match.Success)
                throw new FormatException($"Badly formatted PlaceCommand: '{input}'");

            var x = int.Parse(match.Groups["x"].Value);
            var y = int.Parse(match.Groups["y"].Value);
            var direction = match.Groups["direction"].Value;

            return new PlaceCommand(x, y, ParseDirection(direction));
        }

        private static Direction ParseDirection(string input)
        {
            switch (input)
            {
                case "NORTH": return Direction.North;
                case "SOUTH": return Direction.South;
                case "EAST": return Direction.East;
                case "WEST": return Direction.West;
                default: throw new FormatException($"Unrecognized direction '{input}'");
            }
        }
    }
}