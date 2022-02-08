using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using MartianRobots;

namespace MartianRobotsTests
{
    class CommandStationTests
    {
        [Test]
        public void GivenRobotListWhenTransmitCommandSequenceThenExpectedPositionAndOrientationIsReturned()
        {
            //arrange
            var robots = new List<Robot> { MockData.GetRobot() };
            var commandStation = new CommandStation(robots);

            //act
            commandStation.ExecuteCommandSequence(0);

            //assert
            var robot = commandStation.robots[0];
            Assert.AreEqual(3, robot.PositionX);
            Assert.AreEqual(3, robot.PositionY);
            Assert.AreEqual(Orientation.North, robot.Orientation);
            Assert.AreEqual(true, robot.IsLost);
        }

        [Test]
        public void GivenRobotListWhenTransmitCommandSequenceThenExpectedOutputIsReturned()
        {
            //arrange
            var robots = new List<Robot> { MockData.GetRobot() };
            var commandStation = new CommandStation(robots);

            //act
            commandStation.ExecuteCommandSequence(0);
            var robotReport = Output.GetRobotOutput(robots);

            //assert
            Assert.AreEqual("3 3 N LOST", string.Join(Environment.NewLine, robotReport));
        }
    }
}
