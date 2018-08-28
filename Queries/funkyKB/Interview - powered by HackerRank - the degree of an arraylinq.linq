<Query Kind="Statements" />

/*
    the degree of this array is defined
    as the maximum frequency
    of any one of its elements.
    [https://leetcode.com/problems/degree-of-an-array/description/]
*/

var arr = new[] {6,1,1,2,1,2,2};

var groups = arr.GroupBy(i => i);
groups.Dump();
var degree = groups.Max(g => g.Count());
degree.Dump("degree");
var maxVs = groups.Where(g => g.Count() == degree).Select(g => g.First());
maxVs.Dump("maxVs");
var subArrayMeta = arr
    .Select((x, i) => new { x, i })
    .Where(o => maxVs.Contains(o.x))
    .GroupBy(meta => meta.x);
subArrayMeta.Dump("sub-array meta");
var shortestSubArr = subArrayMeta.Min(o => (o.Last().i - o.First().i) + 1).Dump();

new[] {degree, shortestSubArr}.Dump();

/*
    5,1,2,2,3,1 => 2
    6,1,1,2,1,2,2 => 4
    17,802,88,747,23,160,681,254,46,663,752,741,857,802,387,790,528,93 => 13
*/