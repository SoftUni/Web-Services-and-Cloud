namespace DependencyInversion.Printer
{
    using System;

    public class ConsolePrinter
    {
        public ConsolePrinter(IFormatter formatter)
        {
            this.Formatter = formatter;
        }

        public IFormatter Formatter { get; set; }

        public void Print(string message)
        {
            var formatted = this.Formatter.Format(message);
            Console.WriteLine(formatted);
        }
    }
}
