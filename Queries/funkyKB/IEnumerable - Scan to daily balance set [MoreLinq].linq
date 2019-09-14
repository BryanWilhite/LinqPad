<Query Kind="Program">
  <NuGetReference>MoreLinq</NuGetReference>
  <Namespace>MoreLinq</Namespace>
</Query>

void Main()
{
	var startingBalance = 297.23d;
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
1/3/2019	Greenlight Card ()		Cinemark Theaters		Childcare:Leisure		c	-11.00
1/3/2019	Greenlight Card ()		Cinemark Theaters		Childcare:Leisure		c	-9.00
1/4/2019	Greenlight Card ()		Transfer Money		[]		c	50.00
1/7/2019	Greenlight Card ()		Apple	ITUNES	Childcare:Leisure		c	-5.99
1/9/2019	Greenlight Card ()		Wendy's		Childcare:Furnishings		c	-4.38
1/12/2019	Greenlight Card ()		Greenlight		Childcare:Furnishings		c	-4.99
";
}