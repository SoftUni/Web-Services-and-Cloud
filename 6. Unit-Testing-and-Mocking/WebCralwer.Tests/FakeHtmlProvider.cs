namespace WebCralwer.Tests
{
    using DependencyInversion.WebCrawler;

    public class FakeHtmlProvider : IHtmlProvider
    {
        public string Download(string pageUrl)
        {
            return @" < html >
            < head >
            </ head >
            < body >
                <img src=""nakov.png"" />
                   < label >...</ label >
                   <img alt=""text"" src=""courses/inner/background.jpeg"" >
               </ body >
           </ html > ";
        }
    }
}
