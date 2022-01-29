using MartianRobots;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MartianRobotsTests
{
    [TestFixture]
    class InputOutputTests
    {
        [SetUp]
        public void GlobalSetup()
        {
        }

        [Test]
        public void Test_GetRobotOutput()
        {
            //arrange
            var input = $"5 3\r\n1 1 N\r\n\r\n3 2 E\r\n\r\n2 2 S\r\n\r\n3 3 W\r\n";

            //act
            List<Robot> robots = Input.GetRobots(input);
            var robotReport = Output.GetRobotOutput(robots);

            //assert
            Assert.AreEqual("1 1 N\r\n3 2 E\r\n2 2 S\r\n3 3 W\r\n", robotReport);
        }

        [Test]
        public void Test_GetRobotOutputWhenRobotIsLost()
        {
            //arrange
            var robots = new List<Robot> { MockData.GetRobot() };
            var commandStation = new CommandStation(robots);

            //act
            commandStation.ExecuteCommandSequence(0);
            var robotReport = Output.GetRobotOutput(robots);

            //assert
            Assert.AreEqual("3 3 N LOST" + Environment.NewLine, robotReport);
        }

        [Test]
        public void Test_InputGetRobotsExpectRightRobots()
        {
            //arrange
            var grid = new Grid(5, 3);
            var input = $"5 3\r\n1 1 N\r\n\r\n3 2 E\r\n\r\n2 2 S\r\n\r\n3 3 W\r\n";

            var expectedRobots = new List<Robot>() {
                new Robot(1, 1, Orientation.North, grid, new List<Command>()),
                new Robot(3, 2, Orientation.East, grid, new List<Command>()),
                new Robot(2, 2, Orientation.South, grid, new List<Command>()),
                new Robot(3, 3, Orientation.West, grid, new List<Command>()),
            };

            //act
            List<Robot> actualRobots = Input.GetRobots(input);

            //assert
            for (int i = 0; i < expectedRobots.Count; i++)
            {
                var expectedRobot = expectedRobots[i];
                var actualRobot = actualRobots[i];
                AssertEx.PropertyValuesAreEquals(actualRobot, expectedRobot);
            }
        }

        [Test]
        public void Test_InputGetRobotsWithInvalidOrientationRaiseError()
        {
            //arrange
            var grid = new Grid(5, 3);
            var input = $"5 3\r\n1 1 X\r\n";

            //act

            //assert
            var exception = Assert.Throws<ArgumentException>(() => Input.GetRobots(input));
            Assert.AreEqual("Orientation X not valid", exception?.Message);
        }

        [Test]
        public void Test_InputGetRobotsCommandSequencesExpectRightCommandSequences()
        {
            //arrange
            var grid = new Grid(5, 3);
            var input = $"5 3\r\n1 1 E\r\n{MockData.GetCommandSequenceString()}\r\n3 2 N\r\n{MockData.GetCommandSequenceString()}";

            var expectedRobots = new List<Robot>() {
                new Robot(1, 1, Orientation.East, grid, MockData.GetCommandSequence()),
                new Robot(3, 2, Orientation.North, grid, MockData.GetCommandSequence()),
            };

            //act
            List<Robot> actualRobots = Input.GetRobots(input);

            //assert
            Command[] actualSequences = actualRobots.SelectMany(r => r.Sequences).ToArray();
            Command[] expectedSequences = expectedRobots.SelectMany(r => r.Sequences).ToArray();
            for (int i = 0; i < expectedSequences.Length; i++)
            {
                var expectedSequence = expectedSequences[i];
                var actualSequence = actualSequences[i];

                Assert.AreEqual(actualSequence, expectedSequence);
            }
        }

        [Test]
        public void Test_InputGetRobotsWithInvalidCommandSequenceRaiseError()
        {
            //arrange
            var invalidCommandSequence = "ABCDE";
            var input = $"5 3\r\n1 1 E\r\n{invalidCommandSequence}";

            //act

            //assert
            var exception = Assert.Throws<ArgumentException>(() => Input.GetRobots(input));
            Assert.AreEqual("char A not valid", exception?.Message);
        }
    }
}
