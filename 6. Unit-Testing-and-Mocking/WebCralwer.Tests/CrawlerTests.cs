using System;
namespace WebCralwer.Tests
{
    using System.Linq;
    using DependencyInversion.WebCrawler;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class CrawlerTests
    {
        [TestMethod]
        public void ExtractImageUrls_Should_Return_Collection_Of_Image_Src_Content()
        {
            // Arrange
            var htmlProvider = new FakeHtmlProvider();
            var crawler = new Crawler(htmlProvider);

            var expectedImageUrls = new[]
            {
                "nakov.png",
                "courses/inner/background.jpeg"
            };

            // Act
            var imageUrls = crawler.ExtractImageUrls(string.Empty)
                .ToList();

            
            // Assert
            CollectionAssert.AreEqual(expectedImageUrls, imageUrls);
        }
    }
}
