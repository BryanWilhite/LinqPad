<Query Kind="Program">
  <NuGetReference>Dapper</NuGetReference>
  <NuGetReference>Dapper.SqlBuilder</NuGetReference>
  <Namespace>System.Data.Common</Namespace>
</Query>

//ref: http://www.codeproject.com/Articles/22770/How-To-LINQ-To-SQL-Transformation
//ref: https://github.com/jagregory/fluent-nhibernate/blob/master/src/FluentNHibernate/Utils/ExpressionToSql.cs
//ref: http://stackoverflow.com/questions/671968/retrieving-property-name-from-lambda-expression

void Main()
{
    var customers = new[]
    {
        new Customer { FirstName = "John", LastName = "Doe", RoomNumber = 23 },
        new Customer { FirstName = "Phillip", LastName = "Giles", RoomNumber = 45 },
        new Customer { FirstName = "Francois", LastName = "Garland", RoomNumber = 9 },
        new Customer { FirstName = "Perswishious", LastName = "CrinkleBerry", RoomNumber = 23 },
    }
    .AsQueryable<Customer>();

    customers.Expression.Dump("IQueryable customers");

    var q = customers
        .Where(i => i.RoomNumber > 2)
        .Where(i => (i.LastName == "CrinkleBerry") || (i.FirstName == "John"))
        .Select(i => new { i.FirstName, i.RoomNumber })
        ;

    var dapperTemplate = @"SELECT /**select**/ FROM Customers /**where**/";
    var visitor = new MyVisitor(dapperTemplate);
    visitor.Visit(q.Expression);
    visitor.ToString().Dump("SQL dump");
}

class Customer
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int RoomNumber { get; set; }
}

class SqlStatement
{
    public SqlStatement(string dapperTemplate)
    {
        this._builder = new Dapper.SqlBuilder();
        this._template = this._builder.AddTemplate(dapperTemplate);
    }

    public Dapper.SqlBuilder Builder
    {
        get { return this._builder; }
    }

    public bool HasHaving { get; set; }

    public bool HasGroupBy { get; set; }

    public bool HasSelect { get; set; }

    public bool HasWhere { get; set; }

    public override string ToString()
    {
        return (string.IsNullOrEmpty(this._template.RawSql))? base.ToString() : this._template.RawSql;
    }

    readonly Dapper.SqlBuilder _builder;
    readonly Dapper.SqlBuilder.Template _template;
}

class MyVisitor : ExpressionVisitor
{
    public MyVisitor(string dapperTemplate)
    {
        this._sqlStatement = new SqlStatement(dapperTemplate);
    }

    public override string ToString()
    {
        return this._sqlStatement.ToString();
    }

    protected override Expression VisitConditional(ConditionalExpression expression)
    {
        expression.Dump("VisitConditional");
        return expression;
    }

    protected override Expression VisitConstant(ConstantExpression expression)
    {
        expression.Dump("VisitConstant");
        return expression;
    }

    protected override Expression VisitMethodCall(MethodCallExpression expression)
    {
        expression.Dump("VisitMethodCall");
        if (expression.Method.DeclaringType != typeof(Queryable))
        {
            "".Dump("The expected queryable is not here.");
            return expression;
        }

        if (this.IsProjectionOrPredicateMethodName(expression))
        {
            this.Visit(expression.Arguments.Last());
        }

        this.Visit(expression.Arguments.First());

        return expression;
    }

    protected override Expression VisitNew(NewExpression expression)
    {
        expression.Dump("VisitNew");
        return expression;
    }

    protected override Expression VisitParameter(ParameterExpression expression)
    {
        expression.Dump("VisitParameter");
        return expression;
    }

    protected override Expression VisitUnary(UnaryExpression expression)
    {
        expression.Dump("VisitUnary");
        if (expression == null)
        {
            "".Dump("The expected Unary Expression is not here");
            return null;
        }

        this.HandleExpression(expression.Operand as LambdaExpression, this._sqlStatement);
        return expression;
    }

    readonly SqlStatement _sqlStatement;
}

static class MyVisitorExtensions
{
    public static void HandleExpression(this MyVisitor visitor, LambdaExpression expression, SqlStatement sqlStatement)
    {
        if (expression == null)
        {
            "".Dump("The expected Lambda Expression is not here");
            return;
        }
        visitor.HandleLambdaExpressionBody(expression.Body, sqlStatement);
    }

    public static void HandleExpression(this MyVisitor visitor, BinaryExpression expression, SqlStatement sqlStatement)
    {
        if (expression == null) return;

        var orElse = " OrElse ";
        var s = expression.ToString();
        s.Dump("BinaryExpression");

        if (!sqlStatement.HasSelect) return;

        if (s.Contains(orElse)) sqlStatement.Builder.Where(s.Replace(orElse, " OR "));
        else sqlStatement.Builder.Where(s);
    }

    public static void HandleExpression(this MyVisitor visitor, MemberExpression expression, SqlStatement sqlStatement)
    {
        if (expression == null) return;
        var info = expression.Member as PropertyInfo;
        info.Dump("info");
    }

    public static void HandleExpression(this MyVisitor visitor, NewExpression expression, SqlStatement sqlStatement)
    {
        if (expression == null) return;
        var info = expression.Members.OfType<MemberInfo>();
        info.Dump("info");

        if(sqlStatement.HasSelect) return;
        sqlStatement.Builder.Select(string.Join(", ", info.Select(i => i.Name).ToArray()));
        sqlStatement.HasSelect = true;
    }

    public static void HandleLambdaExpressionBody(this MyVisitor visitor, Expression expression, SqlStatement sqlStatement)
    {
        if (expression == null) return;

        var s = expression.ToString();
        s.Dump("HandleLambdaExpressionBody()");

        visitor.HandleExpression(expression as BinaryExpression, sqlStatement);
        visitor.HandleExpression(expression as MemberExpression, sqlStatement);
        if(s.StartsWith("new")) visitor.HandleExpression(expression as NewExpression, sqlStatement);
    }

    public static bool IsProjectionOrPredicateMethodName(this MyVisitor visitor, MethodCallExpression expression)
    {
        if (expression == null) return false;
        var methodName = expression.Method.Name;
        return new[] { SqlConstants.Select, SqlConstants.Where }.Contains(methodName.ToLowerInvariant());
    }

    public static bool IsPredicateMethodName(this MyVisitor visitor, MethodCallExpression expression)
    {
        if (expression == null) return false;
        var methodName = expression.Method.Name;
        return new[] { SqlConstants.Where }.Contains(methodName.ToLowerInvariant());
    }

    public static bool IsProjectionMethodName(this MyVisitor visitor, MethodCallExpression expression)
    {
        if (expression == null) return false;
        var methodName = expression.Method.Name;
        return new[] { SqlConstants.Select }.Contains(methodName.ToLowerInvariant());
    }
}

static class SqlConstants
{
    public static string From = "from";

    public static string Select = "select";

    public static string Where = "where";
}