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
12/3/2018	Greenlight Card ()		Google (YouTube)		Childcare:Leisure		c	-3.99
12/4/2018	Greenlight Card ()		Trader Joes		Childcare:Furnishings		c	-7.98
12/5/2018	Greenlight Card ()		**VOID**Amazon.com	appears to be a marketplace credit?	Childcare:Cash Support		c	0.00
12/6/2018	Greenlight Card ()		Starbucks		Childcare:Furnishings		c	-2.50
12/6/2018	Greenlight Card ()		Noah's Bagels		Childcare:Furnishings		c	-7.99
12/12/2018	Greenlight Card ()		Greenlight		Childcare:Furnishings		c	-4.99
12/12/2018	Greenlight Card ()		Apple	ITUNES	Childcare:Leisure		c	-10.98
12/31/2018	Greenlight Card ()		Spotify		Childcare:Leisure		c	-9.99
";
}