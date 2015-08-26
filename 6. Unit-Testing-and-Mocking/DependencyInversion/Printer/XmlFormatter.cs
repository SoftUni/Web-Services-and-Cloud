namespace DependencyInversion.Printer
{
    public class XmlFormatter : IFormatter
    {
        public string Format(string message)
        {
            return "<message>" + message + "</message";
        }
    }
}
