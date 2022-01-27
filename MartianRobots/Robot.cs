﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MartianRobots
{
    public class Robot
    {
        public int PosX { get; private set; }
        public int PosY { get; private set; }
        public Orientation Orientation { get; private set; }
        public bool IsLost { get; private set; }
        public List<Command>? Sequences { get; set; }

        private Grid grid;

        public Robot(int x, int y, Orientation orientation, Grid grid, List<Command>? sequences = null)
        {
            PosX = x;
            PosY = y;
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
                    default:
                        throw new ArgumentException($"Command {command} not valid");
                }
            }
        }

        private void MoveFoward()
        {
            var nextPosition = GetNextPosition();

            if (IsOutOfBounds(nextPosition.PositionX, nextPosition.PositionY))
            {
                if (!grid.IsScented(PosX, PosY))
                {
                    IsLost = true;
                    grid.MarkPositionAsScented(PosX, PosY);
                }
            }
            else
            {
                PosX = nextPosition.PositionX;
                PosY = nextPosition.PositionY;
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
            var nextPosition = new Coordinate(PosX, PosY);

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
