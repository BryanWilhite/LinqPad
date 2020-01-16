<Query Kind="Statements" />

// this is an old-school reporting-header trick
var set = new[] { 1, 1, 1, 2, 2, 3, 4, 4, 5 };

var i = 0;
foreach (var item in set)
{
	if(item != i) "change!".Dump();
	item.Dump(nameof(item));
	i = item;
}