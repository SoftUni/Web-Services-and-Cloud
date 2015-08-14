namespace Attributes
{
    using System;

    public class MinValueAttribute : Attribute
    {
        public MinValueAttribute(double minValue)
        {
            this.MinValue = minValue;
        }

        public double MinValue { get; private set; }
    }
}
