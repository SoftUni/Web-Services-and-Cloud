namespace Interfaces.Formatters
{
    public class JsonFormatter : IFormatter
    {
        public string Format(string name)
        {
            return "{ \"name\" : " + name + " }";
        }
    }
}
