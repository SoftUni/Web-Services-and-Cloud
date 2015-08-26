namespace DependencyInversion.Printer
{
    public class UrlFormatter : IFormatter
    {
        public string Format(string message)
        {
            return "?message=" + message;
        }
    }
}
