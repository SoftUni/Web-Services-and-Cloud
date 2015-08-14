namespace Interfaces
{
    using Formatters;

    class Program
    {
        static void Main()
        {
            var printer = new Printer();
            IFormatter formatter = new XmlFormatter();
            //formatter = new JsonFormatter();

            printer.Print("gosho", formatter);
        }
    }
}
