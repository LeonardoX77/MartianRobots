using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MartianRobots
{
    public class Robot
    {
        public int PositionX { get; private set; }
        public int PositionY { get; private set; }
        public Orientation Orientation { get; private set; }
        public bool IsLost { get; private set; }
        public List<Command>? Sequences { get; set; }

        private readonly Grid grid;

        public Robot(int x, int y, Orientation orientation, Grid grid, List<Command>? sequences = null)
        {
            PositionX = x;
            PositionY = y;
            Orientation = orientation;
            this.grid = grid;
            Sequences = sequences;
        }

        public void ExecuteCommand(Command command)
        {
            if (!IsLost)
            {
                switch (command)
                {
                    case Command.Forward:
                        MoveFoward();
                        break;
                    case Command.Right:
                        RotateClockwise();
                        break;
                    case Command.Left:
                        RotateCounterclockwise();
                        break;
                }
            }
        }

        private void MoveFoward()
        {
            var nextPosition = GetNextPosition();

            if (IsOutOfBounds(nextPosition.PositionX, nextPosition.PositionY))
            {
                if (!grid.IsScented(PositionX, PositionY))
                {
                    IsLost = true;
                    grid.MarkPositionAsScented(PositionX, PositionY);
                }
            }
            else
            {
                PositionX = nextPosition.PositionX;
                PositionY = nextPosition.PositionY;
            }
        }

        private void RotateClockwise()
        {
            if (Orientation == Orientation.West)
                Orientation = Orientation.North;
            else
                Orientation++;
        }

        private void RotateCounterclockwise()
        {
            if (Orientation == Orientation.North)
                Orientation = Orientation.West;
            else
                Orientation--;
        }

        private Coordinate GetNextPosition()
        {
            var nextPosition = new Coordinate(PositionX, PositionY);

            if (Orientation == Orientation.North)
                nextPosition.PositionY++;
            else if (Orientation == Orientation.East)
                nextPosition.PositionX++;
            else if (Orientation == Orientation.South)
                nextPosition.PositionY--;
            else if (Orientation == Orientation.West)
                nextPosition.PositionX--;
            
            return nextPosition;
        }

        private bool IsOutOfBounds(int x, int y)
        {
            return x > grid.XBound || x < 0 || y > grid.YBound || y < 0;
        }
    }
}
