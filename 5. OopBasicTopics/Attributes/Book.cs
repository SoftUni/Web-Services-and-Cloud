namespace Attributes
{
    public class Book
    {
        public Book(string title, int copies, double price)
        {
            this.Title = title;
            this.Copies = copies;
            this.Price = price;
        }

        public string Title { get; set; }

        public int Copies { get; set; }

        [MinValue(0d)]
        public double Price { get; set; }
    }
}
