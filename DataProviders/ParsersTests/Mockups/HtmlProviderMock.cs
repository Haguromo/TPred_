using System.Threading.Tasks;
using HtmlAgilityPack;

namespace Parsers.HtmlProviding
{
    public class HtmlProviderDiagnostic : IHtmlProvider
    {
        public Task<HtmlDocument> ProvideAsync(string path)
        {
            var html = new HtmlDocument();
            html.Load(path);

            return new Task<HtmlDocument>(() => html);
        }
    }
}
