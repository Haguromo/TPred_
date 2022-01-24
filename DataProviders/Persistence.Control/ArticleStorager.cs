using Persistence.Contracts;
using Persistence.DataModel;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace Persistence.Control
{
    public class ArticleStorager : IArticleStorager
    {
        private IDbContextFactory<TPredDB> _contextFactory;

        public ArticleStorager()
        {
            _contextFactory = new TPredContextFactory();
        }

        /// <summary>
        /// Add collection of articles to TPredDB.
        /// </summary>
        /// <param name="articles">Articles that will be added to TPredDB. They must be sorted form oldest to newest</param>
        /// <returns>Added articles id's</returns>
        public IEnumerable<int> AddArticles(IEnumerable<Article> articles)
        {
            using (var context = _contextFactory.Create())
            {
                context.Articles.AddRange(articles);

                try
                {
                    context.SaveChanges();
                }
                catch
                {

                }
            }

            return articles.Select(x => x.Id);
        }

        /// <summary>
        /// Add article to TPredDB.
        /// </summary>
        /// <param name="article">Article that will be added to TPredDB</param>
        /// <returns>Added article id</returns>
        public IEnumerable<int> AddArticles(Article article)
        {
            using (var context = _contextFactory.Create())
            {
                context.Articles.Add(article);
                context.SaveChanges();
            }

            return new List<int>
            {
                article.Id
            };
        }

        /// <summary>
        /// Get last added to TPredDB URL of specified source
        /// </summary>
        /// <param name="sourceName">Name of souce website</param>
        /// <returns></returns>
        public bool UrlWasAdded(string url)
        {
            using (var context = _contextFactory.Create())
            {
                try
                {
                    var res = context.Articles.Where(a => a.Url == url);
                    return res.Count() != 0;
                }
                catch (Exception e)
                {
                    return true;
                }
            }
        }
    }
}
