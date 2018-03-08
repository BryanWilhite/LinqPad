<Query Kind="Statements" />

var set = new[] { 9, 9, 9 };

var number = set
    .Reverse()
    .Select((x, i) => x * Math.Pow(10, i))
    .Reverse()
    .Aggregate((a, i) => a + i) + 1;

number.Dump("increment");

var newSet = number
    .ToString()
    .ToCharArray()
    .Select(i => int.Parse(i.ToString()));

newSet.Dump("new set");

//see https://www.youtube.com/watch?v=uQdy914JRKQ