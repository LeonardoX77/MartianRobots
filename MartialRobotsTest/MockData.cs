using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MartianRobots;

namespace MartianRobotsTests
{
    public class MockData
    {
        
        public static string GetCommandSequenceString()
        {
            return "FRRFLLFFRRFLL";
        }
        public static List<Command> GetCommandSequence()
        {
            return new List<Command>
            {
                //FRRFLLFFRRFLL
                Command.Forward,
                Command.Right,
                Command.Right,
                Command.Forward,
                Command.Left,
                Command.Left,
                Command.Forward,
                Command.Forward,
                Command.Right,
                Command.Right,
                Command.Forward,
                Command.Left,
                Command.Left,
            };
        }

        public static Robot GetRobot()
        {
            Robot robot = new Robot(3, 2, Orientation.North, GetGrid());
            robot.Sequences = new List<Command>(GetCommandSequence());
            return robot;
        }

        public static Grid GetGrid()
        {
            return new Grid(5, 3);
        }
    }
}
