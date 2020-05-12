<Query Kind="Statements" />

int GenerateFibonacciSeries(int n)
{
    int value;

    $"before: {nameof(n)}: {n}".Dump();

    switch (n)
    {
        case 0: // base case
            value = 0;
            break;
        case 1: // base case
        case 2: // base case
            value = 1;
            break;
        default: // recursive case: this tail-call is augementing
            value = GenerateFibonacciSeries(n - 1) + GenerateFibonacciSeries(n - 2);
            break;
    }

    $"after: {nameof(n)}: {n}".Dump();

    return value;
}

Enumerable.Range(0, 16).Select(GenerateFibonacciSeries).ToArray().Dump();

// ⚠ the time complexity here is expoential!
// ⚠ `Int32.MaxValue` and `Int64.MaxValue` will easily be exceeded!

// 📖 https://www.csharpstar.com/fibonacci-series-in-csharp/