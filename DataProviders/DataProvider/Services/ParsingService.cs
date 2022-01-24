using DataProvider.DependencyResolving;
using System.Collections.Generic;
using System.Linq;

namespace DataProvider.Services
{
    class ParsingService : IParsingService
    {
        public int Run(Dictionary<string, string> sources)
        {
            var resolver = new DependencyResolver(sources);

            var storager = resolver.BuildStorager();
            var factory = resolver.BuildParsers();

            var atricles = factory.Run();

            storager.AddArticles(atricles);
            
            return atricles.Cast<object>().Count();
        }
    }
}
