<Query Kind="Statements" />

var cache = new Dictionary<int, int>
{
    { 1, 1 },
};

int cumulativeSum(int n)
{
    int value;

    if (cache.Keys.Contains(n))
        return cache[n];
    else
    {
        value = n + cumulativeSum(n - 1);
        cache.Add(n, value);
        return value;
    }
}

cumulativeSum(90).Dump();
cache.Dump();
