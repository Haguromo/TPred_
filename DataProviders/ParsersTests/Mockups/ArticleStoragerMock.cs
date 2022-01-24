using Persistence.Contracts;
using Persistence.DataModel;
using System;
using System.Collections.Generic;

namespace ParsersTests.Mockups
{
    public class ArticleStoragerMock : IArticleStorager
    {
        public IEnumerable<int> AddArticles(IEnumerable<Article> articles)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<int> AddArticles(Article article)
        {
            throw new NotImplementedException();
        }

        public bool UrlWasAdded(string url)
        {
            return false;
        }
    }
}
