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

public static class ExpressionSeed
{
    public static Expression<Func<T, bool>> True<T>() { return f => true; }
    
    public static Expression<Func<T, bool>> False<T>() { return f => false; }
}

void Main()
{
    var data = new[]{2,4,6,8,11,43,65};
    var greaterThanFourAndLessThanForty = ExpressionSeed.True<int>()
        .And<int>(i => i > 4)
        .And<int>(i => i < 40);
    data.AsQueryable().Where(greaterThanFourAndLessThanForty).Dump();
}
