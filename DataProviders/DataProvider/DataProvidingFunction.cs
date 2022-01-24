using DataProvider.Services;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace DataProvider
{
    public static class DataProvidingFunction
    {

        [FunctionName("DataProvidingFunction")]
        public static void Run([TimerTrigger("00:00:05")]TimerInfo myTimer, TraceWriter log, ExecutionContext context)
        {
            var sources = GetSources(context);
            IParsingService serv = new ParsingService();
            var addedSize = serv.Run(sources);
            log.Info($"Provider added " + addedSize.ToString() + " new articles.");
        }

        private static Dictionary<string, string> GetSources(ExecutionContext context)
        {
            var config = new ConfigurationBuilder()
            .SetBasePath(context.FunctionAppDirectory)
            .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
            .Build();

            var res = new Dictionary<string, string>();
            var sources = config.GetSection("Values").GetChildren();

            foreach(var source in sources)
                res.Add(source.Key, source.Value);
            return res;
        }
    }
}
