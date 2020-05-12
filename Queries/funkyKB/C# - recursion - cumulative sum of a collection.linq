<Query Kind="Statements" />

var collection = new[] { 3, 5, 10, 10 };
var cache = new Dictionary<int, int>();

int cumulativeSum(int[] c, int i)
{
    int value;

    $"before: {nameof(i)}: {i}".Dump();

    if (0 < i && i < c.Length) // recursive case
    {
        value = c[i] + cumulativeSum(c, i - 1);
    }
    else
    {
        value = c[0]; // base case
    }

    cache.Add(i, value);

    $"after: {nameof(i)}: {i}".Dump();

    return value;
}

cumulativeSum(collection, 3);

collection.Dump($"{nameof(collection)}");
cache.Dump($"{nameof(cache)}");
