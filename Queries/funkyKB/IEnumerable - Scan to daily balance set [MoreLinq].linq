<Query Kind="Program">
  <NuGetReference>MoreLinq</NuGetReference>
  <Namespace>MoreLinq</Namespace>
</Query>

string GetInput()
{
	return @"
7/1/2019	Greenlight Card ()		Transfer Money		[]		c	50.00
7/1/2019	Greenlight Card ()		Apple	ITUNES	Childcare:Leisure		c	-19.99
7/3/2019	Greenlight Card ()		Spotify		Childcare:Leisure		c	-9.99
7/7/2019	Greenlight Card ()		Apple	ITUNES	Childcare:Leisure		c	-5.99
7/12/2019	Greenlight Card ()		Star Mini Mart		Childcare:Furnishings		c	-4.04
7/12/2019	Greenlight Card ()		Greenlight		Childcare:Furnishings		c	-4.99
7/20/2019	Greenlight Card ()		CVS		Childcare:Leisure		c	-2.62
";
}

void Main()
{
	var startingBalance = 358.74d;
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