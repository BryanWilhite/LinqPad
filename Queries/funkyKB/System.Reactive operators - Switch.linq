<Query Kind="Statements">
  <Reference>&lt;RuntimeDirectory&gt;\System.Runtime.dll</Reference>
  <NuGetReference>System.Reactive</NuGetReference>
  <Namespace>System.Reactive.Linq</Namespace>
  <Namespace>System.Reactive.Subjects</Namespace>
</Query>

// http://reactivex.io/documentation/operators/switch.html

var tempo = 50;

var obStream1 = Observable
    .Interval(TimeSpan.FromMilliseconds(tempo))
    .Select(i => { /* if(i == 9) throw new Exception("error!"); */ return 1; })
    .Take(10);

var obStream2 = Observable
    .Interval(TimeSpan.FromMilliseconds(tempo))
    .Select(i => 2)
    .Take(2);

var obStream3 = Observable
    .Interval(TimeSpan.FromMilliseconds(tempo))
    .Select(i => 3)
    .Take(1);

Action<int> defaultNext = (i) => $"from stream {i}".Dump();
Action<Exception> defaultError = ex => $"error: {ex.Message}".Dump();
Action defaultComplete = () => "complete".Dump();

var observables = obStream1
    .Select(i => { i.Dump("stream 2"); return obStream2; })
    .Select(i => { i.Dump("stream 3"); return obStream3; });

observables.Switch().Subscribe(defaultNext, defaultError, defaultComplete);
