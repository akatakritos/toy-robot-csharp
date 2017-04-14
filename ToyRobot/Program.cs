using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace ToyRobot
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                var name = Process.GetCurrentProcess().ProcessName;
                Console.WriteLine($"Usage: {name} file");
                return;
            }

            var filename = args[0];
            var bot = new Robot(5, 5);
            bot.OnReport = Report;
            bot.Log = Log;

            foreach (var cmd in CommandReader.Read(File.ReadLines(filename)))
            {
                bot.ExecuteCommand(cmd);
            }
        }

        static void Report(string s)
        {
            var current = Console.ForegroundColor;

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(s);
            Console.ForegroundColor = current;
        }

        static void Log(string s)
        {
            var current = Console.ForegroundColor;

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine(s);
            Console.ForegroundColor = current;
        }
    }
}