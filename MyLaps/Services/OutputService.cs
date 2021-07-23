using MyLaps.Interfaces;
using System;

namespace MyLaps.Services
{
    public class OutputService : IOutputService
    {
        public void WriteLine(string line)
        {
            Console.WriteLine(line);
        }
    }
}
