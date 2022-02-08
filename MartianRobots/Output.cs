using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MartianRobots
{
    public static class Output
    {
        public static string[] GetRobotOutput(List<Robot> robots)
        {
            List<string> robotPositions = new();

            foreach(var robot in robots)
            {
                var line = $"{robot.PositionX} {robot.PositionY} {GetOrientation(robot.Orientation)}";
                if (robot.IsLost)
                    line += " LOST";
                robotPositions.Add(line);
            }

            return robotPositions.ToArray();
        }

        private static char GetOrientation(Orientation orientation)
        {
            char result = ' ';
            switch(orientation)
            {
                case Orientation.North:
                    result = 'N';
                    break;
                case Orientation.South:
                    result = 'S';
                    break;
                case Orientation.East:
                    result = 'E';
                    break;
                case Orientation.West:
                    result = 'W';
                    break;
            }

            return result;
        }
    }
}
