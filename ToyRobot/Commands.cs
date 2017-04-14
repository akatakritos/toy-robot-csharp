using System;
using System.Collections.Generic;
using System.Linq;

namespace ToyRobot
{
    /// <summary>
    /// Represents a command the Robot should follow
    /// </summary>
    public class Command
    {
    }

    /// <summary>
    /// Instructs the robot to move one space in its currently facing direction
    /// </summary>
    public class MoveCommand : Command
    {
        /// <summary>
        /// Gets a singleton instance of the <see cref="MoveCommand"/>.
        /// </summary>
        public static MoveCommand Instance { get; } = new MoveCommand();
    }

    /// <summary>
    /// Instructs the robot to report its current location and bearing
    /// </summary>
    public class ReportCommand : Command
    {
        /// <summary>
        /// Gets a singleton instance of the <see cref="ReportCommand"/>.
        /// </summary>
        public static ReportCommand Instance { get; } = new ReportCommand();
    }

    /// <summary>
    /// Instructs the robot to turn 90 degress in the specified direction
    /// </summary>
    public class TurnCommand : Command
    {
        /// <summary>
        /// Gets the direction the robot should turn
        /// </summary>
        public TurnDirection TurnDirection { get; }

        /// <summary>
        /// Creates a new instance of the <see cref="TurnCommand"/> class
        /// </summary>
        /// <param name="turnDirection">The direction the robot is instructed to turn</param>
        public TurnCommand(TurnDirection turnDirection)
        {
            TurnDirection = turnDirection;
        }

        /// <summary>
        /// Gets a singleton instance of a <see cref="TurnCommand"/> to turn Left
        /// </summary>
        public static TurnCommand LeftInstance { get; } = new TurnCommand(TurnDirection.Left);

        /// <summary>
        /// Gets a singleton instance of a <see cref="TurnCommand"/> to turn Right
        /// </summary>
        public static TurnCommand RightInstance { get; } = new TurnCommand(TurnDirection.Right);
    }

    /// <summary>
    /// Instructs the robot to place itself at a specified location and bearing
    /// </summary>
    public class PlaceCommand : Command
    {
        /// <summary>
        /// Get X location the robot should place itself
        /// </summary>
        public int X { get; }

        /// <summary>
        /// Gets the Y location in which the robot should place itself
        /// </summary>
        public int Y { get; }

        /// <summary>
        /// Gets the direction in which the robot should face
        /// </summary>
        public Direction Direction { get; }

        /// <summary>
        /// Constructs a new instance of the PlaceCommand
        /// </summary>
        /// <param name="x">The x location</param>
        /// <param name="y">The y location</param>
        /// <param name="direction">The direction to face</param>
        public PlaceCommand(int x, int y, Direction direction)
        {
            X = x;
            Y = y;
            Direction = direction;
        }
    }

    /// <summary>
    /// Enumerates possible directions in which a robot can turn
    /// </summary>
    public enum TurnDirection
    {
        Left = 0,
        Right = 1
    }

    /// <summary>
    /// Enumerates possible directions a robot might face
    /// </summary>
    public enum Direction
    {
        North = 0,
        East = 1,
        South = 2,
        West = 3
    }
}