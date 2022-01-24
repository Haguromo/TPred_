using Persistence.DataModel;

using System.Collections.Generic;

namespace Persistence.Contracts
{
    public interface IArticleStorager
    {
        /// <summary>
        /// Add collection of articles to TPredDB.
        /// </summary>
        /// <param name="articles">Articles that will be added to TPredDB. They must be sorted form oldest to newest</param>
        /// <returns>Added articles id's</returns>
        IEnumerable<int> AddArticles(IEnumerable<Article> articles);

        /// <summary>
        /// Add article to TPredDB.
        /// </summary>
        /// <param name="article">Article that will be added to TPredDB</param>
        /// <returns>Added article id</returns>
        IEnumerable<int> AddArticles(Article article);

        /// <summary>
        /// Shows if an article exists in database
        /// </summary>
        /// <param name="sourceName">Url to article</param>
        /// <returns></returns>
        bool UrlWasAdded(string url);

    }
}
