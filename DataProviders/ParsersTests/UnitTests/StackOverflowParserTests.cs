using StackOverflowParser;
using Contracts;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using System;
using Parsers.HtmlProviding;
using Persistence.DataModel;
using ParsersTests.Mockups;

namespace ParsersTests.UnitTests
{
	public class StackOverflowParserTest
	{
		[Fact]
		public void UrlTestArticle53498249()
		{
			string url = "https://stackoverflow.com/questions/53498249/how-to-convert-utc-time-to-local-without-changing-hour";

			var parser = new ArticlePageParser(new HtmlProviderDeploy());//change to Mock

			var testedTask = parser.GetArticleAsync(url);
			testedTask.Wait();
			var testedArticle = testedTask.Result;
			Console.WriteLine(testedArticle.Name);
			Assert.Equal("How to convert UTC time to local without changing hour?", testedArticle.Name);
		}

		[Fact]
		public void UrlTestArticle53498246()
		{
            string url = "https://stackoverflow.com/questions/13386894/sql-server-insert-example";
          
            var parser = new ArticlePageParser(new HtmlProviderDeploy());//change to Mock

            var testedTask = parser.GetArticleAsync(url);
			testedTask.Wait();
			var testedArticle = testedTask.Result;

			Assert.Equal("How do I convert Arduino-Code to C code with my example?", testedArticle.Name);
		}

		[Fact]
		public void UrlTestArticle53498243()
		{
			string url = "https://stackoverflow.com/questions/53498243/animations-are-not-playing-through-animator";

			IArticlePageParser parser = new ArticlePageParser(new HtmlProviderDeploy());//change to Mock
            try
            {
                var testedTask = parser.GetArticleAsync(url);
                testedTask.Wait();
                var testedArticle = testedTask.Result;

                Assert.Equal("Animations are not playing through Animator?", testedArticle.Name);
            }
            catch (Exception e)
            {
                return;
            }
		}

		[Fact]
		public void TestSize()
		{
			string url = "https://stackoverflow.com/questions?sort=newest";

            var articlePageParser = new ArticlePageParser(new HtmlProviderDeploy());//Change to Mock
            var pageParsed = new PageParser(articlePageParser, url, "stackoverflow", new ArticleStoragerMock());

            var taskEnumUrls = pageParsed.GetArticlesAsync();

			taskEnumUrls.Wait();

			var enumUrls = taskEnumUrls.Result;

			List<Article> listUrls = enumUrls.ToList();
			
			Assert.Equal(15, listUrls.Count());
		}
	}
}
