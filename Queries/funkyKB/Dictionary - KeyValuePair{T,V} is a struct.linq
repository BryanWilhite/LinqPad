<Query Kind="Statements" />

//a reminder that `KeyValuePair<TKey, TValue>` is a struct:

var collection = new Dictionary<string, int>
{
    { "one", 1 },
    { "two", 2 },
    { "three", 3 }, 
};

collection.FirstOrDefault(i => i.Value == 4).Dump("does not return null");