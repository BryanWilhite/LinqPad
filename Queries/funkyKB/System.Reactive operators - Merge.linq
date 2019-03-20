<Query Kind="Statements">
  <Reference>&lt;RuntimeDirectory&gt;\System.Runtime.dll</Reference>
  <NuGetReference>System.Reactive</NuGetReference>
  <Namespace>System</Namespace>
  <Namespace>System.Reactive.Linq</Namespace>
</Query>

// http://reactivex.io/documentation/operators/merge.html

var obStream1 = Observable
    .Interval(TimeSpan.FromMilliseconds(50))
    .Select((x, i) => i)
    .Take(10);

var obStream2 = Observable
    .Interval(TimeSpan.FromMilliseconds(60))
    .Select((x, i) => i * -10)
    .Take(5);

var obStream3 = Observable
    .Interval(TimeSpan.FromMilliseconds(70))
    .Select((x, i) => i * 100)
    .Take(3);

var observables = obStream1
    .Merge(obStream2)
    .Merge(obStream3);

Action<int> defaultNext = i => $"next {i}".Dump();
Action<Exception> defaultError = ex => $"error: {ex.Message}".Dump();
Action defaultComplete = () => "complete".Dump();

observables.Subscribe(defaultNext, defaultError, defaultComplete);