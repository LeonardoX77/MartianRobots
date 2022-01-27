using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MartianRobots
{
    public class Grid
    {
        public int XBound { get; }
        public int YBound { get;  }
        private List<Coordinate> scentedCoords  = new List<Coordinate>();

        public Grid(int x, int y)
        {
            XBound = x;
            YBound = y;
        }

        public void MarkPositionAsScented(int x, int y)
        {
            scentedCoords.Add(new Coordinate(x, y));
        }

        public bool IsScented(int x, int y)
        {
            return scentedCoords.Contains(new Coordinate(x, y));
        }
    }
}
