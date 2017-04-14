using System;
using System.Collections.Generic;
using System.Linq;

namespace ToyRobot
{
    public static class CommandReader
    {
        public static IEnumerable<Command> Read(IEnumerable<string> source)
        {
            if (source == null)
                throw new ArgumentNullException();

            return ReadImpl();

            IEnumerable<Command> ReadImpl()
            {
                foreach (var s in source)
                {
                    Command result = null;
                    try
                    {
                        result = CommandParser.Parse(s);
                    }
                    catch (FormatException)
                    {
                        continue;
                    }

                    yield return result;
                }
            }
        }

        public static IEnumerable<Command> ReadInline(params string[] commands)
        {
            return Read(commands);
        }
    }
}