using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MartianRobots
{
    public static class Input
    {
        public static List<Robot> GetRobots(string input)
        {
            string[] lines = input.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            return GetRobots(lines);
        }

        public static List<Robot> GetRobots(string[] lines)
        {
            var grid = ParseGrid(lines.First());
            List<Robot> robots = new();

            for (int i = 1; i < lines.Length; i++)
            {
                Robot robot;
                if (i % 2 == 0)
                {
                    robot = robots.Last();
                    robot.Sequences = ParseCommandSequence(lines[i]);
                }
                else
                {
                    robot = ParseRobot(lines[i], grid);
                    robots.Add(robot);
                }
            }

            return robots;
        }

        private static Grid ParseGrid(string coords)
        {
            string[] inputs = coords.Split(' ');
            int x = int.Parse(inputs[0]);
            int y = int.Parse(inputs[1]);
            return new Grid(x, y);
        }

        private static Robot ParseRobot(string robotInitPos, Grid grid)
        {
            string[] inputs = robotInitPos.Split(' ');
            var x = int.Parse(inputs[0]);
            var y = int.Parse(inputs[1]);
            var orientation = GetOrientation(inputs[2]);
            return new Robot(x, y, orientation, grid);
        }

        private static List<Command> ParseCommandSequence(string instruction)
        {
            var commandSequence = new List<Command>();

            foreach (var character in instruction)
                commandSequence.Add(GetCommand(character));

            return commandSequence;
        }

        private static Command GetCommand(char command)
        {
            return command switch
            {
                'F' => Command.Forward,
                'R' => Command.Right,
                'L' => Command.Left,
                _ => throw new ArgumentException($"char {command} not valid"),
            };
        }

        private static Orientation GetOrientation(string orientation)
        {
            return orientation switch
            {
                "N" => Orientation.North,
                "E" => Orientation.East,
                "S" => Orientation.South,
                "W" => Orientation.West,
                _ => throw new ArgumentException($"Orientation {orientation} not valid"),
            };
        }
    }
}
