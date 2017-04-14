using System;
using System.Collections.Generic;
using System.Linq;

using NFluent;

using Xunit;

namespace ToyRobot.Tests
{
    public class CommandReaderTests
    {
        [Fact]
        public void IgnoresUnparseableCommands()
        {
            var commands = new string[]
            {
                "REPORT",
                "LEFT",
                "FFSJKF",
                "PLACE 0,0,FOO",
                "RIGHT",
            };

            var results = CommandReader.Read(commands).ToList();
            Check.That(results).HasSize(3);
        }
    }
}