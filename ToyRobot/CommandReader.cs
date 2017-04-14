using System;
using System.Collections.Generic;
using System.Linq;

namespace ToyRobot
{
    /// <summary>
    /// Reads valid commands from a sequence of strings
    /// </summary>
    public static class CommandReader
    {
        /// <summary>
        /// Convert a sequence of string commands into valid <see cref="Command"/> objects
        /// </summary>
        /// <remarks>
        /// Unparseable commands are ignored
        /// </remarks>
        /// <exception cref="ArgumentNullException"></exception>
        /// <param name="source">Input sequence of string commands</param>
        /// <returns></returns>
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

        /// <summary>
        /// Convert a hardcoded sequence of string commands into valid <see cref="Command"/> objects
        /// </summary>
        /// <remarks>
        /// Useful for tests
        /// </remarks>
        /// <param name="commands">hard-coded sequence of commands</param>
        /// <returns></returns>>
        public static IEnumerable<Command> ReadInline(params string[] commands)
        {
            return Read(commands);
        }
    }
}