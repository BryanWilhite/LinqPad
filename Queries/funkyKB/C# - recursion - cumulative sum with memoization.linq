<Query Kind="Statements" />

var cache = new Dictionary<int, int>
{
    { 1, 1 },
};

int cumulativeSum(int n)
{
    int value;

    $"before: {nameof(n)}: {n}".Dump();

    if (cache.Keys.Contains(n)) // base case
    {
        value = cache[n];
    }
    else // recursive case
    {
        value = n + cumulativeSum(n - 1);
        cache.Add(n, value);
    }

    $"after: {nameof(n)}: {n}".Dump();

    return value;
}

cumulativeSum(90).Dump();
cache.Dump();
