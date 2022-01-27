using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using MartianRobots;

namespace MartianRobotsTests
{
    class RobotsTest
    {
        [TestCase(Command.Right, 1, Orientation.East, TestName = "Test_RotateRightNorthEast")]
        [TestCase(Command.Right, 2, Orientation.South, TestName = "Test_RotateRightNorthSouth")]
        [TestCase(Command.Right, 3, Orientation.West, TestName = "Test_RotateRightNorthWest")]
        [TestCase(Command.Right, 4, Orientation.North, TestName = "Test_RotateRightNorthNorth")]
        [TestCase(Command.Left, 1, Orientation.West, TestName = "Test_RotateLeftNorthWest")]
        [TestCase(Command.Left, 2, Orientation.South, TestName = "Test_RotateLeftNorthSouth")]
        [TestCase(Command.Left, 3, Orientation.East, TestName = "Test_RotateLeftNorthEast")]
        [TestCase(Command.Left, 4, Orientation.North, TestName = "Test_RotateLeftNorthNorth")]
        public void Reorientation(Command command, int timesRotate, Orientation expectedOrientation)
        {
            //arrange
            var grid = new Grid(5, 3);
            var robot = new Robot(0, 0, Orientation.North, grid);

            //act
            for (int i = 0; i < timesRotate; i++)
                robot.ExecuteCommand(command);

            //assert
            Assert.AreEqual(expectedOrientation, robot.Orientation);
            Assert.AreEqual(0, robot.PosX);
            Assert.AreEqual(0, robot.PosY);
        }

        [TestCase(Orientation.North, 1, 2, TestName = "Test_MoveToNorth")]
        [TestCase(Orientation.South, 1, 0, TestName = "Test_MoveToSouth")]
        [TestCase(Orientation.East, 2, 1, TestName = "Test_MoveToEast")]
        [TestCase(Orientation.West, 0, 1, TestName = "Test_MoveToWest")]
        public void DirectionalMovement(Orientation orientation, int x, int y)
        {
            //arrange
            var grid = new Grid(5, 3);
            var robot = new Robot(1, 1, orientation, grid);

            //act
            robot.ExecuteCommand(Command.Forward);

            //assert
            Assert.AreEqual(x, robot.PosX);
            Assert.AreEqual(y, robot.PosY);
        }

        [TestCase(Orientation.North, 3, true, TestName = "Test_Boundaries_NorthLost")]
        [TestCase(Orientation.North, 2, false, TestName = "Test_Boundaries_NorthEdge")]
        [TestCase(Orientation.East, 5, true, TestName = "Test_Boundaries_EastLost")]
        [TestCase(Orientation.East, 4, false, TestName = "Test_Boundaries_EastEdge")]
        [TestCase(Orientation.South, 2, true, TestName = "Test_Boundaries_SouthLost")]
        [TestCase(Orientation.South, 1, false, TestName = "Test_Boundaries_SouthEdge")]
        [TestCase(Orientation.West, 2, true, TestName = "Test_Boundaries_WestLost")]
        [TestCase(Orientation.West, 1, false, TestName = "Test_Boundaries_WestEdge")]
        public void Boundaries(Orientation orientation, int timesForward, bool shouldBeLost)
        {
            //arrange
            var grid = new Grid(5, 3);
            var robot = new Robot(1, 1, orientation, grid);

            //act
            for (int i = 0; i < timesForward; i++)
                robot.ExecuteCommand(Command.Forward);

            //assert
            Assert.AreEqual(shouldBeLost, robot.IsLost);
        }

        [Test]
        public void Test_LastRobotPositionBeforeGetLost()
        {
            //arrange
            var grid = new Grid(5, 3);
            var lostRobot = new Robot(5, 3, Orientation.North, grid);

            //act
            lostRobot.ExecuteCommand(Command.Forward);
            lostRobot.ExecuteCommand(Command.Forward);

            //assert
            Assert.AreEqual(5, lostRobot.PosX);
            Assert.AreEqual(3, lostRobot.PosY);
        }

        [Test]
        public void Test_CheckLastLostRobotScents()
        {
            //arrange
            var grid = new Grid(5, 3);
            var lostRobot = new Robot(5, 3, Orientation.North, grid);
            var savedRobot = new Robot(5, 3, Orientation.North, grid);

            //act
            // this robot should be lost
            lostRobot.ExecuteCommand(Command.Forward);
            // This robot should be able to detect the previous robot scent and stop (avoiding to be lost)
            savedRobot.ExecuteCommand(Command.Forward);

            //assert
            Assert.AreEqual(true, lostRobot.IsLost);
            Assert.AreEqual(false, savedRobot.IsLost);
        }
    }
}
