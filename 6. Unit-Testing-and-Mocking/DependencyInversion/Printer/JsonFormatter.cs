namespace DependencyInversion.Printer
{
    public class JsonFormatter : IFormatter
    {
        public string Format(string message)
        {
            return "{ message: " + message + " }";
        }
    }
}
