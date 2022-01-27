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
        public void Test_TransmitCommandSequence()
        {
            //arrange
            var robots = new List<Robot> { MockData.GetRobot() };
            var commandStation = new CommandStation(robots);

            //act
            commandStation.ExecuteCommandSequence(0);

            //assert
            var robot = commandStation.robots[0];
            Assert.AreEqual(3, robot.PosX);
            Assert.AreEqual(3, robot.PosY);
            Assert.AreEqual(Orientation.North, robot.Orientation);
            Assert.AreEqual(true, robot.IsLost);
        }
    }
}
