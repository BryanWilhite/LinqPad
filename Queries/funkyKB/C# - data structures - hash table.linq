<Query Kind="Program" />

void Main()
{
    var hashTable = new HashTable();

    hashTable.Insert("serena", "moon");

    hashTable.Insert("amy", "mercury");
    hashTable.Insert("rei", "mars");
    hashTable.Insert("lita", "jupiter");
    hashTable.Insert("mina", "venus");
    hashTable.Insert("darien", "tuxedo mask");

    //hashTable.GetBuckets().Dump();

    hashTable["darien"] = "saturn";
    hashTable.Remove("rei");

    hashTable.GetBuckets().Dump();
}

class HashTable
{
    internal static int GetHash(string key, int size)
    {
        var hashedKey = 0;
        for (int i = 0; i < key.Length; i++)
        {
            hashedKey += (int)key[i];
        }

        return hashedKey % size;
    }

    public HashTable()
    {
        this._size = 10;
        this._buckets = Enumerable
            .Range(1, this._size)
            .Select(i => new List<KeyValuePair<string, string>>())
            .ToArray();
    }

    public void Insert(string key, string value)
    {
        var bucket = this.GetBucket(key);
        bucket.Add(new KeyValuePair<string, string>(key, value));
    }

    public string Remove(string key)
    {
        var bucket = this.GetBucket(key);
        var pair = bucket.FirstOrDefault(p => p.Key == key);
        if (pair.Key == null) return null;

        bucket.Remove(pair);
        return pair.Value;
    }

    public string Search(string key)
    {
        var bucket = this.GetBucket(key);
        var pair = bucket.FirstOrDefault(p => p.Key == key);
        if (pair.Key == null) return null;

        return pair.Value;
    }

    #region .NET super-specific stuff:

    public string this[string key] // indexer
    {
        get
        {
            return this.Search(key);
        }

        set
        {
            this.Remove(key);
            this.Insert(key, value);
        }
    }

    #endregion

    internal IEnumerable<ICollection<KeyValuePair<string, string>>> GetBuckets()
    {
        return this._buckets;
    }

    private ICollection<KeyValuePair<string, string>> GetBucket(string key)
    {
        var idx = GetHash(key, this._size);
        var bucket = this._buckets.ElementAt(idx);
        return bucket;
    }

    int _size;
    IEnumerable<ICollection<KeyValuePair<string, string>>> _buckets;
}