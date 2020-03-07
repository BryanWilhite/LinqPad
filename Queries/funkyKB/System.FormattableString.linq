<Query Kind="Statements">
  <Namespace>static System.FormattableString</Namespace>
</Query>

// using static System.FormattableString;

var id = "my identifier";
var formatted = Invariant($"ID: {id}");
formatted.Dump();

// 📖 [ https://stackoverflow.com/questions/35425899/difference-between-string-formattablestring-iformattable ]