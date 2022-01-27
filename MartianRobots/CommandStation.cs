using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MartianRobots
{
    public class CommandStation
    {
        public List<Robot> robots;

        public CommandStation(List<Robot> robots)
        {
            this.robots = robots;
        }

        public void ExecuteCommandSequence(int robotIndex)
        {
            Robot robot = robots[robotIndex];
            if (robot.Sequences != null)
            {
                foreach (var command in robot.Sequences)
                    robot.ExecuteCommand(command);
            }
        }
    }
}
