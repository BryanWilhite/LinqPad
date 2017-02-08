<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Runtime.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.Tasks.dll</Reference>
  <NuGetReference>Rx-Main</NuGetReference>
  <Namespace>System.Net</Namespace>
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
    var tasks =
        from i in Enumerable.Range (0, 50)
        select GetWebPage ("http://foo" + i);
        
    tasks.WithLimitedConcurrency (5).Dump();
}

async Task<string> GetWebPage (string uri)
{
    await Task.Delay (1000 + random.Next(1000));
    return "<html>You downloaded: " + uri + "</html>";
}

Random random = new Random();

public static class Extensions
{
    public static IObservable<T> WithLimitedConcurrency<T> (this IEnumerable<Task<T>> taskFunctions, int maxConcurrency)
    {
        return new ConcurrencyLimiter<T> (taskFunctions, maxConcurrency).IObservable;
    }
    
    class ConcurrencyLimiter<T>
    {
        object _locker = new object(), _disposalLocker = new object();
        IEnumerator<Task<T>> _rator;
        int _outstanding;        
        bool _disposed;
        bool Disposed { get { lock (_disposalLocker) return _disposed; } set { lock (_disposalLocker) _disposed = value; } }
        public readonly IObservable<T> IObservable;
    
        public ConcurrencyLimiter (IEnumerable<Task<T>> taskFunctions, int maxConcurrency)
        {
            _rator = taskFunctions.GetEnumerator();                
            IObservable = Observable.Create<T> (observer =>
            {
                for (int i = 0; i < maxConcurrency; i++) PullNextTask (observer);
                return Disposable.Create (() => Disposed = true);
            });
        }
        
        void PullNextTask (IObserver<T> observer)
        {
            lock (_locker)
            {
                if (Disposed) ClearRator();
                if (_rator == null) return;
                if (!_rator.MoveNext())
                {
                    ClearRator();
                    if (_outstanding == 0) observer.OnCompleted();
                    return;
                }
                _outstanding++;    
                _rator.Current.ContinueWith (ant => ProcessTaskCompletion (observer, ant));
            }
        }
        
        void ProcessTaskCompletion (IObserver<T> observer, Task<T> antecedent)
        {
            lock (_locker) 
                if (Disposed || antecedent.IsFaulted || antecedent.IsCanceled)
                {
                    ClearRator();
                    if (!Disposed)
                        observer.OnError (antecedent.Exception == null ? new OperationCanceledException() : antecedent.Exception.InnerException);
                }
                else 
                {
                    observer.OnNext (antecedent.Result);
                    if (--_outstanding == 0 && _rator == null) 
                        observer.OnCompleted(); 
                    else
                        PullNextTask (observer);
                }
        }
        
        void ClearRator()
        {
            if (_rator == null) return;
            _rator.Dispose();
            _rator = null; 
        }        
    }
}