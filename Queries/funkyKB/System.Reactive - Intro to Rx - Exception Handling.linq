<Query Kind="Statements">
  <Reference>&lt;RuntimeDirectory&gt;\System.Runtime.dll</Reference>
  <NuGetReference>System.Reactive</NuGetReference>
  <Namespace>System.Threading.Tasks</Namespace>
  <Namespace>System.Reactive.Subjects</Namespace>
</Query>

//http://www.introtorx.com/Content/v1.0.10621.0/03_LifetimeManagement.html#Subscribe

IDisposable sub = null;

// The verbose way:
var values = new Subject<int>();
try
{
    sub = values.Subscribe(value => $"1st subscription received: {value}".Dump());
    values.OnNext(999);
    values.OnError(new Exception("Dummy exception [verbose]"));
}
catch (Exception ex)
{
    ex.Dump("exception [verbose]");
}
finally
{
    if(sub != null) sub.Dispose();
}

// The correct way:
values = new Subject<int>();

Action<int> defaultNext = value => $"1st subscription received: {value}".Dump();
Action<Exception> defaultError = ex => ex.Dump("exception [correct]");

sub = values.Subscribe(defaultNext, defaultError);
values.OnNext(999);
values.OnError(new Exception("Dummy exception"));
