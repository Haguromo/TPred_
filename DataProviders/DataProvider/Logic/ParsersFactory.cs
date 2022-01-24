using Contracts;
using Persistence.DataModel;
using System.Collections.Generic;

namespace DataProvider.DependencyResolving
{
    public class ParsersFactory
    {
        private readonly IEnumerable<PageParserBase> _parsers;

        public ParsersFactory(IEnumerable<PageParserBase> parsers)
        {
            _parsers = parsers;
        }

        public IEnumerable<Article> Run()
        {
            var articles = new List<Article>();

            var batches = new List<IEnumerable<Article>>();

            foreach (var parser in _parsers)
            {
                var batch = parser.GetArticlesAsync();
                batch.Wait();
                batches.Add(batch.Result);
            }
                
            foreach (var batch in batches)
                foreach (var article in batch)
                    articles.Add(article);

            return articles;
        }
    }
}
