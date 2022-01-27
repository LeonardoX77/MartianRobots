using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MartianRobots
{
    public struct Coordinate
    {
        public int PositionX { get; set; }
        public int PositionY { get; set; }

        public Coordinate(int x, int y)
        {
            PositionX = x;
            PositionY = y;
        }
    }
}
