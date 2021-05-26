using System;
using System.Linq;
using System.Linq.Expressions;

namespace MedicalResearch.Extensions
{
    public static class ApplySortExtension
    {
        public static IQueryable<T> OrderByPropertyName<T>(this IQueryable<T> q, string sortField, bool asc)
        {
            var param = Expression.Parameter(typeof(T), "p");
            var prop = Expression.Property(param, sortField);
            var exp = Expression.Lambda(prop, param);
            var method = asc ? nameof(Queryable.OrderBy) : nameof(Queryable.OrderByDescending);
            var types = new Type[] { q.ElementType, exp.Body.Type };
            var rs = Expression.Call(typeof(Queryable), method, types, q.Expression, exp);
            return q.Provider.CreateQuery<T>(rs);
        }
    }
}
