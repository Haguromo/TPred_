using Persistence.DataModel;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IArticlePageParser
    {
        Task<Article> GetArticleAsync(string url);
    }
}
