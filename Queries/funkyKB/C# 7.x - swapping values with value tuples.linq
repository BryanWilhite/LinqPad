<Query Kind="Statements" />

var x = 10;
var y = 100;

var tmp = y;
y = x;
x = tmp;

$"x: {x}, y: {y}".Dump("old way");

void swapBack() => (x, y) = (y, x);

swapBack();

$"x: {x}, y: {y}".Dump("new way");

// 📖 https://docs.microsoft.com/en-us/dotnet/csharp/tuples#assignment-and-tuples