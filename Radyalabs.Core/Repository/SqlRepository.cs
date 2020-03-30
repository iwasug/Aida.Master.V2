using Radyalabs.Core.Helper;
using Radyalabs.Core.LogHelper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;

namespace Radyalabs.Core.Repository
{
    public class SqlRepository<T, C> : IRepository<T>
        where T : class
        where C : DbContext
    {
        private readonly C _context;

        private ParameterExpression _paramExpression;

        public Expression<Func<T, bool>> Condition { get; set; }

        public string[] Includes { get; set; }

        public SqlOrderBy OrderBy { get; set; }

        public SqlOrderBy ThenOrderBy { get; set; }

        public int Limit { get; set; }

        public int Offset { get; set; }

        private DateTime _currentTime;

        private ILogHelper _logHelper;

        public SqlRepository(C session)
        {
            _context = session;
            _context.Configuration.LazyLoadingEnabled = false;

            _paramExpression = Expression.Parameter(typeof(T), "p"); ;

            Condition = null;
            Includes = null;
            OrderBy = null;

            _currentTime = DateTime.Now;
            _logHelper = new Log4NetHelper();
        }

        private IQueryable<T> GetQueryWithCondition()
        {
            var query = _context.Set<T>().AsQueryable();

            if (Condition != null)
            {
                query = _context.Set<T>().Where(Condition);
            }

            if (Includes != null && Includes.Length > 0)
            {
                for (int i = 0; i < Includes.Length; i++)
                {
                    query = query.Include(Includes[i]);
                }
            }

            return query;
        }

        public List<T> RetrieveByQuery(IQueryable<T> query)
        {
            List<T> listEntity = new List<T>();

            try
            {
                listEntity = query.ToList();
            }
            catch (Exception ex)
            {
                _logHelper.Write("DBError", DateTime.Now, ex.Message, "System", ex);
            }

            return listEntity;
        }

        private Expression GetOrderExpression(string[] parts)
        {
            Expression exp = _paramExpression;

            string key = "";

            foreach (var part in parts)
            {
                exp = Expression.Property(exp, part);
                key = part;
            }

            return exp;
        }

        private IQueryable<T> GetQueryPreFind()
        {
            IQueryable<T> query = GetQueryWithCondition();

            IOrderedQueryable<T> orderQuery = null;

            #region order by
            if (OrderBy != null)
            {
                var parts = OrderBy.Column.Split('.');

                Expression parent = GetOrderExpression(parts);

                if (parent.Type.Equals(typeof(DateTime)))
                {
                    orderQuery = GetQueryOrder<DateTime>(query, OrderBy.Type, parent, _paramExpression);
                }
                else if (parent.Type.Equals(typeof(DateTime?)))
                {
                    orderQuery = GetQueryOrder<DateTime?>(query, OrderBy.Type, parent, _paramExpression);
                }
                else if (parent.Type.Equals(typeof(Int32)))
                {
                    orderQuery = GetQueryOrder<Int32>(query, OrderBy.Type, parent, _paramExpression);
                }
                else if (parent.Type.Equals(typeof(Int32?)))
                {
                    orderQuery = GetQueryOrder<Int32?>(query, OrderBy.Type, parent, _paramExpression);
                }
                else if (parent.Type.Equals(typeof(Decimal)))
                {
                    orderQuery = GetQueryOrder<Decimal>(query, OrderBy.Type, parent, _paramExpression);
                }
                else if (parent.Type.Equals(typeof(Decimal?)))
                {
                    orderQuery = GetQueryOrder<Decimal?>(query, OrderBy.Type, parent, _paramExpression);
                }
                else if (parent.Type.Equals(typeof(bool)))
                {
                    orderQuery = GetQueryOrder<bool>(query, OrderBy.Type, parent, _paramExpression);
                }
                else if (parent.Type.Equals(typeof(float)))
                {
                    SetQueryWithThenOrdering<float>(ref orderQuery, ThenOrderBy.Type, parent, _paramExpression);
                }
                else if (parent.Type.Equals(typeof(float?)))
                {
                    orderQuery = GetQueryOrder<float?>(query, OrderBy.Type, parent, _paramExpression);
                }
                else
                {
                    orderQuery = GetQueryOrder<object>(query, OrderBy.Type, parent, _paramExpression);
                }
            }
            #endregion

            #region then order by
            if (ThenOrderBy != null && orderQuery != null)
            {
                var parts = ThenOrderBy.Column.Split('.');

                Expression parent = GetOrderExpression(parts);

                if (parent.Type.Equals(typeof(DateTime)))
                {
                    SetQueryWithThenOrdering<DateTime>(ref orderQuery, ThenOrderBy.Type, parent, _paramExpression);
                }
                else if (parent.Type.Equals(typeof(DateTime?)))
                {
                    SetQueryWithThenOrdering<DateTime?>(ref orderQuery, ThenOrderBy.Type, parent, _paramExpression);
                }
                else if (parent.Type.Equals(typeof(Int32)))
                {
                    SetQueryWithThenOrdering<Int32>(ref orderQuery, ThenOrderBy.Type, parent, _paramExpression);
                }
                else if (parent.Type.Equals(typeof(Int32?)))
                {
                    SetQueryWithThenOrdering<Int32?>(ref orderQuery, ThenOrderBy.Type, parent, _paramExpression);
                }
                else if (parent.Type.Equals(typeof(bool)))
                {
                    SetQueryWithThenOrdering<bool>(ref orderQuery, ThenOrderBy.Type, parent, _paramExpression);
                }
                else if (parent.Type.Equals(typeof(float)))
                {
                    SetQueryWithThenOrdering<float>(ref orderQuery, ThenOrderBy.Type, parent, _paramExpression);
                }
                else if (parent.Type.Equals(typeof(float?)))
                {
                    orderQuery = GetQueryOrder<float?>(query, OrderBy.Type, parent, _paramExpression);
                }
                else
                {
                    SetQueryWithThenOrdering<object>(ref orderQuery, ThenOrderBy.Type, parent, _paramExpression);
                }
            }
            #endregion

            if (orderQuery != null)
            {
                query = orderQuery;
            }

            if (Limit > 0)
            {
                query = query.Skip(Offset).Take(Limit);
            }

            return query;
        }

        public List<TResult> Find<TResult>(Func<T, TResult> selector)
        {
            var query = GetQueryPreFind();

            try
            {
                return query.Select(selector).ToList();
            }
            catch (Exception ex)
            {
                _logHelper.Write("DBError", DateTime.Now, ex.Message, "System", ex);
                return new List<TResult>();
            }
        }

        public List<T> Find()
        {
            var query = GetQueryPreFind();

            try
            {
                return query.ToList();
            }
            catch (Exception ex)
            {
                var setting = ConfigurationManager.ConnectionStrings["APLPOAGEntities"];

                if (setting != null)
                {
                    _logHelper.Write("DBError", DateTime.Now, setting.ConnectionString, "System", ex);
                }

                return null;
            }

            //return RetrieveByQuery(query);
        }

        private IOrderedQueryable<T> GetQueryOrder<TKey>(IQueryable<T> query, string orderType, Expression parent, ParameterExpression param)
        {
            Expression<Func<T, TKey>> sortExp = Expression.Lambda<Func<T, TKey>>(parent, param);

            if (orderType.Equals(SqlOrderType.Ascending))
            {
                return query.OrderBy(sortExp);
            }
            else
            {
                return query.OrderByDescending(sortExp);
            }
        }

        private void SetQueryWithThenOrdering<TKey>(ref IOrderedQueryable<T> query, string orderType, Expression parent, ParameterExpression param)
        {
            Expression<Func<T, TKey>> sortExp = Expression.Lambda<Func<T, TKey>>(parent, param);

            if (orderType.Equals(SqlOrderType.Ascending))
            {
                query = query.ThenBy(sortExp);
            }
            else
            {
                query = query.ThenByDescending(sortExp);
            }
        }

        public long? CountByQuery(IQueryable<T> query)
        {
            long? result = null;

            try
            {
                result = query.Count();
            }
            catch (Exception ex)
            {
                _logHelper.Write("DBError", DateTime.Now, ex.Message, "System", ex);
            }

            return result;
        }

        public long? Count()
        {
            var query = GetQueryWithCondition();

            return CountByQuery(query);
        }

        public string Insert(T entity, bool isSaveChanges = false)
        {
            string errMessage = "";

            if (entity == null)
            {
                return "Entity is null";
            }

            try
            {
                LogDbQuery();

                _context.Set<T>().Add(entity);

                if (isSaveChanges)
                {
                    _context.SaveChanges();
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        errMessage += Environment.NewLine + string.Format("Property: {0} Error: {1}",
                        validationError.PropertyName, validationError.ErrorMessage);
                    }
                }

                _logHelper.Write("DBError", DateTime.Now, null, "System", dbEx);
            }

            return errMessage;
        }

        public string Update(T entity, bool isSaveChanges = false)
        {
            string errMessage = "";

            if (entity == null)
            {
                return "Entity is null";
            }

            try
            {
                LogDbQuery();

                _context.Set<T>().Attach(entity);
                _context.Entry(entity).State = EntityState.Modified;

                if (isSaveChanges)
                {
                    _context.SaveChanges();
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        errMessage += Environment.NewLine + string.Format("Property: {0} Error: {1}",
                        validationError.PropertyName, validationError.ErrorMessage);
                    }
                }

                _logHelper.Write("DBError", DateTime.Now, null, "System", dbEx);
            }

            return errMessage;
        }

        public string Delete(T entity, bool isSaveChanges = false)
        {
            string errMessage = "";

            if (entity == null)
            {
                return "Entity is null";
            }

            try
            {
                LogDbQuery();

                if (_context.Entry(entity).State == EntityState.Detached)
                {
                    _context.Set<T>().Attach(entity);
                }

                _context.Set<T>().Remove(entity);

                if (isSaveChanges)
                {
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                errMessage = ex.ToString();

                _logHelper.Write("DBError", DateTime.Now, ex.Message, "System", ex);
            }

            return errMessage;
        }

        public string Delete(Expression<Func<T, bool>> condition, bool isSaveChange = false)
        {
            string errMessage = "";

            try
            {
                LogDbQuery();

                _context.Set<T>().RemoveRange(_context.Set<T>().Where(condition));

                if (isSaveChange)
                {
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                errMessage = ex.ToString();
                _logHelper.Write("DBError", DateTime.Now, ex.Message, "System", ex);
            }

            return errMessage;
        }

        private void LogDbQuery()
        {
           //_context.Database.Log = message => _logHelper.Write("DBQuery", _currentTime,  message, null, false);
        }
    }
}