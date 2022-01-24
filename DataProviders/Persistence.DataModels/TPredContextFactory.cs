using System;
using System.Configuration;
using System.Data.Entity.Infrastructure;
using System.Reflection;

namespace Persistence.DataModel
{
    public class TPredContextFactory : IDbContextFactory<TPredDB>
    {
        public TPredDB Create()
        {
            //var path = ConfigurationManager.GetConfigurationPath();
            //var configuration = ConfigurationManager.OpenConfiguration(path);

            //var path = Assembly.GetExecutingAssembly().CodeBase;

            //var configuration = System.Configuration.ConfigurationManager.OpenExeConfiguration(new Uri(path).LocalPath);

            //return new TPredDB(configuration
            //    .ConnectionStrings
            //    .ConnectionStrings["TPredDB"]
            //    .ConnectionString);

            var connectionSrting = @"Server = tcp:tpred.database.windows.net,1433; Initial Catalog = TPred; Persist Security Info = False; User ID = avlo2000; Password = Mmmm0212; MultipleActiveResultSets = False; Encrypt = True; TrustServerCertificate = False; Connection Timeout = 30;";

            return new TPredDB(connectionSrting);
        }
    }
}
