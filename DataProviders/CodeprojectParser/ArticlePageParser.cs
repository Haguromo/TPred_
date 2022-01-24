using Contracts;
using Parsers.HtmlProviding;
using System.Linq;
using System.Threading.Tasks;
using Persistence.DataModel;
using Parsers.Core.Exceptions;
using HtmlAgilityPack;

namespace CodeprojectParser
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
            string text = "";
            
            var title = html.GetElementbyId("AT").Descendants("div")
                .Where(x => x.OuterHtml.Contains("class=\"header")).First()
                .Descendants("div")
                .Where(x => x.OuterHtml.Contains("class=\"title")).First()
                .Descendants("h1").First().InnerText;

            var topics = html.GetElementbyId("contentdiv").Descendants("p");

            foreach (var topic in topics)
                text += topic.InnerText;

            return new Article(title, text, url, "");
        }
    }
}
