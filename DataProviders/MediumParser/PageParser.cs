using Contracts;
using HtmlAgilityPack;
using Persistence.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace MediumParser
{
    public class PageParser : PageParserBase
    {
        public PageParser(IArticlePageParser articleParser, string url, string sourceName, IArticleStorager storager) :
            base(articleParser, url, sourceName, storager)
        {
        }

        private readonly HtmlWeb _web = new HtmlWeb();

        protected override List<string> GetArticlesUrls(string url)
        {

            var html = _web.Load(url);

            var shortUrls = html.GetElementbyId("root")
                .Descendants("h3")
                .Where(node => node.FirstChild.GetAttributeValue("href", "")
                .Contains("/@matthewbiggins") == false)
                .Select(x => x.FirstChild.GetAttributeValue("href", "")).ToList();

            var urls = new List<string>();

            foreach (var urlToAdd in shortUrls)
                urls.Add(urlToAdd);

            return urls;
        }
    }
}
