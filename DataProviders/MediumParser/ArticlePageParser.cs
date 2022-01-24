using Contracts;
using HtmlAgilityPack;
using Parsers.Core.Exceptions;
using Parsers.HtmlProviding;
using Persistence.DataModel;
using System;
using System.Threading.Tasks;

namespace MediumParser
{
    public class ArticlePageParser : IArticlePageParser
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

            try
            {
                var title = html.DocumentNode.
                    SelectSingleNode("//*[@class = 'section-inner sectionLayout--insetColumn']").
                    SelectSingleNode("//*[@class = 'graf graf--h3 graf--leading graf--title']").
                    InnerText;

                var text = html.DocumentNode.
                    SelectSingleNode("//*[@class = 'section-inner sectionLayout--insetColumn']").InnerText;

                return new Article(title, text, url, "");
            }
            catch(NullReferenceException)
            {
                return null;
            }

        }
    }
}
