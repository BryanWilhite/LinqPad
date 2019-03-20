<Query Kind="Statements">
  <Reference>&lt;RuntimeDirectory&gt;\System.Runtime.dll</Reference>
  <NuGetReference>System.Reactive</NuGetReference>
  <Namespace>System</Namespace>
  <Namespace>System.Reactive</Namespace>
  <Namespace>System.Reactive.Concurrency</Namespace>
  <Namespace>System.Reactive.Disposables</Namespace>
  <Namespace>System.Reactive.Joins</Namespace>
  <Namespace>System.Reactive.Linq</Namespace>
  <Namespace>System.Reactive.Subjects</Namespace>
  <Namespace>System.Reactive.Threading.Tasks</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

// http://reactivex.io/documentation/operators/switch.html

var tempo = 500;

var obStream1 = Observable
    .Interval(TimeSpan.FromMilliseconds(tempo))
    .Select(i => 1)
    .Take(1);

var obStream2 = Observable
    .Interval(TimeSpan.FromMilliseconds(tempo))
    .Select(i => 2)
    .Take(2);

var obStream3 = Observable
    .Interval(TimeSpan.FromMilliseconds(tempo))
    .Select(i => 3)
    .Take(3);

async Task<IObservable<IObservable<int>>> getObservables()
{
    var obs = obStream1.Select(i => obStream2);

    await Task.Delay(tempo/2);

    obs = obs.Select(i => obStream3);

    return obs;
}

Action<int> defaultNext = i => $"from stream {i}".Dump();
Action<Exception> defaultError = ex => $"error: {ex.Message}".Dump();
Action defaultComplete = () => "complete".Dump();

var observables = await getObservables();
observables.Switch().Subscribe(defaultNext, defaultError, defaultComplete);
