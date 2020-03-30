using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Radyalabs.Core.Repository
{
    public interface IUnitOfWork
    {
        IRepository<TSet> GetRepository<TSet>() where TSet : class;

        void BeginTransaction();
        
        string Commit();

        void Rollback();

        void Dispose();
    }
}
