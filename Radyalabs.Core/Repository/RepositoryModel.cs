using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Radyalabs.Core.Repository
{
    public class SqlOrderBy
    {
        public string Column { get; set; }

        public string Type { get; set; }

        public SqlOrderBy() { }

        public SqlOrderBy(string c, string t)
        {
            Column = c;
            Type = t;
        }
    }

    //public enum SqlOrderType { Descending, Ascending }

    public class SqlOrderType 
    {
        public static readonly string Ascending = "asc";
        public static readonly string Descending = "desc";
    }
}
