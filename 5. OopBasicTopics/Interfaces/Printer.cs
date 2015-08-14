namespace Interfaces
{
    using System;

    public class Printer
    {
        public void Print(string name, IFormatter formatter)
        {
            Console.WriteLine(formatter.Format(name));
        }
    }
}
