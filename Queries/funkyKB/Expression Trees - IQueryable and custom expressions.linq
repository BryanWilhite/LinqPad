<Query Kind="Program">
  <Namespace>System.Data.Common</Namespace>
</Query>

void Main()
{
    var companies = new[]
    {
        "Consolidated Messenger",
        "Alpine Ski House",
        "Southridge Video",
        "City Power & Light",
        "Coho Winery",
        "Wide World Importers",
        "Graphic Design Institute",
        "Adventure Works",
        "Humongous Insurance",
        "Woodgrove Bank",
        "Margie's Travel",
        "Northwind Traders",
        "Blue Yonder Airlines",
        "Trey Research",
        "The Phone Company",
        "Wingtip Toys",
        "Lucerne Publishing",
        "Fourth Coffee"
    }
    .AsQueryable<string>();

    companies.QueryWithCustomExpressions().Dump();
}

static class IQueryableExtensions
{
    //ref: https://msdn.microsoft.com/en-us/library/mt654267.aspx
    public static IQueryable<string> QueryWithCustomExpressions(this IQueryable<string> queryable)
    {
        ParameterExpression i = Expression.Parameter(typeof(string), "i");

        Expression left = Expression.Call(i, typeof(string).GetMethod("ToLower", Type.EmptyTypes));
        Expression right = Expression.Constant("coho winery");
        Expression e1 = Expression.Equal(left, right);

        left = Expression.Property(i, typeof(string).GetProperty("Length"));
        right = Expression.Constant(16, typeof(int));
        Expression e2 = Expression.GreaterThan(left, right);
        Expression predicateBody = Expression.OrElse(e1, e2);

        MethodCallExpression whereCallExpression = queryable.GetWhereExpression(predicateBody, i);
        MethodCallExpression orderByCallExpression = queryable.GetOrderByExpression(whereCallExpression, i);

        orderByCallExpression.Dump("expression");

        return queryable.Provider.CreateQuery<string>(orderByCallExpression);
    }

    public static MethodCallExpression GetWhereExpression(this IQueryable<string> queryable, Expression predicateBody, ParameterExpression parameterExpression)
    {
        return Expression.Call(
            typeof(Queryable),
            "Where",
            new Type[] { queryable.ElementType },
            queryable.Expression,
            Expression.Lambda<Func<string, bool>>(predicateBody, new ParameterExpression[] { parameterExpression })
            );
    }

    public static MethodCallExpression GetOrderByExpression(this IQueryable<string> queryable, Expression rootExpression, ParameterExpression parameterExpression)
    {
        return Expression.Call(
            typeof(Queryable),
            "OrderBy",
            new Type[] { queryable.ElementType, queryable.ElementType },
            rootExpression,
            Expression.Lambda<Func<string, string>>(parameterExpression, new ParameterExpression[] { parameterExpression })
            );
    }
}