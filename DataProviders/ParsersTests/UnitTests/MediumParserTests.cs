using MediumParser;
using Parsers.HtmlProviding;
using ParsersTests.Mockups;
using Persistence.DataModel;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ParsersTests.UnitTests
{
    public class MediumParserTests
    {
        [Fact]
        public void TestSize()
        {
            string url = "https://medium.com/topic/programming";

            var articlePageParser = new ArticlePageParser(new HtmlProviderDeploy());//Change to Mock
            var pageParsed = new PageParser(articlePageParser, url, "medium", new ArticleStoragerMock());

            var taskEnumUrls = pageParsed.GetArticlesAsync();

            taskEnumUrls.Wait();

            var enumUrls = taskEnumUrls.Result;

            List<Article> listUrls = enumUrls.ToList();

            Assert.Equal("medium", listUrls[0].SourceName);
        }

        [Fact]
        public void UrlTestArticle1()
        {
            string url = "https://medium.com/@jianbao.tao/become-a-software-engineer-without-a-computer-science-degree-4d29feb9ed76";

            var parser = new ArticlePageParser(new HtmlProviderDeploy());//Change to Mock

            var testedTask = parser.GetArticleAsync(url);
            testedTask.Wait();
            var testedArticle = testedTask.Result;

            Assert.Equal("Become a Software Engineer without a Computer Science degree", testedArticle.Name);
        }

        [Fact]
        public void UrlTestArticle2()
        {
            string url = "https://medium.com/s/story/readability-as-usability-78c5a2a373cc?source=topic_page---------------------------20";

            var parser = new ArticlePageParser(new HtmlProviderDeploy());//Change to Mock

            var testedTask = parser.GetArticleAsync(url);

            testedTask.Wait();
            var testedArticle = testedTask.Result;

            Assert.Null(testedArticle);
        }

        [Fact]
        public void UrlTestArticle3()
        {
            string url = "https://medium.com/@arjenbrandenburgh/your-angular-app-as-progressive-web-app-e481043acf65?source=topic_page---------0------------------1";

            var parser = new ArticlePageParser(new HtmlProviderDeploy());//Change to Mock

            var testedTask = parser.GetArticleAsync(url);
            testedTask.Wait();
            var testedArticle = testedTask.Result;

            Assert.Equal("Your Angular app as Progressive Web App", testedArticle.Name);
        }
    }
}
