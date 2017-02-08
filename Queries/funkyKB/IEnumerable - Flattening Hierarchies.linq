<Query Kind="Program">
  <NuGetReference>MoreLinq</NuGetReference>
  <Namespace>MoreLinq</Namespace>
</Query>

class MyMeta
{
    public MyMeta() { }
    
    public MyMeta(IEnumerable<MyMeta> children)
    {
        children.ForEach(i=> i.Parent = this);
        this.Children = children;
    }
    
    public IEnumerable<MyMeta> Children;

    public MyMeta Parent;

    public string UniqueId;
}

void Main()
{
    var root = new MyMeta
    (
        new[]
        {
            new MyMeta { UniqueId = "L1:one" },
            new MyMeta { UniqueId = "L1:two" },
            new MyMeta { UniqueId = "L1:three" },
            new MyMeta { UniqueId = "L1:four" },
            new MyMeta
            (
                new[]
                {
                    new MyMeta { UniqueId = "L2:one" },
                    new MyMeta { UniqueId = "L2:two" },
                    new MyMeta
                    (
                        new[]
                        {
                            new MyMeta { UniqueId = "L3:one" },
                        }
                    )
                    { UniqueId = "L2:three" },
                    new MyMeta { UniqueId = "L2:four" },
                }
            )
            { UniqueId = "L1:five" },
            new MyMeta { UniqueId = "L1:six" },
            new MyMeta { UniqueId = "L1:seven" },
            new MyMeta { UniqueId = "L1:eight" },
        }
    )
    {
        UniqueId = "root",
    };
    
    root.Children
        .Flatten(i => { return i.Children; })
        .Select(i => i.UniqueId)
        .Dump();
}

public static class IEnumerableOfTExtensions
{
    public static IEnumerable<TSource> Flatten<TSource>(this IEnumerable<TSource> source, Func<TSource, IEnumerable<TSource>> childGetter)
    {
        if (source == null) return Enumerable.Empty<TSource>();
        var flattenedList = new List<TSource>(source);

        source.ForEach(i =>
        {
            var children = childGetter(i);
            if (children != null) flattenedList.AddRange(children.Flatten(childGetter));
        });

        return flattenedList;
    }
}