<Query Kind="Program">
  <NuGetReference>MoreLinq</NuGetReference>
  <Namespace>MoreLinq</Namespace>
</Query>

void Main()
{
    var changes = GetInput()
        .Trim()
        .Split('\n')
        .Select(i =>
        {
            var entry = i.Trim(new[] { '\r', '\t' }).Split('\t');
            var day = Convert.ToDateTime(entry.First()).Day;
            var change = Convert.ToDouble(entry.Last());
            return new KeyValuePair<byte, double>(Convert.ToByte(day), change);
        });

    var numbers = changes
        .Select(i => i.Value)
        .Scan((aggregation, current) => aggregation + current)
        .Dump("scan of changes");

    var dailyBalance = changes
        .Zip(numbers, (x, y) => new KeyValuePair<byte, double>(x.Key, y))
        .Dump("daily balance");

    var dailyBalanceEOD = dailyBalance
        .GroupBy(i => i.Key)
        .Select(i => new KeyValuePair<byte, double>(i.Key, i.Last().Value))
        .ToDictionary(i => i.Key, i => i.Value)
        .Dump("daily balance (EOD)");

    dailyBalanceEOD.ForEach(i => $@"{{{i.Key}, {i.Value}}},".Dump());
}

string GetInput()
{
    return @"
9/5/2017	KAW		Sprouts		Childcare:Furnishings		c	-6.86
9/5/2017	KAW		Menchies		Childcare:Furnishings		c	-4.73
9/7/2017	KAW		Apple		Childcare:Leisure		c	-0.87
9/7/2017	KAW		TST*Z Pastabar		Childcare:Furnishings		c	-6.43
9/11/2017	KAW		Rave		Childcare:Leisure		c	-6.75
9/12/2017	KAW		Greenlight		Childcare:Furnishings		c	-4.99
9/14/2017	KAW		Apple		Childcare:Furnishings		c	-9.94
9/14/2017	KAW		District Market		Childcare:Furnishings		c	-2.5
9/18/2017	KAW		Sprouts		Childcare:Furnishings		c	-6.13
9/20/2017	KAW		Pieology		Childcare:Leisure		c	-2.3
9/27/2017	KAW		Grand Casino Bakery		Childcare:Furnishings		c	-4.4
";
}