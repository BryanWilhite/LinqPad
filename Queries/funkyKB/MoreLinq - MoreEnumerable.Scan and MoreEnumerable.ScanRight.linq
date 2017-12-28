<Query Kind="Statements">
  <NuGetReference>MoreLinq</NuGetReference>
  <Namespace>MoreLinq</Namespace>
</Query>

var numbers = new[] { 50, -20, -10, -10, 5 };

numbers.Scan((aggregation, current) => aggregation + current).Dump("scan");

/*
   MoreEnumerable.Scan<TSource> [http://morelinq.github.io/2.9/ref/api/html/M_MoreLinq_MoreEnumerable_Scan__1.htm]
   produces the same result as Rollup<TSource, TResult> by Eric White (c. 2010)
   [https://blogs.msdn.microsoft.com/ericwhite/2010/02/15/rollup-extension-method-create-running-totals-using-linq-to-objects/]
*/

numbers.ScanRight((current, aggregation) =>
{
    current.Dump("current");
    return aggregation + current;
}).Dump("scan right");

/*
    MoreEnumerable.ScanRight<TSource> reads `numbers` (above) from right to left,
    skipping the last value (5) which is used in the aggregation.
*/
