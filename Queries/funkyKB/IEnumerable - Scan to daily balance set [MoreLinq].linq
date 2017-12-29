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
        .Dump("scan");

    var dailyBalance = changes
        .Zip(numbers, (x, y) => new KeyValuePair<byte, double>(x.Key, y))
        .Dump("daily balance");

    var dailyBalanceEOD = dailyBalance
        .GroupBy(i => i.Key)
        .Select(i => new KeyValuePair<byte, double>(i.Key, i.Last().Value))
        .ToDictionary(i => i.Key, i => i.Value)
        .Dump("daily balance (EOD)");
}

string GetInput()
{
    return @"
8/13/2017	KAW		Opening Balance		[KAW]		c	0
8/13/2017	KAW		Transfer Money		[BDW]		c	100
8/13/2017	KAW		Transfer Money		[BDW]		c	50
8/24/2017	KAW		Shein.com	London GBR	Childcare:Furnishings		c	-12
8/29/2017	KAW		Geeker.com		Childcare:Furnishings		c	-0.01
8/29/2017	KAW		Geeker.com		Childcare:Furnishings		c	-7.95
8/29/2017	KAW		Geeker.com		Childcare:Furnishings		c	-1
";
}