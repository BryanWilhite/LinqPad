<Query Kind="Program" />

void Main()
{
    var queue = new MyQueue();

    queue.Enqueue("nuclear submarine from France");
    queue.Enqueue("seahorse");
    queue.Enqueue("dolphin");
    queue.Enqueue("whaleshark");

    queue[0].Dump("first in...");
    queue.Dequeue().Dump("first out...");

    queue.Dump();
}

class MyQueue
{
    public MyQueue()
    {
        this._storage = new KeyValuePair<int, string>[] { };
    }

    public void Enqueue(string element)
    {
        this._storage = this._storage
            .Union(new[] { new KeyValuePair<int, string>(this._tail, element) })
            .ToArray();
        this._tail++;
    }

    public string Dequeue()
    {
        var data = this._storage[this._head];
        this._storage = this._storage
            .Where(i => i.Key != data.Key)
            .ToArray();
        this._head++;
        return data.Value;
    }

    #region .NET super-specific stuff:

    public string this[int key] // indexer
    {
        get
        {
            return this._storage
                .FirstOrDefault(i => i.Key == key)
                .Value;
        }
    }

    #endregion

    public KeyValuePair<int, string>[] Storage { get { return this._storage; } }

    public int Head { get { return this._head; } }
    public int Tail { get { return this._tail; } }

    private KeyValuePair<int, string>[] _storage;
    private int _head;
    private int _tail;
}

/*
    This rendition of the Stack is based on a lecture by Beiatrix Pedrasa
    [ 🕸 http://www.beiatrix.com/ ]
    [ 🚜 https://github.com/beiatrix/ ]
    [ 📽 https://www.youtube.com/watch?v=1AJ4ldcH2t4 ]
    [ 📖 https://en.wikipedia.org/wiki/Queue_(abstract_data_type) ]

    BTW: the real .NET Queue is the `Queue<T>`.
    [📖 https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.queue-1?view=netstandard-2.1 ]
    [📖 https://stackoverflow.com/questions/496896/how-to-delete-an-element-from-an-array-in-c-sharp ]
*/