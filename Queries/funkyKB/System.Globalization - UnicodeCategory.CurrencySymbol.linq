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

// [ https://docs.microsoft.com/en-us/dotnet/api/system.globalization.unicodecategory?view=netcore-3.1 ]