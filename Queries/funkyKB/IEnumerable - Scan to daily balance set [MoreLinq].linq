<Query Kind="Program">
  <NuGetReference>MoreLinq</NuGetReference>
  <Namespace>MoreLinq</Namespace>
</Query>

void Main()
{
    var startingBalance = 77.54d;
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
        .Scan(startingBalance, (aggregation, current) => aggregation + current)
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
10/2/2017	KAW		Sprouts		Childcare:Furnishings		c	-1.99
10/3/2017	KAW		Govindas		Childcare:Furnishings		c	-9
10/3/2017	KAW		Govindas		Childcare:Furnishings		c	-2.5
10/4/2017	KAW		Govindas		Childcare:Furnishings		c	-4.65
10/12/2017	KAW		Greenlight		Childcare:Furnishings		c	-4.99
10/13/2017	KAW		Transfer Money		BDW		c	50
10/23/2017	KAW		Star Mini Mart		Childcare:Furnishings		c	-5.72
10/25/2017	KAW		Chipotle		Childcare:Furnishings		c	-7.37
10/26/2017	KAW		Shell Calabasas		Childcare:Furnishings		c	-4.33
";
}