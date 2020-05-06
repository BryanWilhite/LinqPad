<Query Kind="Program" />

void Main()
{
    var stack = new MyStack();

    stack.Push("dog");
    stack.Push("cat");
    stack.Push("bear");

    stack.Peek().Dump("last in...");
    stack.Pop().Dump("first out...");

    stack.Dump();
}

class MyStack
{
    public MyStack()
    {
        this._storage = new KeyValuePair<int, string>[] { };
    }

    public void Push(string element)
    {
        this._size++;
        this._storage = this._storage
            .Union(new[] { new KeyValuePair<int, string>(this._size, element) })
            .ToArray();
    }

    public string Pop()
    {
        var data = this._storage.Last();
        this._storage = this._storage
            .Where(i => i.Key != data.Key)
            .ToArray();
        this._size--;
        return data.Value;
    }

    public string Peek()
    {
        var data = this._storage.Last();
        return data.Value;
    }

    public int Size
    {
        get { return this._size; }
    }

    public IEnumerable<KeyValuePair<int, string>> Storage
    {
        get { return this._storage.Reverse(); }
    }

    private KeyValuePair<int, string>[] _storage;
    private int _size;
}

/*
    This rendition of the Stack is based on a lecture by Beiatrix Pedrasa
    [ 🕸 http://www.beiatrix.com/ ]
    [ 🚜 https://github.com/beiatrix/ ]
    [ 📽 https://www.youtube.com/watch?v=1AJ4ldcH2t4 ]
    [ 📖 https://en.wikipedia.org/wiki/Stack_(abstract_data_type) ]

    BTW: the real .NET Stack is the `Stack<T>`.
    [📖 https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.stack-1?view=netstandard-2.1 ]
*/