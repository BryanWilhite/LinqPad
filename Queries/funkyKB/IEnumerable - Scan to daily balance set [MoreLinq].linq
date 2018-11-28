<Query Kind="Program">
  <NuGetReference>MoreLinq</NuGetReference>
  <Namespace>MoreLinq</Namespace>
</Query>

void Main()
{
    var startingBalance = 115.40d;
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
4/3/2018	(KAW)		Etsy		Childcare:Furnishings		c	-13.66
4/3/2018	(KAW)		Amazon.com		Childcare:Furnishings		c	-10.00
4/8/2018	(KAW)		Transfer Money		[Schwab High Yield Checking (3810)]		c	50.00
4/8/2018	(KAW)		Amazon.com		Childcare:Furnishings		c	-7.99
4/12/2018	(KAW)		Greenlight		Childcare:Furnishings		c	-4.99
4/21/2018	(KAW)		Transfer Money		[Schwab High Yield Checking (3810)]		c	50.00
4/26/2018	(KAW)		Sprouts		Childcare:Furnishings		c	-5.49
4/27/2018	(KAW)		The Vegan Joint		Childcare:Furnishings		c	-4.33
4/30/2018	(KAW)		Spotify		Childcare:Leisure		c	-9.99
";
}