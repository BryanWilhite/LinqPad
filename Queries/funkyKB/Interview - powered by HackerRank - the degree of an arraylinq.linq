<Query Kind="Statements" />

/*
    the degree of this array is defined
    as the maximum frequency
    of any one of its elements.
    [https://leetcode.com/problems/degree-of-an-array/description/]
*/

var arr = new[] {1,2,1,3,2};

var groups = arr.GroupBy(i => i);
groups.Dump("groups");
var degree = groups.Max(g => g.Count());
degree.Dump("degree");

var degreeElements = groups
    .Where(g => g.Count() == degree)
    .Select(i => i.First());
degreeElements.Dump("degree elements");

var subArrays = degreeElements.Select(i => 
{
    var firstIndex = Array.IndexOf(arr, i);
    var lastIndex = Array.LastIndexOf(arr, i);
    var length = (lastIndex - firstIndex) + 1;
    var subArray = new int[length];
    Array.Copy(arr, firstIndex, subArray, 0, length);
    return subArray;
});
subArrays.Dump("sub-arrays");
subArrays.Min(a => a.Length).Dump("final degree");

/*
    5,1,2,2,3,1 => 2
    6,1,1,2,1,2,2 => 4
    17,802,88,747,23,160,681,254,46,663,752,741,857,802,387,790,528,93 => 13
    
    for more detail about getting a sub-array in C#: https://stackoverflow.com/a/943650/22944
*/