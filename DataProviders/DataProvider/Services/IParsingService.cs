using System.Collections.Generic;

namespace DataProvider.Services
{
    public interface IParsingService
    {
        int Run(Dictionary<string, string> sources);
    }
}
