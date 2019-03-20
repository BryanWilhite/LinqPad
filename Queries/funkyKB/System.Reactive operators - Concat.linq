<Query Kind="Statements">
  <Reference>&lt;RuntimeDirectory&gt;\System.Runtime.dll</Reference>
  <NuGetReference>MoreLinq</NuGetReference>
  <NuGetReference>System.Reactive</NuGetReference>
  <Namespace>MoreLinq</Namespace>
  <Namespace>System.Reactive.Subjects</Namespace>
  <Namespace>System.Reactive.Linq</Namespace>
</Query>

var observableNow = Observable
    .Interval(TimeSpan.FromMilliseconds(250))
    .Select(o => 1)
    .Take(5);

var observableLater = Observable
    .Interval(TimeSpan.FromMilliseconds(500))
    .Select(o => 2)
    .Take(5);

observableNow
    .Concat(observableLater)
    .Subscribe(i => i.Dump());