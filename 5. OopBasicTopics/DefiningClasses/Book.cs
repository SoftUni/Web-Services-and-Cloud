namespace DefiningClasses
{
    using System;

    public class Book
    {
        public const double DefaultValueAddedTax = 1.2d;

        private string title;
        private double price;

        public Book(string title, int copies, double price)
        {
            this.Title = title;
            this.Copies = copies;
            this.Price = price;
        }

        public Book(string title, int copies)
            : this(title, copies, 10)
        {
        }

        public Book(string title)
            : this(title, 0)
        {
        }

        public double TotalPrice
        {
            get
            {
                return this.price * DefaultValueAddedTax;
            }
        }
        
        public double Price
        {
            get { return this.price; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(
                        "Price cannot be negative");
                }

                this.price = value;
            }
        }

        public int Copies { get; set; } 
        
        public string Title
        {
            get { return this.title; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentException("Book title cannot be null");
                }

                this.title = value;
            }
        }

        public void PrintContent()
        {
            Console.WriteLine("Guncho...");
        }
    }
}
