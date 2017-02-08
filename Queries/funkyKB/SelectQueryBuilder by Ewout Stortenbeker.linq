<Query Kind="Program">
  <Namespace>System.Data.Common</Namespace>
</Query>

// ref: http://www.codeproject.com/Articles/13419/SelectQueryBuilder-Building-complex-and-flexible-S
void Main()
{
    var statement = new SelectQueryBuilder { TopRecords = 10 };
    statement.SelectFromTable("Customers");
    statement.SelectAllColumns();
    statement.TopRecords = 10;

    var test1 = false;
    var test2 = true;
    var test3 = true;
    
    if (test1) statement.AddWhere("CompanyName", Comparison.Like, "International%");
    if (test2) statement.AddWhere("City", Comparison.Like, "Los%");
    if (test3) statement.AddWhere("Country", Comparison.Equals, "United States of America");
    statement.AddWhere("InceptDate", Comparison.LessOrEquals, new SqlLiteral("getDate()"));
    statement.BuildQuery().Dump("Comparison and SqlLiteral");

    statement = new SelectQueryBuilder();
    statement.SelectColumns("count(*) AS Count", "ShipCity");
    statement.SelectFromTable("Orders");
    statement.GroupBy("ShipCity");
    statement.AddHaving("ShipCity", Comparison.NotEquals, "Amsterdam");
    statement.AddOrderBy("count(*)", Sorting.Descending);
    statement.BuildQuery().Dump("SelectColumns() and GroupBy()");

    statement = new SelectQueryBuilder();
    statement.SelectFromTable("Orders");
    statement.AddJoin(JoinType.InnerJoin,
                  "Customers", "CustomerID",
                  Comparison.Equals,
                  "Orders", "CustomerID");
    statement.AddWhere("Customers.City",
          Comparison.Equals, "London");
    statement.BuildQuery().Dump("AddJoin()");

    statement = new SelectQueryBuilder();
    statement.SelectFromTable("Orders");

    //level 1:
    statement.AddWhere("CustomerID", Comparison.Equals, "VINET", 1);
    statement.AddWhere("OrderDate", Comparison.LessThan, new DateTime(2005, 1, 1), 1);

    //level 2:
    statement.AddWhere("CustomerID", Comparison.Equals, "TOMSP", 2);
    WhereClause clause = statement.AddWhere("OrderDate", Comparison.LessThan, new DateTime(2004, 6, 30), 2);
    clause.AddClause(LogicOperator.Or, Comparison.GreaterThan, new DateTime(2006, 1, 1));

    statement.BuildQuery().Dump("nested WHERE clauses");
}

//
// Class: SelectQueryBuilder
// Copyright 2006 by Ewout Stortenbeker
// Email: 4ewout@gmail.com
//
// This class is part of the CodeEngine Framework. This framework also contains
// the UpdateQueryBuilder, InsertQueryBuilder and DeleteQueryBuilder.
// You can download the framework DLL at http://www.code-engine.com/
//
public class SelectQueryBuilder : IQueryBuilder
{
    protected bool _distinct = false;
    protected TopClause _topClause = new TopClause(100, TopUnit.Percent);
    protected List<string> _selectedColumns = new List<string>();   // array of string
    protected List<string> _selectedTables = new List<string>();    // array of string
    protected List<JoinClause> _joins = new List<JoinClause>(); // array of JoinClause
    protected WhereStatement _whereStatement = new WhereStatement();
    protected List<OrderByClause> _orderByStatement = new List<OrderByClause>();    // array of OrderByClause
    protected List<string> _groupByColumns = new List<string>();        // array of string
    protected WhereStatement _havingStatement = new WhereStatement();

    internal WhereStatement WhereStatement
    {
        get { return _whereStatement; }
        set { _whereStatement = value; }
    }

    public SelectQueryBuilder() { }
    public SelectQueryBuilder(DbProviderFactory factory)
    {
        _dbProviderFactory = factory;
    }

    private DbProviderFactory _dbProviderFactory;
    public void SetDbProviderFactory(DbProviderFactory factory)
    {
        _dbProviderFactory = factory;
    }

    public bool Distinct
    {
        get { return _distinct; }
        set { _distinct = value; }
    }

    public int TopRecords
    {
        get { return _topClause.Quantity; }
        set
        {
            _topClause.Quantity = value;
            _topClause.Unit = TopUnit.Records;
        }
    }
    public TopClause TopClause
    {
        get { return _topClause; }
        set { _topClause = value; }
    }

    public string[] SelectedColumns
    {
        get
        {
            if (_selectedColumns.Count > 0)
                return _selectedColumns.ToArray();
            else
                return new string[1] { "*" };
        }
    }
    public string[] SelectedTables
    {
        get { return _selectedTables.ToArray(); }
    }

    public void SelectAllColumns()
    {
        _selectedColumns.Clear();
    }
    public void SelectCount()
    {
        SelectColumn("count(1)");
    }
    public void SelectColumn(string column)
    {
        _selectedColumns.Clear();
        _selectedColumns.Add(column);
    }
    public void SelectColumns(params string[] columns)
    {
        _selectedColumns.Clear();
        foreach (string column in columns)
        {
            _selectedColumns.Add(column);
        }
    }
    public void SelectFromTable(string table)
    {
        _selectedTables.Clear();
        _selectedTables.Add(table);
    }
    public void SelectFromTables(params string[] tables)
    {
        _selectedTables.Clear();
        foreach (string Table in tables)
        {
            _selectedTables.Add(Table);
        }
    }
    public void AddJoin(JoinClause newJoin)
    {
        _joins.Add(newJoin);
    }
    public void AddJoin(JoinType join, string toTableName, string toColumnName, Comparison @operator, string fromTableName, string fromColumnName)
    {
        JoinClause NewJoin = new JoinClause(join, toTableName, toColumnName, @operator, fromTableName, fromColumnName);
        _joins.Add(NewJoin);
    }

    public WhereStatement Where
    {
        get { return _whereStatement; }
        set { _whereStatement = value; }
    }

    public void AddWhere(WhereClause clause) { AddWhere(clause, 1); }
    public void AddWhere(WhereClause clause, int level)
    {
        _whereStatement.Add(clause, level);
    }
    public WhereClause AddWhere(string field, Comparison @operator, object compareValue) { return AddWhere(field, @operator, compareValue, 1); }
    public WhereClause AddWhere(Enum field, Comparison @operator, object compareValue) { return AddWhere(field.ToString(), @operator, compareValue, 1); }
    public WhereClause AddWhere(string field, Comparison @operator, object compareValue, int level)
    {
        WhereClause NewWhereClause = new WhereClause(field, @operator, compareValue);
        _whereStatement.Add(NewWhereClause, level);
        return NewWhereClause;
    }

    public void AddOrderBy(OrderByClause clause)
    {
        _orderByStatement.Add(clause);
    }
    public void AddOrderBy(Enum field, Sorting order) { this.AddOrderBy(field.ToString(), order); }
    public void AddOrderBy(string field, Sorting order)
    {
        OrderByClause NewOrderByClause = new OrderByClause(field, order);
        _orderByStatement.Add(NewOrderByClause);
    }

    public void GroupBy(params string[] columns)
    {
        foreach (string Column in columns)
        {
            _groupByColumns.Add(Column);
        }
    }

    public WhereStatement Having
    {
        get { return _havingStatement; }
        set { _havingStatement = value; }
    }

    public void AddHaving(WhereClause clause) { AddHaving(clause, 1); }
    public void AddHaving(WhereClause clause, int level)
    {
        _havingStatement.Add(clause, level);
    }
    public WhereClause AddHaving(string field, Comparison @operator, object compareValue) { return AddHaving(field, @operator, compareValue, 1); }
    public WhereClause AddHaving(Enum field, Comparison @operator, object compareValue) { return AddHaving(field.ToString(), @operator, compareValue, 1); }
    public WhereClause AddHaving(string field, Comparison @operator, object compareValue, int level)
    {
        WhereClause NewWhereClause = new WhereClause(field, @operator, compareValue);
        _havingStatement.Add(NewWhereClause, level);
        return NewWhereClause;
    }

    public DbCommand BuildCommand()
    {
        return (DbCommand)this.BuildQuery(true);
    }

    public string BuildQuery()
    {
        return (string)this.BuildQuery(false);
    }

    /// <summary>
    /// Builds the select query
    /// </summary>
    /// <returns>Returns a string containing the query, or a DbCommand containing a command with parameters</returns>
    private object BuildQuery(bool buildCommand)
    {
        if (buildCommand && _dbProviderFactory == null)
            throw new Exception("Cannot build a command when the Db Factory hasn't been specified. Call SetDbProviderFactory first.");

        DbCommand command = null;
        if (buildCommand)
            command = _dbProviderFactory.CreateCommand();

        string Query = "SELECT ";

        // Output Distinct
        if (_distinct)
        {
            Query += "DISTINCT ";
        }

        // Output Top clause
        if (!(_topClause.Quantity == 100 & _topClause.Unit == TopUnit.Percent))
        {
            Query += "TOP " + _topClause.Quantity;
            if (_topClause.Unit == TopUnit.Percent)
            {
                Query += " PERCENT";
            }
            Query += " ";
        }

        // Output column names
        if (_selectedColumns.Count == 0)
        {
            if (_selectedTables.Count == 1)
                Query += _selectedTables[0] + "."; // By default only select * from the table that was selected. If there are any joins, it is the responsibility of the user to select the needed columns.

            Query += "*";
        }
        else
        {
            foreach (string ColumnName in _selectedColumns)
            {
                Query += ColumnName + ',';
            }
            Query = Query.TrimEnd(','); // Trim de last comma inserted by foreach loop
            Query += ' ';
        }
        // Output table names
        if (_selectedTables.Count > 0)
        {
            Query += " FROM ";
            foreach (string TableName in _selectedTables)
            {
                Query += TableName + ',';
            }
            Query = Query.TrimEnd(','); // Trim de last comma inserted by foreach loop
            Query += ' ';
        }

        // Output joins
        if (_joins.Count > 0)
        {
            foreach (JoinClause Clause in _joins)
            {
                string JoinString = "";
                switch (Clause.JoinType)
                {
                    case JoinType.InnerJoin: JoinString = "INNER JOIN"; break;
                    case JoinType.OuterJoin: JoinString = "OUTER JOIN"; break;
                    case JoinType.LeftJoin: JoinString = "LEFT JOIN"; break;
                    case JoinType.RightJoin: JoinString = "RIGHT JOIN"; break;
                }
                JoinString += " " + Clause.ToTable + " ON ";
                JoinString += WhereStatement.CreateComparisonClause(Clause.FromTable + '.' + Clause.FromColumn, Clause.ComparisonOperator, new SqlLiteral(Clause.ToTable + '.' + Clause.ToColumn));
                Query += JoinString + ' ';
            }
        }

        // Output where statement
        if (_whereStatement.ClauseLevels > 0)
        {
            if (buildCommand)
                Query += " WHERE " + _whereStatement.BuildWhereStatement(true, ref command);
            else
                Query += " WHERE " + _whereStatement.BuildWhereStatement();
        }

        // Output GroupBy statement
        if (_groupByColumns.Count > 0)
        {
            Query += " GROUP BY ";
            foreach (string Column in _groupByColumns)
            {
                Query += Column + ',';
            }
            Query = Query.TrimEnd(',');
            Query += ' ';
        }

        // Output having statement
        if (_havingStatement.ClauseLevels > 0)
        {
            // Check if a Group By Clause was set
            if (_groupByColumns.Count == 0)
            {
                throw new Exception("Having statement was set without Group By");
            }
            if (buildCommand)
                Query += " HAVING " + _havingStatement.BuildWhereStatement(true, ref command);
            else
                Query += " HAVING " + _havingStatement.BuildWhereStatement();
        }

        // Output OrderBy statement
        if (_orderByStatement.Count > 0)
        {
            Query += " ORDER BY ";
            foreach (OrderByClause Clause in _orderByStatement)
            {
                string OrderByClause = "";
                switch (Clause.SortOrder)
                {
                    case Sorting.Ascending:
                        OrderByClause = Clause.FieldName + " ASC"; break;
                    case Sorting.Descending:
                        OrderByClause = Clause.FieldName + " DESC"; break;
                }
                Query += OrderByClause + ',';
            }
            Query = Query.TrimEnd(','); // Trim de last AND inserted by foreach loop
            Query += ' ';
        }

        if (buildCommand)
        {
            // Return the build command
            command.CommandText = Query;
            return command;
        }
        else
        {
            // Return the built query
            return Query;
        }
    }
}

//
// Interface: IQueryBuilder
// Copyright 2006 by Ewout Stortenbeker
// Email: 4ewout@gmail.com
//
// This interface is part of the CodeEngine Framework.
// You can download the framework DLL at http://www.code-engine.com/
// 
public interface IQueryBuilder
{
    string BuildQuery();
}

//
// Class: SqlLiteral
// Copyright 2006 by Ewout Stortenbeker
// Email: 4ewout@gmail.com
//
// This class is part of the CodeEngine Framework.
// You can download the framework DLL at http://www.code-engine.com/
// 
public class SqlLiteral
{
    public const string StatementRowsAffected = "SELECT @@ROWCOUNT";

    private string _value;
    public string Value
    {
        get { return _value; }
        set { _value = value; }
    }

    public SqlLiteral(string value)
    {
        _value = value;
    }
}

/// <summary>
/// Represents comparison operators for WHERE, HAVING and JOIN clauses
/// </summary>
public enum Comparison
{
    Equals,
    NotEquals,
    Like,
    NotLike,
    GreaterThan,
    GreaterOrEquals,
    LessThan,
    LessOrEquals,
    In
}

/// <summary>
/// Represents operators for JOIN clauses
/// </summary>
public enum JoinType
{
    InnerJoin,
    OuterJoin,
    LeftJoin,
    RightJoin
}

/// <summary>
/// Represents logic operators for chaining WHERE and HAVING clauses together in a statement
/// </summary>
public enum LogicOperator
{
    And,
    Or
}

/// <summary>
/// Represents sorting operators for SELECT statements
/// </summary>
public enum Sorting
{
    Ascending,
    Descending
}

/// <summary>
/// Represents a unit for TOP clauses in SELECT statements
/// </summary>
public enum TopUnit
{
    Records,
    Percent
}

/// <summary>
/// Represents a JOIN clause to be used with SELECT statements
/// </summary>
public struct JoinClause
{
    public JoinType JoinType;
    public string FromTable;
    public string FromColumn;
    public Comparison ComparisonOperator;
    public string ToTable;
    public string ToColumn;
    public JoinClause(JoinType join, string toTableName, string toColumnName, Comparison @operator, string fromTableName, string fromColumnName)
    {
        JoinType = join;
        FromTable = fromTableName;
        FromColumn = fromColumnName;
        ComparisonOperator = @operator;
        ToTable = toTableName;
        ToColumn = toColumnName;
    }
}

/// <summary>
/// Represents a ORDER BY clause to be used with SELECT statements
/// </summary>
public struct OrderByClause
{
    public string FieldName;
    public Sorting SortOrder;
    public OrderByClause(string field)
    {
        FieldName = field;
        SortOrder = Sorting.Ascending;
    }
    public OrderByClause(string field, Sorting order)
    {
        FieldName = field;
        SortOrder = order;
    }
}

/// <summary>
/// Represents a TOP clause for SELECT statements
/// </summary>
public struct TopClause
{
    public int Quantity;
    public TopUnit Unit;
    public TopClause(int nr)
    {
        Quantity = nr;
        Unit = TopUnit.Records;
    }
    public TopClause(int nr, TopUnit aUnit)
    {
        Quantity = nr;
        Unit = aUnit;
    }
}

/// <summary>
/// Represents a WHERE clause on 1 database column, containing 1 or more comparisons on 
/// that column, chained together by logic operators: eg (UserID=1 or UserID=2 or UserID>100)
/// This can be achieved by doing this:
/// WhereClause myWhereClause = new WhereClause("UserID", Comparison.Equals, 1);
/// myWhereClause.AddClause(LogicOperator.Or, Comparison.Equals, 2);
/// myWhereClause.AddClause(LogicOperator.Or, Comparison.GreaterThan, 100);
/// </summary>
public struct WhereClause
{
    private string m_FieldName;
    private Comparison m_ComparisonOperator;
    private object m_Value;
    internal struct SubClause
    {
        public LogicOperator LogicOperator;
        public Comparison ComparisonOperator;
        public object Value;
        public SubClause(LogicOperator logic, Comparison compareOperator, object compareValue)
        {
            LogicOperator = logic;
            ComparisonOperator = compareOperator;
            Value = compareValue;
        }
    }
    internal List<SubClause> SubClauses;    // Array of SubClause

    /// <summary>
    /// Gets/sets the name of the database column this WHERE clause should operate on
    /// </summary>
    public string FieldName
    {
        get { return m_FieldName; }
        set { m_FieldName = value; }
    }

    /// <summary>
    /// Gets/sets the comparison method
    /// </summary>
    public Comparison ComparisonOperator
    {
        get { return m_ComparisonOperator; }
        set { m_ComparisonOperator = value; }
    }

    /// <summary>
    /// Gets/sets the value that was set for comparison
    /// </summary>
    public object Value
    {
        get { return m_Value; }
        set { m_Value = value; }
    }

    public WhereClause(string field, Comparison firstCompareOperator, object firstCompareValue)
    {
        m_FieldName = field;
        m_ComparisonOperator = firstCompareOperator;
        m_Value = firstCompareValue;
        SubClauses = new List<SubClause>();
    }
    public void AddClause(LogicOperator logic, Comparison compareOperator, object compareValue)
    {
        SubClause NewSubClause = new SubClause(logic, compareOperator, compareValue);
        SubClauses.Add(NewSubClause);
    }
}

public class WhereStatement : List<List<WhereClause>>
{
    // The list in this container will contain lists of clauses, and 
    // forms a where statement alltogether!

    public int ClauseLevels
    {
        get { return this.Count; }
    }

    private void AssertLevelExistance(int level)
    {
        if (this.Count < (level - 1))
        {
            throw new Exception("Level " + level + " not allowed because level " + (level - 1) + " does not exist.");
        }
        // Check if new level must be created
        else if (this.Count < level)
        {
            this.Add(new List<WhereClause>());
        }
    }

    public void Add(WhereClause clause) { this.Add(clause, 1); }
    public void Add(WhereClause clause, int level)
    {
        this.AddWhereClauseToLevel(clause, level);
    }
    public WhereClause Add(string field, Comparison @operator, object compareValue) { return this.Add(field, @operator, compareValue, 1); }
    public WhereClause Add(Enum field, Comparison @operator, object compareValue) { return this.Add(field.ToString(), @operator, compareValue, 1); }
    public WhereClause Add(string field, Comparison @operator, object compareValue, int level)
    {
        WhereClause NewWhereClause = new WhereClause(field, @operator, compareValue);
        this.AddWhereClauseToLevel(NewWhereClause, level);
        return NewWhereClause;
    }

    private void AddWhereClause(WhereClause clause)
    {
        AddWhereClauseToLevel(clause, 1);
    }

    private void AddWhereClauseToLevel(WhereClause clause, int level)
    {
        // Add the new clause to the array at the right level
        AssertLevelExistance(level);
        this[level - 1].Add(clause);
    }

    public string BuildWhereStatement()
    {
        DbCommand dummyCommand = null; // = DataAccess.UsedDbProviderFactory.CreateCommand();
        return BuildWhereStatement(false, ref dummyCommand);
    }

    public string BuildWhereStatement(bool useCommandObject, ref DbCommand usedDbCommand)
    {
        string Result = "";
        foreach (List<WhereClause> WhereStatement in this) // Loop through all statement levels, OR them together
        {
            string LevelWhere = "";
            foreach (WhereClause Clause in WhereStatement) // Loop through all conditions, AND them together
            {
                string WhereClause = "";

                if (useCommandObject)
                {
                    // Create a parameter
                    string parameterName = string.Format(
                        "@p{0}_{1}",
                        usedDbCommand.Parameters.Count + 1,
                        Clause.FieldName.Replace('.', '_')
                        );

                    DbParameter parameter = usedDbCommand.CreateParameter();
                    parameter.ParameterName = parameterName;
                    parameter.Value = Clause.Value;
                    usedDbCommand.Parameters.Add(parameter);

                    // Create a where clause using the parameter, instead of its value
                    WhereClause += CreateComparisonClause(Clause.FieldName, Clause.ComparisonOperator, new SqlLiteral(parameterName));
                }
                else
                {
                    WhereClause = CreateComparisonClause(Clause.FieldName, Clause.ComparisonOperator, Clause.Value);
                }

                foreach (WhereClause.SubClause SubWhereClause in Clause.SubClauses) // Loop through all subclauses, append them together with the specified logic operator
                {
                    switch (SubWhereClause.LogicOperator)
                    {
                        case LogicOperator.And:
                            WhereClause += " AND "; break;
                        case LogicOperator.Or:
                            WhereClause += " OR "; break;
                    }

                    if (useCommandObject)
                    {
                        // Create a parameter
                        string parameterName = string.Format(
                            "@p{0}_{1}",
                            usedDbCommand.Parameters.Count + 1,
                            Clause.FieldName.Replace('.', '_')
                            );

                        DbParameter parameter = usedDbCommand.CreateParameter();
                        parameter.ParameterName = parameterName;
                        parameter.Value = SubWhereClause.Value;
                        usedDbCommand.Parameters.Add(parameter);

                        // Create a where clause using the parameter, instead of its value
                        WhereClause += CreateComparisonClause(Clause.FieldName, SubWhereClause.ComparisonOperator, new SqlLiteral(parameterName));
                    }
                    else
                    {
                        WhereClause += CreateComparisonClause(Clause.FieldName, SubWhereClause.ComparisonOperator, SubWhereClause.Value);
                    }
                }
                LevelWhere += "(" + WhereClause + ") AND ";
            }
            LevelWhere = LevelWhere.Substring(0, LevelWhere.Length - 5); // Trim de last AND inserted by foreach loop
            if (WhereStatement.Count > 1)
            {
                Result += " (" + LevelWhere + ") ";
            }
            else
            {
                Result += " " + LevelWhere + " ";
            }
            Result += " OR";
        }
        Result = Result.Substring(0, Result.Length - 2); // Trim de last OR inserted by foreach loop

        return Result;
    }

    internal static string CreateComparisonClause(string fieldName, Comparison comparisonOperator, object value)
    {
        string Output = "";
        if (value != null && value != System.DBNull.Value)
        {
            switch (comparisonOperator)
            {
                case Comparison.Equals:
                    Output = fieldName + " = " + FormatSQLValue(value); break;
                case Comparison.NotEquals:
                    Output = fieldName + " <> " + FormatSQLValue(value); break;
                case Comparison.GreaterThan:
                    Output = fieldName + " > " + FormatSQLValue(value); break;
                case Comparison.GreaterOrEquals:
                    Output = fieldName + " >= " + FormatSQLValue(value); break;
                case Comparison.LessThan:
                    Output = fieldName + " < " + FormatSQLValue(value); break;
                case Comparison.LessOrEquals:
                    Output = fieldName + " <= " + FormatSQLValue(value); break;
                case Comparison.Like:
                    Output = fieldName + " LIKE " + FormatSQLValue(value); break;
                case Comparison.NotLike:
                    Output = "NOT " + fieldName + " LIKE " + FormatSQLValue(value); break;
                case Comparison.In:
                    Output = fieldName + " IN (" + FormatSQLValue(value) + ")"; break;
            }
        }
        else // value==null    || value==DBNull.Value
        {
            if ((comparisonOperator != Comparison.Equals) && (comparisonOperator != Comparison.NotEquals))
            {
                throw new Exception("Cannot use comparison operator " + comparisonOperator.ToString() + " for NULL values.");
            }
            else
            {
                switch (comparisonOperator)
                {
                    case Comparison.Equals:
                        Output = fieldName + " IS NULL"; break;
                    case Comparison.NotEquals:
                        Output = "NOT " + fieldName + " IS NULL"; break;
                }
            }
        }
        return Output;
    }

    internal static string FormatSQLValue(object someValue)
    {
        string FormattedValue = "";
        //                string StringType = Type.GetType("string").Name;
        //                string DateTimeType = Type.GetType("DateTime").Name;

        if (someValue == null)
        {
            FormattedValue = "NULL";
        }
        else
        {
            switch (someValue.GetType().Name)
            {
                case "String": FormattedValue = "'" + ((string)someValue).Replace("'", "''") + "'"; break;
                case "DateTime": FormattedValue = "'" + ((DateTime)someValue).ToString("yyyy/MM/dd hh:mm:ss") + "'"; break;
                case "DBNull": FormattedValue = "NULL"; break;
                case "Boolean": FormattedValue = (bool)someValue ? "1" : "0"; break;
                case "SqlLiteral": FormattedValue = ((SqlLiteral)someValue).Value; break;
                default: FormattedValue = someValue.ToString(); break;
            }
        }
        return FormattedValue;
    }

    /// <summary>
    /// This static method combines 2 where statements with eachother to form a new statement
    /// </summary>
    /// <param name="statement1"></param>
    /// <param name="statement2"></param>
    /// <returns></returns>
    public static WhereStatement CombineStatements(WhereStatement statement1, WhereStatement statement2)
    {
        // statement1: {Level1}((Age<15 OR Age>=20) AND (strEmail LIKE 'e%') OR {Level2}(Age BETWEEN 15 AND 20))
        // Statement2: {Level1}((Name = 'Peter'))
        // Return statement: {Level1}((Age<15 or Age>=20) AND (strEmail like 'e%') AND (Name = 'Peter'))

        // Make a copy of statement1
        WhereStatement result = WhereStatement.Copy(statement1);

        // Add all clauses of statement2 to result
        for (int i = 0; i < statement2.ClauseLevels; i++) // for each clause level in statement2
        {
            List<WhereClause> level = statement2[i];
            foreach (WhereClause clause in level) // for each clause in level i
            {
                for (int j = 0; j < result.ClauseLevels; j++)  // for each level in result, add the clause
                {
                    result.AddWhereClauseToLevel(clause, j);
                }
            }
        }

        return result;
    }

    public static WhereStatement Copy(WhereStatement statement)
    {
        WhereStatement result = new WhereStatement();
        int currentLevel = 0;
        foreach (List<WhereClause> level in statement)
        {
            currentLevel++;
            result.Add(new List<WhereClause>());
            foreach (WhereClause clause in statement[currentLevel - 1])
            {
                WhereClause clauseCopy = new WhereClause(clause.FieldName, clause.ComparisonOperator, clause.Value);
                foreach (WhereClause.SubClause subClause in clause.SubClauses)
                {
                    WhereClause.SubClause subClauseCopy = new WhereClause.SubClause(subClause.LogicOperator, subClause.ComparisonOperator, subClause.Value);
                    clauseCopy.SubClauses.Add(subClauseCopy);
                }
                result[currentLevel - 1].Add(clauseCopy);
            }
        }
        return result;
    }

}