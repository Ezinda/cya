using ceya.Core;
using System;

namespace ceya.Infrastructure.DataAccess
{
    public class DatabaseFactory : Disposable, IDatabaseFactory
    {
        private IConnectionStringFactory connectionStringFactory;
        private GestionComercialWebEntities dataContext;

        public DatabaseFactory(IConnectionStringFactory connectionStringFactory)
        {
            this.connectionStringFactory = connectionStringFactory;
        }

        public GestionComercialWebEntities Get()
        {
            return dataContext ?? (dataContext = new GestionComercialWebEntities(connectionStringFactory.Get()));
        }
        protected override void DisposeCore()
        {
            if (dataContext != null)
                dataContext.Dispose();
        }
    }

    public interface IDatabaseFactory : IDisposable
    {
        GestionComercialWebEntities Get();
    }
}
