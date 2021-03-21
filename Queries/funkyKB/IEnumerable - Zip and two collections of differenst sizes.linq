<Query Kind="Statements" />

var a = new[] { 1, 2, 3 };
var b = new[] { "one", "two", "three", "four", "five" };

a.Zip(b, (first, second) => new { first, second }).Dump();

// Zip ignores non-corresponding elements.
