
using CodeprojectParser;
using Contracts;
using Parsers.HtmlProviding;
using ParsersTests.Mockups;
using Persistence.DataModel;
using System.Collections.Generic;
using System.Linq;

using Xunit;

namespace ParsersTests.UnitTests
{
    public class CodeprojectParserTests
	{
		[Fact]
		public void UrlTestArticle334773()
		{
			string url = "https://www.codeproject.com/Articles/334773/Graphical-BinaryTrees";

			var parser = new ArticlePageParser(new HtmlProviderDeploy());//Change to Mock

            var testedTask = parser.GetArticleAsync(url);
			testedTask.Wait();
			var testedArticle = testedTask.Result;

			Assert.Equal("Graphical BinaryTrees", testedArticle.Name);
		}

		[Fact]
		public void UrlTestArticle1262627()
		{
			string url = "https://www.codeproject.com/Articles/1262627/Enterprise-is-dead-long-live-Agile";

			var parser = new ArticlePageParser(new HtmlProviderDeploy());//Change to Mock

            var testedTask = parser.GetArticleAsync(url);
			testedTask.Wait();
			var testedArticle = testedTask.Result;

			Assert.Equal("Enterprise is dead, long live Agile!", testedArticle.Name);
		}

		[Fact]
		public void UrlTestArticle1211510()
		{
			string url = "https://www.codeproject.com/Tips/1211510/DLL-Registry-Control-Easier";

			IArticlePageParser parser = new ArticlePageParser(new HtmlProviderDeploy());//Change to Mock

            var testedTask = parser.GetArticleAsync(url);
			testedTask.Wait();
			var testedArticle = testedTask.Result;

			Assert.Equal("DLL Registry Control Easier", testedArticle.Name);
		}

		[Fact]
		public void TestSize()
		{
			string url = "https://www.codeproject.com/script/Articles/Latest.aspx";

            var articlePageParser = new ArticlePageParser(new HtmlProviderDeploy());//Change to Mock
            var pageParsed = new PageParser(articlePageParser, url, "codeproject", new ArticleStoragerMock());

			var taskEnumUrls = pageParsed.GetArticlesAsync();
			taskEnumUrls.Wait();
			var enumUrls = taskEnumUrls.Result;
			List<Article> listUrls = enumUrls.ToList();

			Assert.Equal(20, listUrls.Count());
		}
	}
}
