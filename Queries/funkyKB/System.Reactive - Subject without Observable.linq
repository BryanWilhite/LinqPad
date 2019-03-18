<Query Kind="Statements">
  <Reference>&lt;RuntimeDirectory&gt;\System.Runtime.dll</Reference>
  <NuGetReference>MoreLinq</NuGetReference>
  <NuGetReference>System.Reactive</NuGetReference>
  <Namespace>MoreLinq</Namespace>
  <Namespace>System.Reactive.Subjects</Namespace>
  <Namespace>System.Reactive.Linq</Namespace>
</Query>

// calling Subject.OnNext() explicitly implies there is no stream to observe:
var loneSubject = new Subject<int>();
loneSubject.Subscribe(i => i.Dump("kind of useless, mostly"));
Enumerable.Range(0, 5).ForEach(i => loneSubject.OnNext(i));

var usefulSubject = new Subject<int>();
var observable = Observable.Range(0, 5);
usefulSubject.Subscribe(i => i.Dump("slightly better"));

observable.Subscribe(usefulSubject);