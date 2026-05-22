using System.Linq.Expressions;
using System.Reflection;

public static class DynamicQueryExtensions
{
    public static IQueryable<T> TextFilter<T>(this IQueryable<T> source, string term)
    {
        if (source is null)
            throw new ArgumentNullException(nameof(source));

        if (string.IsNullOrWhiteSpace(term))
            return source;

        Type elementType = typeof(T);
        PropertyInfo[] stringProperties = elementType.GetProperties()
            .Where(prop => prop.PropertyType == typeof(string))
            .ToArray();

        if (!stringProperties.Any())
            return source;

        string normalizedTerm = term.Trim().ToLowerInvariant();
        MethodInfo toLowerMethod = typeof(string).GetMethod(nameof(string.ToLowerInvariant), Type.EmptyTypes)!;
        MethodInfo containsMethod = typeof(string).GetMethod(nameof(string.Contains), new[] { typeof(string) })!;

        ParameterExpression parameter = Expression.Parameter(elementType, "x");
        Expression? body = null;

        foreach (PropertyInfo property in stringProperties)
        {
            Expression propertyAccess = Expression.Property(parameter, property);
            Expression notNull = Expression.NotEqual(propertyAccess, Expression.Constant(null, typeof(string)));
            Expression lowerValue = Expression.Call(propertyAccess, toLowerMethod);
            Expression containsTerm = Expression.Call(lowerValue, containsMethod, Expression.Constant(normalizedTerm));
            Expression filterExpression = Expression.AndAlso(notNull, containsTerm);

            body = body is null ? filterExpression : Expression.OrElse(body, filterExpression);
        }

        if (body is null)
            return source;

        Expression<Func<T, bool>> lambda = Expression.Lambda<Func<T, bool>>(body, parameter);
        return source.Where(lambda);
    }

    public static IQueryable TextFilter(this IQueryable source, string term)
    {
        if (source is null)
            throw new ArgumentNullException(nameof(source));

        if (string.IsNullOrWhiteSpace(term))
            return source;

        Type elementType = source.ElementType;
        PropertyInfo[] stringProperties = elementType.GetProperties()
            .Where(prop => prop.PropertyType == typeof(string))
            .ToArray();

        if (!stringProperties.Any())
            return source;

        MethodInfo toLowerMethod = typeof(string).GetMethod(nameof(string.ToLowerInvariant), Type.EmptyTypes)!;
        MethodInfo containsMethod = typeof(string).GetMethod(nameof(string.Contains), new[] { typeof(string) })!;

        ParameterExpression parameter = Expression.Parameter(elementType, "x");
        Expression? body = null;
        Expression termExpression = Expression.Constant(term.Trim().ToLowerInvariant());

        foreach (PropertyInfo property in stringProperties)
        {
            Expression propertyAccess = Expression.Property(parameter, property);
            Expression notNull = Expression.NotEqual(propertyAccess, Expression.Constant(null, typeof(string)));
            Expression lowerValue = Expression.Call(propertyAccess, toLowerMethod);
            Expression containsTerm = Expression.Call(lowerValue, containsMethod, termExpression);
            Expression filterExpression = Expression.AndAlso(notNull, containsTerm);

            body = body is null ? filterExpression : Expression.OrElse(body, filterExpression);
        }

        if (body is null)
            return source;

        LambdaExpression lambda = Expression.Lambda(body, parameter);

        Expression whereCall = Expression.Call(
            typeof(Queryable),
            nameof(Queryable.Where),
            new[] { elementType },
            source.Expression,
            lambda
        );

        return source.Provider.CreateQuery(whereCall);
    }
}
