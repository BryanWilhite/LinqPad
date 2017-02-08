<Query Kind="Statements">
  <Namespace>System.Collections.Generic</Namespace>
</Query>

/*
    “The easiest to understand definition of Aggregate is that it performs an operation on each element of the list taking into account the operations that have gone before.”
    [http://stackoverflow.com/questions/7105505/linq-aggregate-algorithm-explained]
*/
var data = new[]
{
    new KeyValuePair<byte,string[]>(1,new[]{"one","two","three"}),
    new KeyValuePair<byte,string[]>(2,new[]{"two","three"}),
    new KeyValuePair<byte,string[]>(3,new[]{"two"}),
    new KeyValuePair<byte,string[]>(4,new[]{"two","four"}),
};

data
    .Select(i => i.Value)
    .Aggregate((current, i) =>
    {
        current.Dump("current");
        i.Dump("next");
        var n = current.Intersect(i).ToArray();
        n.Dump("result");
        return n;
    })
    .Dump("aggregate result");