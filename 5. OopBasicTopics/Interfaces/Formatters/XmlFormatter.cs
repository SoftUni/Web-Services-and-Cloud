namespace Interfaces.Formatters
{
    public class XmlFormatter : IFormatter
    {
        public string Format(string name)
        {
            return "<name>" + name + "</name>";
        }
    }
}
