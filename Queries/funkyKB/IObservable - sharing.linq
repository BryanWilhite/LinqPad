<Query Kind="Statements">
  <Reference>&lt;RuntimeDirectory&gt;\System.Runtime.dll</Reference>
  <NuGetReference>System.Reactive</NuGetReference>
  <Namespace>System.Reactive.Linq</Namespace>
</Query>

//http://www.introtorx.com/content/v1.0.10621.0/14_HotAndColdObservables.html#PublishAndConnect
var limit = 10;

var coreObservable = Observable
    .Interval(TimeSpan.FromSeconds(1))
    .Take(limit);
coreObservable.Subscribe(i=>i.Dump(nameof(coreObservable)));

var obs1 = coreObservable.Select(i => i * 2).Take(limit/2);
obs1.Subscribe(i => i.Dump(nameof(obs1)));

var obs2 = coreObservable.Select(i => i + 2).Skip(limit/2).Take(limit/2);
obs2.Subscribe(i => i.Dump(nameof(obs2)));
