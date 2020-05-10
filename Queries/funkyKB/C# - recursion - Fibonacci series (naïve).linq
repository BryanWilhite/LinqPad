<Query Kind="Statements" />

int GenerateFibonacciSeries(int n)
{
    switch (n)
    {
        case 0: return 0;
        case 1: case 2: return 1;
        default: return GenerateFibonacciSeries(n - 1) + GenerateFibonacciSeries(n - 2);
    }
}

Enumerable.Range(0, 16).Select(GenerateFibonacciSeries).ToArray().Dump();

// ⚠ the time complexity here is expoential!
// ⚠ `Int32.MaxValue` and `Int64.MaxValue` will easily be exceeded!

// 📖 https://www.csharpstar.com/fibonacci-series-in-csharp/