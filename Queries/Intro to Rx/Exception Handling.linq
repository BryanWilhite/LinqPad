<Query Kind="Statements">
  <Reference>&lt;RuntimeDirectory&gt;\System.Runtime.dll</Reference>
  <NuGetReference>Rx-Main</NuGetReference>
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

//http://www.introtorx.com/Content/v1.0.10621.0/03_LifetimeManagement.html#Subscribe
var values = new Subject<int>();
try
{
    values.Subscribe(value => Console.WriteLine("1st subscription received {0}", value));
}
catch (Exception)
{
    Console.WriteLine("Won't catch anything here!");
}
values.OnNext(0);
values.OnError(new Exception("Dummy exception"));

//The correct way to way to handle exceptions is to provide a delegate for OnError notifications...

values = new Subject<int>();
values.Subscribe(
value => Console.WriteLine("1st subscription received {0}", value),
ex => Console.WriteLine("Caught an exception : {0}", ex));
values.OnNext(0);
values.OnError(new Exception("Dummy exception"));