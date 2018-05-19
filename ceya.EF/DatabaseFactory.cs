using System;

namespace ceya.EF
{
    public class DatabaseFactory : Disposable, IDatabaseFactory
    {
        private GestionComercialWebEntities dataContext;
        public GestionComercialWebEntities Get()
        {
            return dataContext ?? (dataContext = new GestionComercialWebEntities());
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
