using Persistence.Contracts;
using Persistence.Control;
using Persistence.DataModel;

using System.Data.Entity.Infrastructure;
using System.Linq;

using Xunit;

namespace Persistence.Tests
{
    public class ArticleStoragerUnitTests
    {
        [Fact]
        public void OneArticleAddTest()
        {
            IArticleStorager _articleStorager = new ArticleStorager();
            IDbContextFactory<TPredDB> _contextFactory = new TPredContextFactory();

            var article = new Article("name", "text", "google.com", "Google");

            var id = _articleStorager.AddArticles(article);

            using (var context = _contextFactory.Create())
            {
                Assert.Equal(article.GetStaticInfo(), context.Articles.Where(x => x.Id == id.FirstOrDefault()).FirstOrDefault().GetStaticInfo());
            }
        }
    }
}
