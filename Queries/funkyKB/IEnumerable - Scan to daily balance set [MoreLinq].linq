<Query Kind="Program">
  <NuGetReference>MoreLinq</NuGetReference>
  <Namespace>MoreLinq</Namespace>
</Query>

void Main()
{
    var startingBalance = 165.47d;
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
8/2/2018	(KAW)		Apple	ITUNES	Childcare:Leisure		c	-5.99
8/2/2018	(KAW)		Spotify		Childcare:Leisure		c	-9.99
8/9/2018	(KAW)		Transfer Money		[Schwab High Yield Checking (3810)]		c	50.00
8/12/2018	(KAW)		Rite Aid		Childcare:Furnishings		c	-7.07
8/12/2018	(KAW)		Greenlight		Childcare:Furnishings			-4.99
8/13/2018	(KAW)		The Green Store		Childcare:Furnishings		c	-11.49
8/21/2018	(KAW)		Apple	ITUNES	Childcare:Leisure		c	-5.99
8/22/2018	(KAW)		Govindas		Childcare:Furnishings		c	-7.20
8/31/2018	(KAW)		Spotify		Childcare:Leisure		c	-9.99
";
}