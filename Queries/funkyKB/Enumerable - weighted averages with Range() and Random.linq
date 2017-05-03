<Query Kind="Statements" />

/*
    “can I get the correct average of a set of numbers from the averages of several subsets?”
    [https://math.stackexchange.com/questions/864657/can-i-get-the-correct-average-of-a-set-of-numbers-from-the-averages-of-several-s]
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

(new[]
{
    setOne.Average() * setOne.Count(),
    setTwo.Average() * setTwo.Count(),
    setThree.Average() * setThree.Count()
}
.Sum() / set.Count())
.Dump("weighted average (from unequal subsets)");