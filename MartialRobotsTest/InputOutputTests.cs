using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.CompareNetObjects;
using MartianRobots;

namespace MartianRobotsTests
{
    [TestFixture]
    class InputOutputTests
    {
        private string input;
        private string output;

        [SetUp]
        public void GlobalSetup()
        {
            input = "5 3\r\n1 1 E\r\nRFRFRFRF\r\n3 2 N\r\nFRRFLLFFRRFLL\r\n0 3 W\r\nLLFFFRFLFL";
            output = "1 1 E\r\n3 3 N LOST\r\n4 2 N";
        }

        [Test]
        public void Test_GetRobotOutput()
        {
            //arrange
            var robots = new List<Robot> { MockData.GetRobot() };

            //act
            var robotReport = Output.GetRobotOutput(robots);

            //assert
            Assert.AreEqual("3 2 N" + Environment.NewLine, robotReport);
        }

        [Test]
        public void Test_GetRobotOutputWhenRobotIsLost()
        {
            //arrange
            var robots = new List<Robot> { MockData.GetRobot() };
            var commandStation = new CommandStation(robots);

            //act
            commandStation.TransmitCommandSequence(0);
            var robotReport = Output.GetRobotOutput(robots);

            //assert
            Assert.AreEqual("3 3 N LOST" + Environment.NewLine, robotReport);
        }

        [Test]
        public void Test_InputGetRobotsExpectRightRobots()
        {
            //arrange
            var grid = new Grid(5, 3);
            input = $"5 3\r\n1 1 E\r\n\r\n3 2 N\r\n";

            var expectedRobots = new List<Robot>() {
                new Robot(1, 1, Orientation.East, grid, new List<Command>()),
                new Robot(3, 2, Orientation.North, grid, new List<Command>()),
            };

            //act
            List<Robot> actualRobots;
            actualRobots = Input.GetRobots(input);

            //assert
            Assert.That(actualRobots, IsDeeplyEqual.To(expectedRobots));
        }

        [Test]
        public void Test_InputGetRobotsCommandSequencesExpectRightCommandSequences()
        {
            //arrange
            var grid = new Grid(5, 3);
            input = $"5 3\r\n1 1 E\r\n{MockData.GetCommandSequenceString()}\r\n3 2 N\r\n{MockData.GetCommandSequenceString()}";

            var expectedRobots = new List<Robot>() {
                new Robot(1, 1, Orientation.East, grid, MockData.GetCommandSequence()),
                new Robot(3, 2, Orientation.North, grid, MockData.GetCommandSequence()),
            };

            //act
            List<Robot> actualRobots;
            actualRobots = Input.GetRobots(input);

            //assert
            Assert.That(
                actualRobots.SelectMany(r => r.Sequences).ToArray(), 
                IsDeeplyEqual.To(expectedRobots.SelectMany(r => r.Sequences).ToArray()));
        }
    }
}
