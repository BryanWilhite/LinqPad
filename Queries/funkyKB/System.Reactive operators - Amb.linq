<Query Kind="Statements">
  <Reference>&lt;RuntimeDirectory&gt;\System.Runtime.dll</Reference>
  <NuGetReference>System.Reactive</NuGetReference>
  <Namespace>System.Reactive.Linq</Namespace>
  <Namespace>System.Reactive.Subjects</Namespace>
</Query>

// http://reactivex.io/documentation/operators/amb.html

var tempo = 50;

var obStream1 = Observable
    .Interval(TimeSpan.FromMilliseconds(tempo))
    .Select(i => 1)
    .Take(10);

var obStream2 = Observable
    .Interval(TimeSpan.FromMilliseconds(tempo/2))
    .Select(i => 2)
    .Take(2);

var obStream3 = Observable
    .Interval(TimeSpan.FromMilliseconds(tempo))
    .Select(i => 3)
    .Take(1);

Action<int> defaultNext = (i) => $"from stream {i}".Dump();
Action<Exception> defaultError = ex => $"error: {ex.Message}".Dump();
Action defaultComplete = () => "complete".Dump();

var observable = obStream1
    .Amb(obStream2)
    .Amb(obStream3);

observable.Subscribe(defaultNext, defaultError, defaultComplete);