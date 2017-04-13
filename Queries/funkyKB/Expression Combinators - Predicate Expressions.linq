<Query Kind="Program" />

public static class ExpressionExtensions
{
    public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> thisExpression, Expression<Func<T, bool>> expression)
    {
        var invokedExpr = Expression.Invoke(expression, thisExpression.Parameters.Cast<Expression>());
        return Expression.Lambda<Func<T, bool>>(Expression.OrElse(thisExpression.Body, invokedExpr), thisExpression.Parameters);
    }

    public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> thisExpression, Expression<Func<T, bool>> expression)
    {
        var invokedExpr = Expression.Invoke(expression, thisExpression.Parameters.Cast<Expression>());
        return Expression.Lambda<Func<T, bool>>(Expression.AndAlso(thisExpression.Body, invokedExpr), thisExpression.Parameters);
    }
}

public static class ExpressionCombinator
{
    public static Expression<Func<T, bool>> Combine<T>(bool @bool = true) { return f => @bool; }
}

void Main()
{
    var data = new[] { 2, 4, 6, 8, 11, 43, 65 };
    var greaterThanFourAndLessThanForty = ExpressionCombinator.Combine<int>()
        .And<int>(i => i > 4)
        .And<int>(i => i < 40);
    data.AsQueryable().Where(greaterThanFourAndLessThanForty).Dump();
}