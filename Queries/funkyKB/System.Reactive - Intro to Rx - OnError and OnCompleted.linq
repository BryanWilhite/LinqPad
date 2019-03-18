<Query Kind="Statements">
  <Reference>&lt;RuntimeDirectory&gt;\System.Runtime.dll</Reference>
  <NuGetReference>System.Reactive</NuGetReference>
  <Namespace>System.Reactive.Subjects</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

//http://www.introtorx.com/Content/v1.0.10621.0/03_LifetimeManagement.html#OnErrorAndOnCompleted
var subject = new Subject<int>();
subject.Subscribe(Console.WriteLine, () => Console.WriteLine("Completed"));
subject.OnNext(1);
subject.OnCompleted();
subject.OnNext(2);