<Query Kind="Statements" />

/*
    “Why is an average of an average usually incorrect?”
    [https://math.stackexchange.com/questions/95909/why-is-an-average-of-an-average-usually-incorrect]
*/

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
.Dump("average of averages (from equal subsets)");

setOne = set.Take(5);
setTwo = set.Skip(5).Take(3);
setThree = set.Skip(8).Take(4);

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
.Dump("average of averages (from unequal subsets)");

