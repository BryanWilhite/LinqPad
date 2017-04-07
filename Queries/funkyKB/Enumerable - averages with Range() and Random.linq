<Query Kind="Statements" />

var generator = new Random(DateTime.Now.Millisecond);
var set = Enumerable.Range(1, 12)
    .Select(i => generator.Next(1, 100))
    .ToArray();

set.Dump("set of random numbers");

set.Average().Dump("set average");

var setOne = set.Take(4);
var setTwo = set.Skip(4).Take(4);
var setThree = set.Skip(8).Take(4);

setOne.Dump("set one");
setTwo.Dump("set two");
setThree.Dump("set three");

new[]
{
    setOne.Average(),
    setTwo.Average(),
    setThree.Average()
}
.Average()
.Dump("average of averages");