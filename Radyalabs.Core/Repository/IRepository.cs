using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Radyalabs.Core.Repository
{
    public interface IRepository<T>
    {
        Expression<Func<T, bool>> Condition { get; set; }

        string[] Includes { get; set; }

        SqlOrderBy OrderBy { get; set; }

        SqlOrderBy ThenOrderBy { get; set; }

        int Limit { get; set; }

        int Offset { get; set; }

        List<T> RetrieveByQuery(IQueryable<T> query);

        List<TResult> Find<TResult>(Func<T, TResult> selector);
        
        List<T> Find();

        long? CountByQuery(IQueryable<T> query);

        long? Count();

        string Insert(T entity, bool isSaveChanges = false);

        string Update(T entity, bool isSaveChanges = false);

        string Delete(T entity, bool isSaveChanges = false);

        string Delete(Expression<Func<T, bool>> condition, bool isSaveChange = false);
    }
}
