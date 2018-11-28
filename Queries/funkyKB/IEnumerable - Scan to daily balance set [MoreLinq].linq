<Query Kind="Program">
  <NuGetReference>MoreLinq</NuGetReference>
  <Namespace>MoreLinq</Namespace>
</Query>

void Main()
{
    var startingBalance = 121.23d;
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
6/3/2018	(KAW)		Transfer Money		[Schwab High Yield Checking (3810)]		c	50.00
6/9/2018	(KAW)		Spotify		Childcare:Leisure		c	-9.99
6/11/2018	(KAW)		Apple	ITUNES	Childcare:Leisure		c	-7.98
6/12/2018	(KAW)		Transfer Money		[Schwab High Yield Checking (3810)]		c	50.00
6/12/2018	(KAW)		Greenlight		Childcare:Furnishings		c	-4.99
6/15/2018	(KAW)		IN-N-OUT BURGER		Childcare:Furnishings		c	-6.82
6/16/2018	(KAW)		Sephora		Childcare:Furnishings		c	-11.00
";
}