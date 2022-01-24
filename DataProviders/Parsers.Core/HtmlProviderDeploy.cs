using HtmlAgilityPack;
using Parsers.Core.Exceptions;
using System;
using System.Threading.Tasks;

namespace Parsers.HtmlProviding
{
    public sealed class HtmlProviderDeploy : IHtmlProvider
    {
        public async Task<HtmlDocument> ProvideAsync(string url)
        {
            try
            {
                var html = await new HtmlWeb().LoadFromWebAsync(url);
                return html;
            }
            catch(Exception e)
            {
                throw new FailedToLoadHtmlException();
            }         
        }
    }
}
