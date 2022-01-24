using Persistence.DataModel;

namespace Persistence.Tests
{
    internal static class ArticleExtensions
    {
        internal static string GetStaticInfo(this Article article)
        {
            return string.Format("Name: {0},\n Text: {1},\n Url: {2},\n SourceName: {3}", article.Name, article.Text, article.Url, article.SourceName);
        }
    }
}
