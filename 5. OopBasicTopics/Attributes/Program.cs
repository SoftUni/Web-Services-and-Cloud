namespace Attributes
{
    using System;

    public class Program
    {
        static void Main()
        {
            var book = new Book("Pod Igoto", 15, -1d);
            var properties = book.GetType().GetProperties();

            foreach (var property in properties)
            {
                foreach (var attribute in property.GetCustomAttributes(false))
                {
                    if (attribute is MinValueAttribute)
                    {
                        double minValue = ((MinValueAttribute)attribute).MinValue;
                        double actualValue = (double)property.GetValue(book);
                        if (actualValue < minValue)
                        {
                            throw new ArgumentOutOfRangeException(
                                string.Format(
                                    "{0} cannot be nagative",
                                    property.Name));
                        }
                    }
                }
            }
        }
    }
}
