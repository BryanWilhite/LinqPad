<Query Kind="Statements" />

var data = new[] { 1, 2, 3, 4, 5, 6, 7 };

void walk<T>(IEnumerable<T> collection, int i)
{
    $"before: {nameof(i)}: {i}".Dump();

    int nextIndex = i + 1;
    if (nextIndex < collection.Count()) // recursive case
    {
        walk(data, nextIndex);
    }
    else // base case
    {
        collection.Dump(nameof(collection));
    }

    $"after: {nameof(i)}: {i}".Dump();
}

walk(data, 0);
