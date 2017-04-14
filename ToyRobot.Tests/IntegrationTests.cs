using System;
using System.Collections.Generic;
using System.Linq;

using NFluent;

using Xunit;

namespace ToyRobot.Tests
{
    public class IntegrationTests
    {
        [Fact]
        public void Test4()
        {
            var subject = new Robot(5, 5);
            string report = null;
            subject.OnReport = s => report = s;

            var commands = CommandReader.ReadInline(
                "PLACE 1,2,EAST",
                "MOVE",
                "MOVE",
                "LEFT",
                "MOVE",
                "REPORT"
            );

            foreach (var command in commands)
                subject.ExecuteCommand(command);

            Check.That(report).IsEqualTo("3,3,NORTH");
        }
    }
}