<Query Kind="Statements">
  <Namespace>System.Globalization</Namespace>
</Query>

var unicodeCategory = UnicodeCategory.CurrencySymbol;
var unicodePoints = Enumerable.Range(0, ushort.MaxValue);
unicodePoints.Count().Dump("total number of Unicode Points?");

unicodePoints
    .Select(i => Convert.ToChar(i))
    .Where(i => CharUnicodeInfo.GetUnicodeCategory(i) == unicodeCategory)
    .Dump("currency symbols");

// [ 📖 https://docs.microsoft.com/en-us/dotnet/api/system.globalization.unicodecategory?view=netcore-3.1 ]

/*
    What is apparently not .NET Framework (only in .NET Core)
    is the `System.Text.Unicode.UnicodeRange` class
    which would eliminate the need to scan up to `ushort.MaxValue`.

    [ 📖 https://docs.microsoft.com/en-us/dotnet/api/system.text.unicode.unicoderange?view=netcore-3.1]
*/
