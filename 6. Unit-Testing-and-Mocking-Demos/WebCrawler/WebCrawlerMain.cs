namespace WebCrawler
{
    using System;

    public class WebCrawlerMain
    {
        static void Main()
        {
            var crawler = new Crawler();
            var urls = crawler.ExtractImageUrls("http://dariknews.bg/");

            int count = 0;
            foreach (var url in urls)
            {
                Console.WriteLine("{0, -3}: {1}", count, url);

                count++;
            }
        }
    }
}
