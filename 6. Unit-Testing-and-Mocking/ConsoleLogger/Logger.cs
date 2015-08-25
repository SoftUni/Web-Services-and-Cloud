namespace ConsoleLogger
{
    using System;

    public class Logger
    {
        public void Log(string message)
        {
            Console.WriteLine("{0}: {1}",
                DateTime.Now, message);
        }
    }
}
