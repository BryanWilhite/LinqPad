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
    var provider = new LocationTracker();
    var reporter1 = new LocationReporter("FixedGPS");
    reporter1.Subscribe(provider);
    var reporter2 = new LocationReporter("MobileGPS");
    reporter2.Subscribe(provider);
    
    provider.TrackLocation(new Location(47.6456, -122.1312));
    provider.TrackLocation(new Location(47.6460, -122.1233));
    reporter1.Unsubscribe();
    provider.TrackLocation(new Location(47.6677, -122.1199));
    provider.TrackLocation(null);
    provider.EndTransmission();
}

public struct Location
{
    public Location(double latitude, double longitude)
    {
        this.lat = latitude;
        this.lon = longitude;
    }

    public double Latitude { get { return this.lat; } }

    public double Longitude { get { return this.lon; } }

    double lat, lon;
}

public class LocationTracker : IObservable<Location>
{
    public LocationTracker()
    {
        observers = new List<IObserver<Location>>();
    }

    private List<IObserver<Location>> observers;

    public IDisposable Subscribe(IObserver<Location> observer) 
    {
        if (!observers.Contains(observer)) observers.Add(observer);
        return new Unsubscriber(observers, observer);
    }

    private class Unsubscriber : IDisposable
    {
        public Unsubscriber(List<IObserver<Location>> observers, IObserver<Location> observer)
        {
            this._observers = observers;
            this._observer = observer;
        }

        public void Dispose()
        {
            if (_observer != null && _observers.Contains(_observer))
            _observers.Remove(_observer);
        }
        
        List<IObserver<Location>>_observers;
        IObserver<Location> _observer;
    }

    public void TrackLocation(Nullable<Location> loc)
    {
        foreach (var observer in observers)
        {
            if (!loc.HasValue) observer.OnError(new LocationUnknownException());
            else observer.OnNext(loc.Value);
        }
    }

    public void EndTransmission()
    {
        foreach (var observer in observers.ToArray())
        {
            if (observers.Contains(observer)) observer.OnCompleted();
        }
        observers.Clear();
    }
}

public class LocationUnknownException : Exception
{
   internal LocationUnknownException() 
   { }
}

public class LocationReporter : IObserver<Location>
{
    public LocationReporter(string name)
    {
        this.instName = name;
    }

    public string Name { get{ return this.instName; } }

    public virtual void Subscribe(IObservable<Location> provider)
    {
        if (provider != null) unsubscriber = provider.Subscribe(this);
    }

    public virtual void OnCompleted()
    {
        Console.WriteLine("The Location Tracker has completed transmitting data to {0}.", this.Name);
        this.Unsubscribe();
    }

    public virtual void OnError(Exception e)
    {
        Console.WriteLine("{0}: The location cannot be determined.", this.Name);
    }

    public virtual void OnNext(Location value)
    {
        Console.WriteLine("{2}: The current location is {0}, {1}", value.Latitude, value.Longitude, this.Name);
    }

    public virtual void Unsubscribe()
    {
        Console.WriteLine("{0}: unsubscribing...", this.Name);
        unsubscriber.Dispose();
    }

    string instName;
    IDisposable unsubscriber;
}