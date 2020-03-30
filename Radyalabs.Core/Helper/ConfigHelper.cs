using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Core.EntityClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Radyalabs.Core.Helper
{
    public class ConfigHelper
    {
        public static string GetConnectionString(string connectionName)
        {
            string connectionStringWithMetadata = ConfigurationManager.ConnectionStrings[connectionName].ConnectionString;

            EntityConnectionStringBuilder entityConnectionStringBuilder = new EntityConnectionStringBuilder(connectionStringWithMetadata);

            return entityConnectionStringBuilder.ProviderConnectionString;
        }

        public static string GetValue(string key)
        {
            return ConfigurationManager.AppSettings.Get(key);
        }
    }
}
