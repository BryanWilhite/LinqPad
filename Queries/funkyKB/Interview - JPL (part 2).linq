<Query Kind="Program">
  <NuGetReference>MoreLinq</NuGetReference>
  <Namespace>MoreLinq</Namespace>
</Query>

void Main()
{
    var tree = new []
    {
        new MyMeta
        {
            UniqueId = "item-1-1",
            Children = new []
            {
                new MyMeta { UniqueId = "item-2-1" },
                new MyMeta { UniqueId = "item-2-2" },
                new MyMeta
                {
                    UniqueId = "item-2-3",
                    Children = new[]
                    { 
                        new MyMeta { UniqueId = "item-3-1" },
                        new MyMeta { UniqueId = "item-3-2" },
                    }
                },
            }
        },
        new MyMeta { UniqueId = "item-1-2" },
    };
    
    tree.Flatten(i => i.Children).Select(i =>i.UniqueId).Dump();
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

class MyMeta
{
    public MyMeta() { }
    
    public MyMeta(IEnumerable<MyMeta> children)
    {
        children.ForEach(i=> i.Parent = this);
        this.Children = children;
    }

    public IEnumerable<MyMeta> Children { get; set;}

    public MyMeta Parent { get; set;}

    public string UniqueId { get; set;}
}