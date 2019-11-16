<Query Kind="Statements">
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

var compositeTasks = (new[] { 1, 2, 3, 4 })
    .Select(i => Task.WhenAll(
            Task.FromResult(i),
            Task.FromResult(i * 2),
            Task.FromResult(i * 4)
        ))
    .ToArray();

Task.WaitAll(compositeTasks);

compositeTasks.SelectMany(i => i.Result).ToArray().Dump("results");

var compositeTasksWithError = (new[] { 1, 2, 3, 4 })
    .Select(i => Task.WhenAll(
            Task.FromResult(i),
            Task.FromException(new Exception($"oops! {nameof(i)}: {i}")),
            Task.FromResult(i * 4)
        ))
    .ToArray();

try
{
    Task.WaitAll(compositeTasksWithError);
}
catch (AggregateException aex)
{
  aex.Dump(nameof(AggregateException));
}
