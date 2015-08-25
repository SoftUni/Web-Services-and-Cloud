namespace ConsoleLogger
{
    public class ConsoleLoggerMain
    {
        static void Main()
        {
            var logger = new Logger();
            logger.Log("Invalid operation exception");
        }
    }
}
