using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.DataModel
{
    internal class ConfigurationManager
    {
        public static string GetConfigurationPath()
        {
            return Assembly.GetExecutingAssembly().CodeBase;
        }

        public static Configuration OpenConfiguration(string filePath)
        {
            var configFileMap = new ExeConfigurationFileMap(new Uri(filePath).LocalPath);

            return System.Configuration.ConfigurationManager.OpenMappedExeConfiguration(configFileMap, ConfigurationUserLevel.None);
        }
    }
}
