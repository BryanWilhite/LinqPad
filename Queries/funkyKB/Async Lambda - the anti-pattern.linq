<Query Kind="Statements">
  <NuGetReference>MoreLinq</NuGetReference>
  <Namespace>MoreLinq</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

var number = 0;
var data = Enumerable.Range(0, 1000);

data.ForEach(async i =>
{
    await Task.Delay(TimeSpan.FromMilliseconds(50));
    Interlocked.Increment(ref number);
});

number.Dump("number should not be incremented");
