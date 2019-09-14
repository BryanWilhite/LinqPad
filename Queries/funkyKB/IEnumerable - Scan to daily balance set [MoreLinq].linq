<Query Kind="Program">
  <NuGetReference>MoreLinq</NuGetReference>
  <Namespace>MoreLinq</Namespace>
</Query>

void Main()
{
    var startingBalance = 195.74d;
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
11/1/2018	Greenlight Card ()		Apple	ITUNES	Childcare:Leisure		c	-8.91
11/3/2018	Greenlight Card ()		Spotify		Childcare:Leisure		c	-9.99
11/9/2018	Greenlight Card ()		CVS		Childcare:Furnishings		c	-3.25
11/12/2018	Greenlight Card ()		City of Santa Monica	beach parking	Childcare:Furnishings		c	-2.00
11/12/2018	Greenlight Card ()		Greenlight		Childcare:Furnishings			-4.99
11/13/2018	Greenlight Card ()		Apple	ITUNES	Childcare:Leisure		c	-5.99
11/18/2018	Greenlight Card ()		Transfer Money		[]			50.00
11/28/2018	Greenlight Card ()		Transfer Money		[]		c	150.00
11/30/2018	Greenlight Card ()		National/Robertson Car		Childcare:Furnishings		c	-4.97
11/30/2018	Greenlight Card ()		Spotify		Childcare:Leisure		c	-9.99
";
}