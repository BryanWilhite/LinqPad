<Query Kind="Program">
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

void Main()
{
    var observable = GetHeartBeat()
        .TimeInterval()
        .Buffer (3, 1)
        .Select (buffer => buffer.Average(x => 60 / x.Interval.TotalSeconds));

    observable.Dump();
    observable.Count().Dump();
}

public IObservable<int> GetHeartBeat()
{
    return Observable.Create<int> ((observer, cancelToken) => Start (observer, cancelToken));
}

async Task Start (IObserver<int> observer, CancellationToken cancelToken)
{
    int beat = 0;
    var random = new Random();
    while (beat < 10)
    {
        await Task.Delay (random.Next (500) + 700, cancelToken);
        observer.OnNext (beat);
        beat++;
    }
}
