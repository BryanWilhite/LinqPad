<Query Kind="Statements" />

int[,] matrix = new int[,]
{
    { 1, 42, 0, 1 },
    { 0, 0, 43, 1 },
    { 44, 1, 0, 1 },
};

matrix[0, 1].Dump("row 0, col 1");
matrix[1, 2].Dump("row 1, col 2");

matrix.GetLength(0).Dump("rows");
var columnCount = matrix.GetLength(1).Dump("columns");
var columnMultiple = columnCount - 1;
var columnIndex = 0;

var sb = new StringBuilder();
foreach (var element in matrix)
{
    var isAtEndOfRow = (columnIndex > 0) && (columnIndex % columnMultiple == 0);

    if (isAtEndOfRow)
    {
        sb.AppendLine($"{element}");
        columnIndex = 0;
    }
    else
    {
        sb.Append($"{element}, ");
        columnIndex++;
    }
}

sb.ToString().Dump("report");

// 📖 https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/arrays/multidimensional-arrays
