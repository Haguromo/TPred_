using Contracts;
using HtmlAgilityPack;
using Persistence.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace StackOverflowParser
{
	public sealed class PageParser : PageParserBase
	{
        public PageParser(IArticlePageParser articleParser, string url, string sourceName, IArticleStorager storager)
            : base(articleParser, url, sourceName, storager)
        {
        }

        private readonly HtmlWeb _web = new HtmlWeb();

        protected override List<string> GetArticlesUrls(string url)
		{
			var html = _web.Load(url);

			var shortUrls = html.GetElementbyId("questions")
				.Descendants("a")
				.Where(x => x.GetAttributeValue("class", "")
				.Contains("question-hyperlink"))
				.Select(node => node.GetAttributeValue("href", "")).ToList();

			var urls = new List<string>();

			foreach (var urlToAdd in shortUrls)
			{
				urls.Add(string.Concat("https://stackoverflow.com", urlToAdd));
			}

            return urls;
		}
	}
}
