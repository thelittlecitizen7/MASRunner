using System;
using System.Threading;

namespace MAS.IO.SystemOutput
{
    class ConsoleOutput : IOutput
    {
        private ConsoleColor ConsoleColor { get; set; }
        public ConsoleOutput(ConsoleColor consoleColor)
        {
            ConsoleColor = consoleColor;
        }
        public void Print(string msg)
        {
            Console.ForegroundColor = ConsoleColor;
            string outputMsg = $"{msg} , thread : {Thread.CurrentThread.ManagedThreadId}";
            Console.WriteLine(outputMsg);
        }
    }
}
