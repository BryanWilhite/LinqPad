<Query Kind="Statements" />

/*
    Hans-Christian Holm - Case study: Making use of functional programming techniques in .NET
    [https://vimeo.com/97541187]

    http://www.yr.no/
*/

Func<int,int,int> f = (x,y) => x + y;
Func<int,int,int> g = (x,y) => x * y;

var numbers = new[] {2, 3, 10};

numbers.Aggregate(f).Dump();
numbers.Aggregate(g).Dump();

Func<Func<int,int,int>,int> numbersAgg = numbers.Aggregate;

var operations = new[] {f, g};

operations.Select(numbersAgg).ToArray().Dump();