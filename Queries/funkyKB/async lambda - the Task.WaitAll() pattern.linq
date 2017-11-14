<Query Kind="Statements">
  <NuGetReference>MoreLinq</NuGetReference>
  <Namespace>MoreLinq</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

var stopwatch = new Stopwatch();

var number = 0;
var data = Enumerable.Range(0, 1000);

stopwatch.Start();
var tasks = data.Select(async i =>
{ //try data.AsParallel().Select... and compare performance
    await Task.Delay(TimeSpan.FromMilliseconds(50));
    Interlocked.Increment(ref number);
});

Task.WaitAll(tasks.ToArray());

stopwatch.Stop();
stopwatch.Elapsed.Milliseconds.Dump("elapsed milliseconds");

number.Dump("number should be incremented");

/*
    Note that ++ or -- is not thread safe [see https://stackoverflow.com/a/443530/22944].
    Use instead Interlocked.Increment or Interlocked.Decrement, respectively.
*/