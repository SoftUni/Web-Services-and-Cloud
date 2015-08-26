namespace DependencyInversion.WebCrawler
{
    public interface IHtmlProvider
    {
        string Download(string pageUrl);
    }
}
