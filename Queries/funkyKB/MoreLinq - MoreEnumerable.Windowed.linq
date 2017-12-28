<Query Kind="Statements">
  <NuGetReference>MoreLinq</NuGetReference>
  <Namespace>MoreLinq</Namespace>
</Query>

var numbers = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

numbers.Windowed(2).Dump("windowed by 2");

/*
    projecting windowed by 2 with MoreLinq
    is similar to SelectWithPrevious<TSource, TResult>()
    [https://github.com/BryanWilhite/SonghayCore/blob/master/SonghayCore/Extensions/IEnumerableOfTExtensions.cs]
*/