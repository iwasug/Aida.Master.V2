using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;

namespace Radyalabs.Core.Helper
{
    public static class IQueryableExtensions
    {
        /// <summary>
        /// For an Entity Framework IQueryable, returns the SQL with inlined Parameters.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <returns></returns>
        public static string ToTraceQuery<T>(this IQueryable<T> query)
        {
            ObjectQuery<T> objectQuery = GetQueryFromQueryable(query);

            var result = objectQuery.ToTraceString();
            foreach (var parameter in objectQuery.Parameters)
            {
                var name = "@" + parameter.Name;
                var value = "'" + parameter.Value.ToString() + "'";
                result = result.Replace(name, value);
            }

            return result;
        }

        /// <summary>
        /// For an Entity Framework IQueryable, returns the SQL and Parameters.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <returns></returns>
        public static string ToTraceString<T>(this IQueryable<T> query)
        {
            ObjectQuery<T> objectQuery = GetQueryFromQueryable(query);

            var traceString = new StringBuilder();

            traceString.AppendLine(objectQuery.ToTraceString());
            traceString.AppendLine();

            foreach (var parameter in objectQuery.Parameters)
            {
                traceString.AppendLine(parameter.Name + " [" + parameter.ParameterType.FullName + "] = " + parameter.Value);
            }

            return traceString.ToString();
        }

        private static System.Data.Entity.Core.Objects.ObjectQuery<T> GetQueryFromQueryable<T>(IQueryable<T> query)
        {
            var internalQueryField = query.GetType().GetFields(System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).Where(f => f.Name.Equals("_internalQuery")).FirstOrDefault();
            var internalQuery = internalQueryField.GetValue(query);
            var objectQueryField = internalQuery.GetType().GetFields(System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).Where(f => f.Name.Equals("_objectQuery")).FirstOrDefault();
            return objectQueryField.GetValue(internalQuery) as System.Data.Entity.Core.Objects.ObjectQuery<T>;
        }
    }
}
