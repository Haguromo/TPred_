using Contracts;
using HtmlAgilityPack;
using Parsers.Core.Exceptions;
using Parsers.HtmlProviding;
using Persistence.DataModel;
using System.Linq;
using System.Threading.Tasks;

namespace StackOverflowParser
{
    public sealed class ArticlePageParser : IArticlePageParser
	{
        private readonly IHtmlProvider _provider;

		public ArticlePageParser(IHtmlProvider provider)
		{
            _provider = provider;
		}

		public async Task<Article> GetArticleAsync(string url)
		{
            HtmlDocument html;
            try
            {
                html = await _provider.ProvideAsync(url);
            }
            catch (FailedToLoadHtmlException e)
            {
                return new Article();
            }

            var title = "";
			title = html.GetElementbyId("question-header")
				.Descendants("a")
				.Where(x => x.GetAttributeValue("class", "")
				.Contains("question-hyperlink"))
				.Last()
				.InnerText;

			string text = "";

			var topics = html.GetElementbyId("mainbar")
			  .Descendants("div")
			  .Where(x => x.GetAttributeValue("class", "")
			  .Contains("post-text"));

			foreach (var topic in topics)
				text += topic.InnerText;

			return new Article(title, text, url, "");
		}
	}
}
