using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace MiniQuick.Infrastructure.Repository
{
    public interface IRepository
    {
        IEnumerable<T> QueryAll<T>() where T : class, new();
        IEnumerable<T> QueryBy<T>(Expression<Func<T, bool>> predicate) where T : class, new();
        T FindBy<T>(Expression<Func<T, bool>> predicate) where T : class, new();
        T First<T>(Expression<Func<T, bool>> predicate) where T : class, new();
        void Create<T>(T entity) where T : class, new();
        void Update<T>(T entity) where T : class,  new();
        void Delete<T>(T entity) where T : class,  new();
    }

    
}
