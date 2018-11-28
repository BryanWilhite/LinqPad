<Query Kind="Program">
  <NuGetReference>MoreLinq</NuGetReference>
  <Namespace>MoreLinq</Namespace>
</Query>

void Main()
{
    var startingBalance = 81.02d;
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
3/1/2018	(KAW)		Dylan's Candy Bar-12		Childcare:Furnishings			-1.99
3/1/2018	(KAW)		The Salad Bar		Childcare:Furnishings			-5.75
3/3/2018	(KAW)		Transfer Money		[Schwab High Yield Checking (3810)]		c	50.00
3/7/2018	(KAW)		Govindas		Childcare:Furnishings		c	-3.15
3/9/2018	(KAW)		CVS				c	-8.65
3/12/2018	(KAW)		Greenlight		Childcare:Furnishings		c	-4.99
3/19/2018	(KAW)		Star Mini Mart		Childcare:Furnishings		c	-2.10
3/19/2018	(KAW)		Govindas		Childcare:Furnishings		c	-5.85
3/22/2018	(KAW)		Transfer Money		[Schwab High Yield Checking (3810)]		c	50.00
3/27/2018	(KAW)		Buffalo Wild Wings		Childcare:Furnishings		c	-7.65
3/28/2018	(KAW)		ArcLight		Childcare:Leisure		c	-12.00
3/28/2018	(KAW)		Metro Downtown Santa Monica		Childcare:Furnishings		c	-3.50
3/31/2018	(KAW)		Spotify		Childcare:Leisure		c	-9.99
";
}