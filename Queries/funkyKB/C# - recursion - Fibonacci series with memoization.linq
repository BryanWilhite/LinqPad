<Query Kind="Statements" />

var cache = new Dictionary<double, double>
{
    { 0, 0 },
    { 1, 1 },
    { 2, 1 },
};

double GenerateFibonacciSeries(double n)
{
    if(cache.Keys.Contains(n))
        return cache[n];
    else
    {
        var value = GenerateFibonacciSeries(n - 1) + GenerateFibonacciSeries(n - 2);
        cache.Add(n, value);
        return value;
    }
}

Enumerable
    .Range(0, 96)
    .Select(i => Convert.ToDouble(i))
    .Select(GenerateFibonacciSeries)
    .ToArray()
    .Dump();

// 📖 https://www.csharpstar.com/fibonacci-series-in-csharp/