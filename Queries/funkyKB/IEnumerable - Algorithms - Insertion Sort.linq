<Query Kind="Program">
  <Namespace>System.Diagnostics.Contracts</Namespace>
</Query>

void Main()
{
    (new List<int>(new[] { 2 }))
        .WithInsertionSort(4)
        .WithInsertionSort(0)
        .WithInsertionSort(3)
        .WithInsertionSort(1)
        .Dump("sorted array");
}

static class IListExtensions
{
    public static IList<int> WithInsertionSort(this IList<int> integers, int newInteger)
    {
        int i, j; /* counters */

        integers.Add(newInteger);
        var n = integers.Count();

        for (i = 1; i < n; i++)
        {
            j = i;
            while ((j > 0) && (integers[j] < integers[j - 1]))
            {
                integers.Swap(j, j - 1);
                j = j - 1;
            }
        }
        return integers;
    }

    public static void Swap<T>(this IList<T> list, int firstIndex, int secondIndex)
    { //https://stackoverflow.com/a/2094260/22944
        Contract.Requires(list != null);
        Contract.Requires(firstIndex >= 0 && firstIndex < list.Count);
        Contract.Requires(secondIndex >= 0 && secondIndex < list.Count);

        if (firstIndex == secondIndex) return;

        T temp = list[firstIndex];
        list[firstIndex] = list[secondIndex];
        list[secondIndex] = temp;
    }
}
