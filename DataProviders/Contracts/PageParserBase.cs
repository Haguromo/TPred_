using Persistence.Contracts;
using Persistence.DataModel;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contracts
{
    public abstract class PageParserBase
    {
        /// <summary>
        /// method for getting list of articles from a specific site
        /// </summary>
        /// <param name="articleParser">parser that is used for this html markup</param>
        /// <returns></returns>
        public async Task<IEnumerable<Article>> GetArticlesAsync()
        {
            var urls = GetArticlesUrls(_url);

            var tasks = from u in urls
                        where !_storager.UrlWasAdded(u) 
                        select _articleParser.GetArticleAsync(u);

            var articles = (await Task.WhenAll(tasks)).Where(a => a != null).ToList();

            for (var i = 0; i < articles.Count; i++)
                articles[i].SourceName = _sourceName;

            return articles;
        }

        protected IArticlePageParser _articleParser;

        protected abstract List<string> GetArticlesUrls(string url);

        protected readonly string _url;

        protected readonly string _sourceName;

        protected IArticleStorager _storager;

        protected PageParserBase(IArticlePageParser articleParser, string url, string sourceName, IArticleStorager storager)
        {
            _articleParser = articleParser;
            _url = url;
            _sourceName = sourceName;
            _storager = storager;
        }
    }
}
