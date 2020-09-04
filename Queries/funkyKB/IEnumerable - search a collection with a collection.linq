<Query Kind="Program" />

void Main()
{
    var collectionOne = new[]
    {
        new OneClass { PropertyOne = "p0" },
        new OneClass { PropertyOne = "p1" },
        new OneClass { PropertyOne = "p2" },
        new OneClass { PropertyOne = "p3" },
    };

    var collectionOther = new[]
    {
        new OtherClass { PropertyOther = "o0" },
        new OtherClass { PropertyOther = "p1" },
        new OtherClass { PropertyOther = "p2" },
        new OtherClass { PropertyOther = "o3" },
    };

    var union = collectionOne
        .Where(i => collectionOther.Count(j => j.PropertyOther == i.PropertyOne) > 0);

    union.Dump();
}

class OneClass
{
    public string PropertyOne { get; set; }
}


class OtherClass
{
    public string PropertyOther { get; set; }
}