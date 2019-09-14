<Query Kind="Program">
  <NuGetReference>MoreLinq</NuGetReference>
  <Namespace>MoreLinq</Namespace>
</Query>

void Main()
{
	var startingBalance = 311.87d;
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
2/1/2019	Greenlight Card ()		Apple	ITUNES	Childcare:Leisure		c	-5.99
2/3/2019	Greenlight Card ()		Spotify		Childcare:Leisure		c	-9.99
2/12/2019	Greenlight Card ()		Transfer Money		[]		c	50.00
2/12/2019	Greenlight Card ()		Greenlight		Childcare:Furnishings		c	-4.99
2/15/2019	Greenlight Card ()		Mod Pizza		Childcare:Furnishings		c	-11.76
2/21/2019	Greenlight Card ()		Apple	ITUNES	Childcare:Leisure		c	-15.97
";
}