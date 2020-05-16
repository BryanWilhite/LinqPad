<Query Kind="Statements" />

/*
    📖 https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/arrays/multidimensional-arrays

    BTW: there *are* matrices and vectors in .NET
    📖 https://docs.microsoft.com/en-us/dotnet/api/system.numerics?view=netstandard-2.1
*/

int[,] matrix = new int[,]
{
    { 1, 42, 0, 1 },
    { 0, 0, 43, 1 },
    { 44, 1, 0, 1 },
};

matrix[0, 1].Dump("row 0, col 1");
matrix[1, 2].Dump("row 1, col 2");

matrix.GetLength(0).Dump("rows");

// print out matrix:
var columnCount = matrix.GetLength(1).Dump("columns");
var columnMultiple = columnCount - 1;
var columnIndex = 0;

var sb = new StringBuilder();
foreach (var element in matrix)
{
    var isAtEndOfRow = (columnIndex > 0) && (columnIndex % columnMultiple == 0);

    if (isAtEndOfRow)
    {
        sb.AppendLine($"{element:00}");
        columnIndex = 0;
    }
    else
    {
        sb.Append($"{element:00}, ");
        columnIndex++;
    }
}

sb.ToString().Dump("report");

sb = new StringBuilder();

// print out vector
var vectorIndex = 2;
foreach (var rowIndex in Enumerable.Range(0, matrix.GetLength(0)))
{
    sb.AppendLine($"{matrix[rowIndex, vectorIndex]:00}");
}

sb.ToString().Dump($"report: vector [,{vectorIndex}]");
