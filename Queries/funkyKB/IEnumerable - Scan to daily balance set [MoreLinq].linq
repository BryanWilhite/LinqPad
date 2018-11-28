<Query Kind="Program">
  <NuGetReference>MoreLinq</NuGetReference>
  <Namespace>MoreLinq</Namespace>
</Query>

void Main()
{
    var startingBalance = 152.76d;
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
9/4/2018	(KAW)		Star Mini Mart		Childcare:Furnishings		c	-8.55
9/5/2018	(KAW)		Star Mini Mart		Childcare:Furnishings		c	-7.07
9/12/2018	(KAW)		Greenlight		Childcare:Furnishings			-4.99
9/13/2018	(KAW)		Transfer Money		[Schwab High Yield Checking (3810)]		c	50.00
9/20/2018	(KAW)		India Sweets & Spices		Childcare:Furnishings		c	-7.89
9/21/2018	(KAW)		Transfer Money		[Schwab High Yield Checking (3810)]		c	50.00
9/21/2018	(KAW)		Apple	ITUNES	Childcare:Leisure		c	-5.99
9/25/2018	(KAW)		Govindas		Childcare:Furnishings		c	-4.05
9/28/2018	(KAW)		Idyllwild Camp		Other Income:Reversal of Charges		c	25.00
9/30/2018	(KAW)		India Sweets & Spices		Childcare:Furnishings		c	-2.73
9/30/2018	(KAW)		India Sweets & Spices		Childcare:Furnishings		c	-22.50
9/30/2018	(KAW)		Spotify		Childcare:Leisure		c	-9.99
";
}