<Query Kind="Statements" />

var data = new[] { 1, 2, 3, 4, 5, 6, 7 };

void walk<T>(IEnumerable<T> collection, int index)
{
    collection.ElementAt(index).Dump();

    int nextIndex = index + 1;
    if (nextIndex < collection.Count()) walk(data, nextIndex);
}

walk(data, 0);
