<Query Kind="Program">
  <NuGetReference>MoreLinq</NuGetReference>
  <Namespace>MoreLinq</Namespace>
</Query>

string GetInput()
{
	return @"
3/1/2019	Greenlight Card ()		Spotify		Childcare:Leisure		c	-9.99
3/2/2019	Greenlight Card ()		Transfer Money		[]		c	50.00
3/7/2019	Greenlight Card ()		Apple	ITUNES	Childcare:Leisure		c	-8.97
3/12/2019	Greenlight Card ()		Greenlight		Childcare:Furnishings		c	-4.99
";
}

void Main()
{
	var startingBalance = 313.17d;
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
