<Query Kind="Statements">
  <Reference>&lt;RuntimeDirectory&gt;\System.Runtime.dll</Reference>
  <NuGetReference>Rx-Main</NuGetReference>
  <Namespace>System.Reactive.Linq</Namespace>
</Query>

//http://www.introtorx.com/content/v1.0.10621.0/14_HotAndColdObservables.html#PublishAndConnect

var coreObservable = Observable.Interval(TimeSpan.FromSeconds(1));

coreObservable.Subscribe(i=>i.Dump("core"));

var obs1 = coreObservable.Select(i=>i*2).Take(5);
var obs2 = coreObservable.Select(i=>i+2).Skip(5).Take(5);