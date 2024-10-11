using System.Linq.Expressions;
namespace C.Common.Extensions;

public static class QueryFilterExtension
{
    public static IQueryable<T> WhereIf<T>(IQueryable<T> source, object? value, Expression<Func<T, bool>> predicate)
    {
        if (value is null)
        {
            return source;
        }

        if (value is string x && string.IsNullOrEmpty(x)) { return source; }

        return source.Where(predicate).AsQueryable();
    }

}