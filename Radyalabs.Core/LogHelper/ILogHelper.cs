using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Radyalabs.Core.LogHelper
{
    public interface ILogHelper
    {
        void Write(string logtype, DateTime dt, string message, string createBy, Exception ex = null, bool isWriteTime = true);
    }
}
