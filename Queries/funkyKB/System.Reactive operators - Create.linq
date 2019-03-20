<Query Kind="Statements">
  <Reference>&lt;RuntimeDirectory&gt;\System.Runtime.dll</Reference>
  <NuGetReference>System.Reactive</NuGetReference>
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

// http://reactivex.io/documentation/operators/create.html

var lifetime = 10;
var tempo = 500;

async Task StartHeartbeat(IObserver<int> observer, CancellationToken cancelToken)
{
    int beat = 0;
    while (beat < lifetime)
    {
        await Task.Delay(tempo, cancelToken);
        observer.OnNext(beat);
        beat++;
    }
}

var observable = Observable.Create<int>(StartHeartbeat);

observable.Dump();
observable.Count().Dump();