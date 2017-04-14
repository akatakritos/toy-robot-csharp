using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ToyRobot
{
    /// <summary>
    /// Command Parser turns string commands into strongly typed objects we can work
    /// with at higher levels.
    /// </summary>
    public static class CommandParser
    {
        /// <summary>
        /// Parse a command string into a strongly typed <see cref="Command"/> object.
        /// </summary>
        /// <exception cref="FormatException">The string is not recognized as a command</exception>
        /// <param name="input">The command string</param>
        /// <returns></returns>
        public static Command Parse(string input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            if (input.StartsWith("PLACE ", StringComparison.InvariantCultureIgnoreCase))
                return ParsePlaceCommand(input);

            if (input == "MOVE")
                return MoveCommand.Instance;

            if (input == "LEFT")
                return TurnCommand.LeftInstance;

            if (input == "RIGHT")
                return TurnCommand.RightInstance;

            if (input == "REPORT")
                return ReportCommand.Instance;

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