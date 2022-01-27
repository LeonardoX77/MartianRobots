using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MartianRobots
{
    public static class Output
    {
        public static string GetRobotOutput(List<Robot> robots)
        {
            var sb = new StringBuilder();

            foreach(var robot in robots)
            {
                var line = $"{robot.PosX} {robot.PosY} {GetOrientation(robot.Orientation)}";
                if (robot.IsLost)
                    line += " LOST";
                sb.AppendLine(line);
            }

            return sb.ToString();
        }

        private static char GetOrientation(Orientation orientation)
        {
            switch(orientation)
            {
                case Orientation.North:
                    return 'N';
                case Orientation.South:
                    return 'S';
                case Orientation.East:
                    return 'E';
                case Orientation.West:
                    return 'W';
                default:
                    throw new ArgumentException($"orientation {orientation} not valid");
            }
        }
    }
}
