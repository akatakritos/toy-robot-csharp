using System;
using System.Collections.Generic;
using System.Linq;

namespace ToyRobot
{
    public class Robot
    {
        private readonly int _boardWidth;
        private readonly int _boardHeight;
        public int X { get; private set; } = -1;
        public int Y { get; private set; } = -1;
        public Direction Facing { get; private set; }
        public Action<string> OnReport { get; set; }
        public bool IsPlaced { get; private set; }

        public Action<string> Log { get; set; }

        public Robot(int boardWidth, int boardHeight)
        {
            _boardWidth = boardWidth;
            _boardHeight = boardHeight;
        }

        public bool ExecuteCommand(Command cmd)
        {
            switch (cmd)
            {
                case PlaceCommand placeCommand:
                    return HandlePlaceCommand(placeCommand);
                case ReportCommand _:
                    return Report();
                case MoveCommand _:
                    return Move();
                case TurnCommand turnCommand:
                    return Turn(turnCommand.TurnDirection);
            }

            return false;
        }

        private bool Turn(TurnDirection turnDirection)
        {
            EmitLog($"Processing Turn '{turnDirection}'");

            if (!IsPlaced)
                return NotPlaced();

            var directionInt = (int)Facing;

            if (turnDirection == TurnDirection.Left)
                directionInt--;
            else if (turnDirection == TurnDirection.Right)
                directionInt++;
            else
                throw new InvalidOperationException($"Unexpcted TurnDirection");

            if (directionInt < 0)
                directionInt = 4 + directionInt;

            Facing = (Direction)(directionInt % 4);

            EmitLog($"Now facing {Facing}");
            return true;
        }

        private bool Move()
        {
            EmitLog($"Processing move command");

            if (!IsPlaced)
                return NotPlaced();

            var delta = GetDelta(Facing);

            if (!IsOnBoard(X + delta.dx, Y + delta.dy))
            {
                EmitLog("Destination off board. Command ignored.");
                return false;
            }

            X = X + delta.dx;
            Y = Y + delta.dy;

            EmitLog($"Moved to ({X},{Y})");
            return true;
        }

        private (int dx, int dy) GetDelta(Direction direction)
        {
            switch (direction)
            {
                case Direction.East: return (1, 0);
                case Direction.South: return (0, -1);
                case Direction.West: return (-1, 0);
                case Direction.North: return (0, 1);
                default: throw new ArgumentOutOfRangeException(nameof(direction));
            }
        }

        private bool Report()
        {
            EmitLog("Processing report command");
            if (!IsPlaced)
                return false;

            var status = $"{X},{Y},{FacingAsString}";

            OnReport?.Invoke(status);
            return true;
        }

        private string FacingAsString => Facing.ToString().ToUpper();

        private bool HandlePlaceCommand(PlaceCommand cmd)
        {
            EmitLog($"Processing place command ({cmd.X},{cmd.Y}) facing {cmd.Direction}");
            if (!IsOnBoard(cmd.X, cmd.Y))
                return false;

            X = cmd.X;
            Y = cmd.Y;
            Facing = cmd.Direction;
            IsPlaced = true;

            EmitLog($"Now placed at ({X},{Y}) facing {Facing}");
            return true;
        }

        private bool IsOnBoard(int x, int y)
        {
            return x >= 0 && x < _boardWidth && y >= 0 && y < _boardHeight;
        }

        private bool NotPlaced()
        {
            EmitLog("Not yet placed. Command ignored.");
            return false;
        }

        private void EmitLog(string s)
        {
            Log?.Invoke(s);
        }
    }
}