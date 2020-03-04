<Query Kind="Statements" />

// https://docs.microsoft.com/en-us/dotnet/csharp/tuples

var tuple = new Tuple<string, int>("one", 1);
tuple.Dump();

(string key, int value) tuple7x = (key: "one", value: 1);
tuple7x.Dump();
tuple7x.value.Dump(tuple7x.key);