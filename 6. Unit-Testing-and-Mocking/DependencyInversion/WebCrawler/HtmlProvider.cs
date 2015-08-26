namespace DependencyInversion.WebCrawler
{
    using System.Net;

    public class HtmlProvider : IHtmlProvider
    {
        public string Download(string pageUrl)
        {
            var client = new WebClient();

            return client.DownloadString(pageUrl);
        }
    }
}
