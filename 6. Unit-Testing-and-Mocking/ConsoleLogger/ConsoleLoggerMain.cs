namespace ConsoleLogger
{
    public class ConsoleLoggerMain
    {
        static void Main()
        {
            var logger = new ConsolePrinter();
            logger.Print("Invalid operation exception");
        }
    }
}
