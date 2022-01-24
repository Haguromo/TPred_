using Contracts;
using HtmlAgilityPack;
using Persistence.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace CodeprojectParser
{
    public class PageParser : PageParserBase
    {
        public PageParser(IArticlePageParser articleParser, string url, string sourceName, IArticleStorager storager) 
            : base(articleParser, url, sourceName, storager)
        {
        }

        private readonly HtmlWeb _web = new HtmlWeb();

        protected override List<string> GetArticlesUrls(string url)
        {
            var html = _web.Load(url);

            var table = html.GetElementbyId("C").Descendants("table").Where(x => x.OuterHtml.Contains("padded article-list")).First();
            var urls = new List<string>();

            foreach (var tr in table.Descendants("tr").Where(x => x.OuterHtml.Contains("valign=\"top\"")))
            {
                var divs = tr.Descendants("td").Where(x => x.OuterHtml.Contains("class=\"title\"")).First();

                var indStart = divs.OuterHtml.IndexOf("href");
                var indEnd = divs.OuterHtml.IndexOf(">", indStart);

                var segment = divs.OuterHtml.Substring(indStart, divs.OuterHtml.Length - indEnd);

                urls.Add(string.Concat("https://www.codeproject.com", segment.Split('\"')[1]));
            }

            return urls;
        }
    }
}

