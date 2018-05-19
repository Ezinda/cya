using System;
using System.Collections.Generic;
using System.Data.Entity.Core.EntityClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ceya.Infrastructure.DataAccess
{
    public static class ConnectionStringHelper
    {
        public const string DefaultConnectionName = "GestionComercialWebEntities";

        public static string GetConnectionString()
        {
            var envConnectionString = System.Environment.GetEnvironmentVariable("CONNECTION_STRING");
            var envServer = System.Environment.GetEnvironmentVariable("CONNECTION_SERVER");
            var envDbName = System.Environment.GetEnvironmentVariable("CONNECTION_DBNAME");
            var envIntegrated = System.Environment.GetEnvironmentVariable("CONNECTION_INTEGRATED");
            var envUsername = System.Environment.GetEnvironmentVariable("CONNECTION_USERNAME");
            var envPassword = System.Environment.GetEnvironmentVariable("CONNECTION_PASSWORD");
            var envMultiple = System.Environment.GetEnvironmentVariable("CONNECTION_MULTIPLE");
            var isIntegrated = !(envIntegrated == "F" || envIntegrated == "FALSE" || envIntegrated == "0");
            var isMultiple = !(envMultiple == "F" || envMultiple == "FALSE" || envMultiple == "0");

            if (!string.IsNullOrEmpty(envConnectionString))
            {
                return envConnectionString;
            }
            else if (!string.IsNullOrEmpty(envServer) && !string.IsNullOrEmpty(envDbName))
            {
                var entityBuilder = new EntityConnectionStringBuilder();

                entityBuilder.Provider = "System.Data.SqlClient";

                if (!isIntegrated && !string.IsNullOrEmpty(envUsername) && !string.IsNullOrEmpty(envPassword))
                {
                    entityBuilder.ProviderConnectionString = _BuildConnectionStringProvider(envServer, envDbName, envUsername, envPassword, false, isMultiple);
                }
                else 
                {
                    entityBuilder.ProviderConnectionString = _BuildConnectionStringProvider(envServer, envDbName, null, null, true, isMultiple);
                }

                return entityBuilder.ConnectionString;
            }

            return DefaultConnectionName;
        }

        private static string _BuildConnectionStringProvider(string datasource,
            string initialCatalog,
            string userId,
            string password,
            bool useIntegratedSecurity,
            bool useMultipleActiveResultSets)
        {
            // provider connection string=
            // &quot;
            //      data source=25.120.114.66;initial catalog=cim_web;user id=fs1;password=1;MultipleActiveResultSets=True;App=EntityFramework
            //      o data source=K-VIPER\SQLEXPRESS2012;initial catalog=cim_desktop_migracion;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework
            // &quot

            string connectionString =
                "data source={0};" +
                "initial catalog={1};" +
                "user id={2};" +
                "password={3};" +
                "MultipleActiveResultSets={4};" +
                "App={5}";
            string localConnectionString =
                "data source={0};" +
                "initial catalog={1};" +
                "integrated security={2};" +
                "MultipleActiveResultSets={3}" +
                ";App={4}";

            if (useIntegratedSecurity == true)
            {
                return String.Format(localConnectionString,
                    datasource,
                    initialCatalog,
                    "True",
                    useMultipleActiveResultSets ? "True" : "False",
                    "EntityFramework");
            }
            else
            {
                return String.Format(connectionString,
                    datasource,
                    initialCatalog,
                    userId,
                    password,
                    useMultipleActiveResultSets ? "True" : "False",
                    "EntityFramework");
            }
        }
    }
}
