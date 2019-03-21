<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Runtime.dll</Reference>
  <NuGetReference>System.Reactive</NuGetReference>
  <Namespace>System.Threading.Tasks</Namespace>
  <Namespace>System.Reactive.Linq</Namespace>
</Query>

void Main()
{
    var observable = GetHeartBeat()
        .TimeInterval()
        .Buffer(3, 1)
        .Select((buffer, index) => string.Format("{0}, {1}", buffer.Average(x => 60 / x.Interval.TotalSeconds), index));

    observable.Dump();
}

public IObservable<int> GetHeartBeat()
{
    return Observable.Create<int>((observer, cancelToken) => Start(observer, cancelToken));
}

async Task Start (IObserver<int> observer, CancellationToken cancelToken)
{
    int beat = 0;
    var random = new Random();
    while(beat < 10)
    {
        await Task.Delay(random.Next (500) + 700, cancelToken);
        observer.OnNext(beat);
        beat++;
    }
}