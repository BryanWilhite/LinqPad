<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Runtime.dll</Reference>
  <NuGetReference>System.Reactive</NuGetReference>
  <Namespace>System.Reactive.Disposables</Namespace>
</Query>

void Main()
{
    var numbers = new MySequenceOfNumbers();
    var observer = new MyConsoleObserver<int>();
    numbers.Subscribe(observer);
}

public class MyConsoleObserver<T> : IObserver<T>
{
    public void OnNext(T value)
    {
        Console.WriteLine("Received value {0}", value);
    }
    public void OnError(Exception error)
    {
        Console.WriteLine("Sequence faulted with {0}", error);
    }
    public void OnCompleted()
    {
        Console.WriteLine("Sequence terminated");
    }
}

public class MySequenceOfNumbers : IObservable<int>
{
    public IDisposable Subscribe(IObserver<int> observer)
    {
        observer.OnNext(1);
        observer.OnNext(2);
        observer.OnNext(3);
        observer.OnCompleted();
        return Disposable.Empty;
    }
}