<Query Kind="Program">
  <Connection>
    <ID>cd884946-dbf0-42d1-ac90-d3a615f50162</ID>
    <Driver>AstoriaAuto</Driver>
    <Server>http://services.odata.org/AdventureWorksV3/AdventureWorks.svc</Server>
  </Connection>
</Query>

void Main()
{
    /*
        implement the cumulative sum function from the R programming language

        //cumsum(int[] array, int startPos, int count);
        var set = new[] { 4, 1, 0, 3, 2 };
        cumsum(set, 3, 4) == new[] { 3, 5, 9, 10 };
        cumsum(set, 4, 2) == new[] { 2, 6 };
    */

    var set = new[] { 4, 1, 0, 3, 2 };
    set.ToCulmulativeSum(3, 4).Dump();
    set.ToCulmulativeSum(4, 2).Dump();
}

static class IEnumerableOfTExtensions
{
    public static IEnumerable<int> ToCulmulativeSum(this IEnumerable<int> enumerable, int startPos, int count)
    {
        if (enumerable == null) return Enumerable.Empty<int>();

        var wrappedSet = enumerable.Wrap(startPos, count);
        wrappedSet.Dump("wrapped set");
        return wrappedSet
            .Select((x, i) => x + wrappedSet.Take(i).Sum());
    }

    public static IEnumerable<T> Wrap<T>(this IEnumerable<T> enumerable, int startPos, int count)
    {
        if (enumerable == null) return Enumerable.Empty<T>();

        var length = enumerable.Count();

        if(startPos > length) throw new ArgumentException("The start position is larger than the length of the enumerable.", "startPos");

        var wrappedSet = enumerable
            .Skip(startPos)
            .Union(enumerable.Take(length - startPos));
        return wrappedSet;
    }
}