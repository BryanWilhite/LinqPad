<Query Kind="Program">
  <NuGetReference>MoreLinq</NuGetReference>
  <Namespace>MoreLinq</Namespace>
</Query>

void Main()
{
    var startingBalance = 204.00d;
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
10/3/2018	(KAW)		National/Robertson Car		Childcare:Furnishings		c	-3.08
10/5/2018	(KAW)		Ulta		Childcare:Furnishings		c	-19.21
10/5/2018	(KAW)		Govindas		Childcare:Furnishings		c	-6.30
10/7/2018	(KAW)		Transfer Money		[Schwab High Yield Checking (3810)]		c	50.00
10/9/2018	(KAW)		Govindas		Childcare:Leisure		c	-3.15
10/12/2018	(KAW)		National/Robertson Car		Childcare:Furnishings		c	-2.88
10/12/2018	(KAW)		Greenlight		Childcare:Furnishings			-4.99
10/16/2018	(KAW)		Google *YouTube		Childcare:Leisure		c	-6.99
10/18/2018	(KAW)		National/Robertson Car		Childcare:Furnishings		c	-5.67
10/21/2018	(KAW)		Apple	ITUNES	Childcare:Leisure		c	-5.99
";
}