namespace DependencyInversion.WebCrawler
{
    using System.Collections.Generic;
    using System.Text.RegularExpressions;

    public class Crawler
    {
        private const string ImageRegexPattern = "<img.*?src=\"(.*?)\".*?>";

        private Regex regex;

        private Regex Regex
        {
            get
            {
                if (this.regex == null)
                {
                    this.regex = new Regex(ImageRegexPattern);
                }

                return this.regex;
            }
        }

        public Crawler(IHtmlProvider htmlProvider)
        {
            this.HtmlProvider = htmlProvider;
        }

        public IHtmlProvider HtmlProvider { get; set; }

        public IEnumerable<string> ExtractImageUrls(string pageUrl)
        {
            var html = this.HtmlProvider.Download(pageUrl);

            return this.ParseImages(html);
        }

        private IEnumerable<string> ParseImages(string html)
        {
            MatchCollection matches = this.Regex.Matches(html);

            var imageUrls = new List<string>(matches.Count);
            foreach (Match match in matches)
            {
                imageUrls.Add(match.Groups[1].Value);
            }

            return imageUrls;
        }
    }
}
