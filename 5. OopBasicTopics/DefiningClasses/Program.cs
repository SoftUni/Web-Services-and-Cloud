namespace DefiningClasses
{
    using System;

    public class Program
    {
        static void Main()
        {
            var book = new Book("Pod Igoto", 15, 22d);
            var bookTwo = new Book("A song of ice and fire", 10);
            var bookThree = new Book("Muh song");

            Console.WriteLine(book.TotalPrice);
            book.PrintContent();

            try
            {
                var bookFour = new Book("The Alchemist", 0, -5);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
