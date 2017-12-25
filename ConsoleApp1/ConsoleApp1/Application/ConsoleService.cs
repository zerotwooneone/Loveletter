using System;

namespace ConsoleApp1.Application
{
    public class ConsoleService : IConsoleService
    {
        public void WriteLine(string text)
        {
            Console.WriteLine(text);
        }

        public string ReadLine()
        {
            return Console.ReadLine();
        }
    }
}