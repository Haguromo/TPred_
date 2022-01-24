using HtmlAgilityPack;
using System.Threading.Tasks;

namespace Parsers.HtmlProviding
{
    public interface IHtmlProvider
    {
        /// <summary>
        /// This method asynctonously provides HTML markup from a page
        /// </summary>
        /// <param name="url">Url to page</param>
        /// <returns>HtmlDocument from AgilityWebPack library</returns>
        Task<HtmlDocument> ProvideAsync(string url);
    }
}
