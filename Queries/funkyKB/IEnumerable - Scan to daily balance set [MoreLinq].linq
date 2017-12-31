<Query Kind="Program">
  <NuGetReference>MoreLinq</NuGetReference>
  <Namespace>MoreLinq</Namespace>
</Query>

void Main()
{
    var startingBalance = 91.32d;
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
11/2/2017	KAW		Transfer Money		BDW		c	50
11/2/2017	KAW		Juanita's Cafe		Childcare:Furnishings		c	-4.75
11/3/2017	KAW		**VOID**ELF Cosmetics		Childcare:Leisure		c	0
11/7/2017	KAW		Signature Burger		Childcare:Furnishings		c	-4.37
11/8/2017	KAW		Signature Burger		Childcare:Furnishings		c	-4.37
11/10/2017	KAW		Arclight Culver City		Childcare:Leisure		c	-16.5
11/10/2017	KAW		Chipotle		Childcare:Furnishings		c	-9.72
11/12/2017	KAW		Greenlight		Childcare:Furnishings		c	-4.99
11/28/2017	KAW		Govindas		Childcare:Furnishings		c	-2.25
";
}