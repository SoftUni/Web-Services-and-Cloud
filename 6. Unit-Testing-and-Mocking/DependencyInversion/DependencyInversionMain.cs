namespace DependencyInversion
{
    using System;
    using System.Collections.Generic;
    using DirectoryTraversal;
    using Ninject;
    using Printer;
    using RpgGame;
    using WebCrawler;

    public class DependencyInversionMain
    {
        private static void Main()
        {
            var kernel = new StandardKernel();
            Bindings.RegisterBindings(kernel);
            
            // Console printer
            RunConsolePrinter(kernel);

            // Rpg game
            RunRpgGame(kernel);

            // Crawler
            RunCrawler(kernel);

            // Directory traversal
            RunDirectoryTraverser(kernel);
        }

        private static void RunConsolePrinter(StandardKernel kernel)
        {
            var formatter = kernel.Get<IFormatter>();
            var printer = new ConsolePrinter(formatter);
            printer.Print("Hello world");
        }

        private static void RunDirectoryTraverser(StandardKernel kernel)
        {
            var directoryProvider = kernel.Get<IDirectoryProvider>();
            var traverser = new DirectoryTraverser(
                @"C:\SVN\web-services-and-cloud\August-2015",
                directoryProvider);

            var children = traverser.GetChildDirectories();
            foreach (var child in children)
            {
                Console.WriteLine(child);
            }

            Console.WriteLine(traverser.CurrentDirectory);
        }

        private static void RunCrawler(StandardKernel kernel)
        {
            var htmlProvider = kernel.Get<IHtmlProvider>();
            var crawler = new Crawler(htmlProvider);
            var urls = crawler.ExtractImageUrls("http://dariknews.bg/");

            int count = 0;
            foreach (var url in urls)
            {
                Console.WriteLine("{0, -3}: {1}", count, url);
                count++;
            }
        }

        private static void RunRpgGame(StandardKernel kernel)
        {
            var randomNumberProvider = kernel.Get<IRandomNumberProvider>();
            var possibleItemDrops = new List<Item>
            {
                new Item("Axe", 25, 5),
                new Item("Shield", 5, 50),
                new Item("Resilience Potion", 0, 30)
            };

            var character = new Character(
                possibleItemDrops,
                randomNumberProvider);

            var droppedItem = character.DropItem();
            Console.WriteLine(droppedItem);
        }
    }
}
