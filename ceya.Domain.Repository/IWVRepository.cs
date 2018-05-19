using ceya.Core;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using X.PagedList;

namespace ceya.Domain.Repository
{
    public interface IWVRepository<T> where T : class
    {
        T GetById(Guid id);
        T GetById(string id);
        T Get(Expression<Func<T, bool>> where);
        bool Exists(Expression<Func<T, bool>> where);
        IEnumerable<T> GetAll();
        IEnumerable<T> GetMany(Expression<Func<T, bool>> where);
        IPagedList<T> GetPage<TOrder>(Page page, Expression<Func<T, bool>> where, Expression<Func<T, TOrder>> order);
    }

}
