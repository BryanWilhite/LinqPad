<Query Kind="Statements" />

int GenerateFibonacciSeries(int n)
{
    switch (n)
    {
        case 0: case 1: return n;
        default: return GenerateFibonacciSeries(n - 1) + GenerateFibonacciSeries(n - 2);
    }
}

Enumerable.Range(0, 16).Select(GenerateFibonacciSeries).ToArray().Dump();

// 📖 https://www.csharpstar.com/fibonacci-series-in-csharp/