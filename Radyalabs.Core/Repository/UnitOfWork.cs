using Radyalabs.Core.LogHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Radyalabs.Core.Repository
{
    public class UnitOfWork<C> : IUnitOfWork where C : DbContext
    {
        private DbTransaction _transaction;
        private Dictionary<Type, object> _repositories;

        private C _ctx;

        private ILogHelper _logHelper;

        public UnitOfWork()
        {
            _ctx = Activator.CreateInstance<C>();
            _repositories = new Dictionary<Type, object>();

            _logHelper = new Log4NetHelper();
        }

        public IRepository<TSet> GetRepository<TSet>() where TSet : class
        {
            if (_repositories.Keys.Contains(typeof(TSet)))
                return _repositories[typeof(TSet)] as IRepository<TSet>;

            var repository = new SqlRepository<TSet, C>(_ctx);

            _repositories.Add(typeof(TSet), repository);

            return repository;
        }

        public void BeginTransaction()
        {
            var adapter = (IObjectContextAdapter)_ctx;
            var objectContext = adapter.ObjectContext;

            if (_transaction == null)
            {
                if (objectContext.Connection.State != ConnectionState.Open)
                {
                    objectContext.Connection.Open();
                }
                _transaction = objectContext.Connection.BeginTransaction();
            }
        }

        public string Commit()
        {
            string err = null;

            try
            {
                if (_ctx.SaveChanges() >= 0)
                {
                    if (_transaction != null)
                    {
                        _transaction.Commit();
                    }
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                if (_transaction != null)
                {
                    _transaction.Rollback();
                }

                string errMessage = "";

                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        errMessage += Environment.NewLine + string.Format("Property: {0} Error: {1}",
                        validationError.PropertyName, validationError.ErrorMessage);
                    }
                }

                if (!string.IsNullOrEmpty(errMessage))
                {
                    errMessage = errMessage.Trim(System.Environment.NewLine.ToCharArray());

                    _logHelper.Write("DBTransactionError", DateTime.Now, errMessage, "System");
                }

                return errMessage;
            }
            catch (Exception ex)
            {
                _logHelper.Write("DBTransactionError", DateTime.Now, null, "System", ex);

                return ex.ToString();
            }
            finally
            {
                _transaction.Dispose();
            }

            return err;
        }

        public void Rollback()
        {
            if (_transaction == null) return;

            try
            {
                _transaction.Rollback();
            }
            finally
            {
                _transaction.Dispose();
            }
        }

        #region IDisposable Members

        public void Dispose()
        {
            if (null != _transaction)
            {
                _transaction.Dispose();
            }

            if (null != _ctx)
            {
                _ctx.Dispose();
            }
        }

        #endregion
    }
}
