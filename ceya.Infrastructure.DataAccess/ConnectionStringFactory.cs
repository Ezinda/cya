using ceya.Core;
using System;

namespace ceya.Infrastructure.DataAccess
{
    public class ConnectionStringFactory : IConnectionStringFactory
    {
        private string connectionString;
        public string Get()
        {
            return connectionString ?? (connectionString = ConnectionStringHelper.GetConnectionString());
        }
    }

    public interface IConnectionStringFactory
    {
        string Get();
    }
}
