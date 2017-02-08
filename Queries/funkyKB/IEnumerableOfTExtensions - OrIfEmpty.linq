<Query Kind="Program" />

public static class IEnumerableOfTExtensions
{
    public static IEnumerable<TEnumerable> OrIfEmpty<TEnumerable>(this IEnumerable<TEnumerable> enumerable,
        IEnumerable<TEnumerable> alternateEnumerable)
    {
        if (enumerable == null) return Enumerable.Empty<TEnumerable>();
        return enumerable.Any() ? enumerable : alternateEnumerable;
    }
}

void Main()
{
    var list = new List<int>{-1,-2,-3,-4,-5,-6,-7,-8,-9,0,1,2,3,4,5,6,7,8,9};

    list.Where(i => i > 9)
        .OrIfEmpty(list.Where(i => i > 5))
        .Dump();
}