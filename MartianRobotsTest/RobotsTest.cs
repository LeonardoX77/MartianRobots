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
        [TestCase(Command.Right, 1, Orientation.East, TestName = "GivenRobotPositionNorthWhenMovesToRightx1ThenEastPositionIsExpected")]
        [TestCase(Command.Right, 2, Orientation.South, TestName = "GivenRobotPositionNorthWhenMovesToRightx2ThenSouthPositionIsExpected")]
        [TestCase(Command.Right, 3, Orientation.West, TestName = "GivenRobotPositionNorthWhenMovesToRightx3ThenWestPositionIsExpected")]
        [TestCase(Command.Right, 4, Orientation.North, TestName = "GivenRobotPositionNorthWhenMovesToRightx4ThenNorthPositionIsExpected")]
        [TestCase(Command.Left, 1, Orientation.West, TestName = "GivenRobotPositionNorthWhenMovesToLeftx1ThenWestPositionIsExpected")]
        [TestCase(Command.Left, 2, Orientation.South, TestName = "GivenRobotPositionNorthWhenMovesToLeftx2ThenSouthPositionIsExpected")]
        [TestCase(Command.Left, 3, Orientation.East, TestName = "GivenRobotPositionNorthWhenMovesToLeftx3ThenEastPositionIsExpected")]
        [TestCase(Command.Left, 4, Orientation.North, TestName = "GivenRobotPositionNorthWhenMovesToLeftx4ThenNorthPositionIsExpected")]
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
            Assert.AreEqual(0, robot.PositionX);
            Assert.AreEqual(0, robot.PositionY);
        }

        [TestCase(Orientation.North, 1, 2, TestName = "GivenInitialRobotPositionWhenMovesToNorthThenExpectedPositionIsReturned")]
        [TestCase(Orientation.South, 1, 0, TestName = "GivenInitialRobotPositionWhenMovesToSouthThenExpectedPositionIsReturned")]
        [TestCase(Orientation.East, 2, 1, TestName = "GivenInitialRobotPositionWhenMovesToEastThenExpectedPositionIsReturned")]
        [TestCase(Orientation.West, 0, 1, TestName = "GivenInitialRobotPositionWhenMovesToWestThenExpectedPositionIsReturned")]
        public void DirectionalMovement(Orientation orientation, int x, int y)
        {
            //arrange
            var grid = new Grid(5, 3);
            var robot = new Robot(1, 1, orientation, grid);

            //act
            robot.ExecuteCommand(Command.Forward);

            //assert
            Assert.AreEqual(x, robot.PositionX);
            Assert.AreEqual(y, robot.PositionY);
        }

        [TestCase(Orientation.North, 3, true, TestName = "GivenGridBoundariesWhenRobotOrientationPointingToNorthOutsideEdgeThenIsLostIsReturned")]
        [TestCase(Orientation.North, 2, false, TestName = "GivenGridBoundariesWhenRobotOrientationPointingToNorthNextToEdgeThenNotLostIsReturned")]
        [TestCase(Orientation.East, 5, true, TestName = "GivenGridBoundariesWhenRobotOrientationPointingToEastOutsideEdgeThenIsLostIsReturned")]
        [TestCase(Orientation.East, 4, false, TestName = "GivenGridBoundariesWhenRobotOrientationPointingToEastNextToEdgeThenNotLostIsReturned")]
        [TestCase(Orientation.South, 2, true, TestName = "GivenGridBoundariesWhenRobotOrientationPointingToSouthOutsideEdgeThenIsLostIsReturned")]
        [TestCase(Orientation.South, 1, false, TestName = "GivenGridBoundariesWhenRobotOrientationPointingToSouthNextToEdgeThenNotLostIsReturned")]
        [TestCase(Orientation.West, 2, true, TestName = "GivenGridBoundariesWhenRobotOrientationPointingToWestOutsideEdgeThenIsLostIsReturned")]
        [TestCase(Orientation.West, 1, false, TestName = "GivenGridBoundariesWhenRobotOrientationPointingToWestNextToEdgeThenNotLostIsReturned")]
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
        public void GivenInitialRobotPositionWhenMovesOutsideGridEdgeThenLastRobotPositionBeforeGetLostIsReturned()
        {
            //arrange
            var grid = new Grid(5, 3);
            var lostRobot = new Robot(5, 3, Orientation.North, grid);

            //act
            lostRobot.ExecuteCommand(Command.Forward);
            lostRobot.ExecuteCommand(Command.Forward);

            //assert
            Assert.AreEqual(5, lostRobot.PositionX);
            Assert.AreEqual(3, lostRobot.PositionY);
        }

        [Test]
        public void Given2RobotsWhenBothAreInTheSameGridEdgeThenSecondRobotIsNotLostCausedByFirstRobotScent()
        {
            //arrange
            var grid = new Grid(5, 3);
            var lostRobot = new Robot(5, 3, Orientation.North, grid);
            var savedRobot = new Robot(5, 3, Orientation.North, grid);

            //act
            // robot should be lost
            lostRobot.ExecuteCommand(Command.Forward);
            // robot should be able to detect the previous robot scent and stop (avoiding to be lost)
            savedRobot.ExecuteCommand(Command.Forward);

            //assert
            Assert.AreEqual(true, lostRobot.IsLost);
            Assert.AreEqual(false, savedRobot.IsLost);
        }
    }
}
