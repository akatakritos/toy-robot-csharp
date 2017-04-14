using System;
using System.Collections.Generic;
using System.Linq;

namespace ToyRobot
{
    public class Command
    {
    }

    public class MoveCommand : Command
    {
    }

    public class ReportCommand : Command
    {
    }

    public class TurnCommand : Command
    {
        public TurnDirection TurnDirection { get; }

        public TurnCommand(TurnDirection turnDirection)
        {
            TurnDirection = turnDirection;
        }
    }

    public enum TurnDirection
    {
        Left = 0,
        Right = 1
    }

    public enum Direction
    {
        North = 0,
        East = 1,
        South = 2,
        West = 3
    }

    public class PlaceCommand : Command
    {
        public int X { get; }
        public int Y { get; }
        public Direction Direction { get; }

        public PlaceCommand(int x, int y, Direction direction)
        {
            X = x;
            Y = y;
            Direction = direction;
        }
    }
}