<Query Kind="Program" />

void Main()
{
    var numbers = new[] { 50, -20, -10, -10, 5 };
    numbers.Rollup(0, (aggregation, current) => aggregation + current).Dump("rollup");
}

static class IEnumerableOfExtensions
{
    public static IEnumerable<TResult> Rollup<TSource, TResult>(
        this IEnumerable<TSource> source,
        TResult seed,
        Func<TSource, TResult, TResult> projection)
    {
        TResult nextSeed = seed;
        foreach (TSource src in source)
        {
            TResult projectedValue = projection(src, nextSeed);
            nextSeed = projectedValue;
            yield return projectedValue;
        }
    }
}

/*
    “This extension method is sort of a cross between the Select and Aggregate extension methods.
    Like Select, when using this extension method you write a lambda expression
    to project the new value in a new collection. Unlike Select, in the projection lambda expression,
    you are passed the projected value for the immediately preceding element.
    The previous value for the first element in the projected collection is a seed value
    that you pass to the Rollup extension method.”

    from “Rollup Extension Method: Create Running Totals using LINQ to Objects” by Eric White
    [https://blogs.msdn.microsoft.com/ericwhite/2010/02/15/rollup-extension-method-create-running-totals-using-linq-to-objects/]
*/