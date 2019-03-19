<Query Kind="Statements">
  <Reference>&lt;RuntimeDirectory&gt;\System.Runtime.dll</Reference>
  <NuGetReference>System.Reactive</NuGetReference>
  <Namespace>System.Reactive.Subjects</Namespace>
</Query>

//http://www.introtorx.com/Content/v1.0.10621.0/03_LifetimeManagement.html#Unsubscribing
IDisposable firstSubscription = null;
IDisposable secondSubscription = null;

var values = new Subject<int>();
try
{
    firstSubscription = values.Subscribe(value => Console.WriteLine("1st subscription received {0}", value));
    secondSubscription = values.Subscribe(value => Console.WriteLine("2nd subscription received {0}", value));
    values.OnNext(0);
    values.OnNext(1);
    values.OnNext(2);
    values.OnNext(3);
    firstSubscription.Dump("about to call Dispose()...");
    firstSubscription.Dispose();
    Console.WriteLine("Disposed of 1st subscription");
    values.OnNext(4);
    values.OnNext(5);
}
finally
{
    if(firstSubscription != null) {
        firstSubscription.Dump("Dispose() called twice?");
        firstSubscription.Dispose();
    }
    if(secondSubscription != null) secondSubscription.Dispose();
}