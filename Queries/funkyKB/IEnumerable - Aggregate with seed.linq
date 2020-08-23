<Query Kind="Statements" />

var strings = new[] { "one", "two", "three" };

strings.Aggregate(string.Empty,
    (a, i) => string.Concat(a, (a.Length == 0) ? a : ", ", $"\"{i}\"")
).Dump();