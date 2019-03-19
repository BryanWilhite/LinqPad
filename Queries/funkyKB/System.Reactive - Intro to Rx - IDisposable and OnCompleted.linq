<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Runtime.dll</Reference>
  <NuGetReference>System.Reactive</NuGetReference>
  <Namespace>System.Reactive.Subjects</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

/*
    “The IDisposable interface is a handy type to have around
    and it is also integral to Rx. I like to think of types
    that implement IDisposable as having explicit lifetime management.”

    [http://www.introtorx.com/Content/v1.0.10621.0/03_LifetimeManagement.html#OnErrorAndOnCompleted]
*/
void Main()
{
    var wrapper = new MyWrapper();
    wrapper.DoIt();
}

class MyWrapper
{
    public void DoIt()
    {
        _subject = new Subject<int>();
        Action<int> defaultNext = i => i.Dump();
        Action completedAction = () => "completed".Dump();

        _subject.Subscribe(defaultNext, completedAction);
        _subject.HasObservers.Dump("Subject has observers?");
        _subject.IsDisposed.Dump("Subject is disposed?");

        _subject.OnNext(1);
        _subject.OnCompleted();
        _subject.HasObservers.Dump("Subject has observers?");

        _subject.OnNext(2);
        string.Empty.Dump("completed subject does not move next");
    }

    ~MyWrapper()
    {
        _subject.IsDisposed.Dump("Subject is disposed? ...eventually?");
        /*
            The `IDisposable` implementation of `Subject<T>`
            “enables observers to cancel notifications at any time
            before the provider has stopped sending them…”

            [https://docs.microsoft.com/en-us/dotnet/api/system.iobservable-1?view=netstandard-2.0#remarks]
            
            “An `IDisposable` implementation that enables the provider
            to remove observers when notification is complete.”
            
            [https://docs.microsoft.com/en-us/dotnet/standard/events/observer-design-pattern#applying-the-pattern]
        */
    }

    Subject<int> _subject;
}