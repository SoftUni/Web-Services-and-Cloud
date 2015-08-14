namespace DefiningClasses
{
    public class Rectangle
    {
        public Rectangle(double width, double height)
        {
            this.Width = width;
            this.Height = height;
        }

        public double Width { get; set; }

        public double Height { get; set; }

        public double Area
        {
            get
            {
                return this.Width * this.Height;
            }
        }
    }
}
