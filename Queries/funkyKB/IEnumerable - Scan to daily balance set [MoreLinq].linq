<Query Kind="Program">
  <NuGetReference>MoreLinq</NuGetReference>
  <Namespace>MoreLinq</Namespace>
</Query>

void Main()
{
    var startingBalance = 100.86d;
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
2/1/2018	(KAW)		Ulta		Childcare:Furnishings			-10.06
2/3/2018	(KAW)		Spotify		Childcare:Furnishings			-9.99
2/11/2018	(KAW)		CVS		Childcare:Furnishings			-9.84
2/12/2018	(KAW)		Greenlight		Childcare:Furnishings			-4.99
2/15/2018	(KAW)		Alices Shop		Childcare:Furnishings			-12.02
2/16/2018	(KAW)		Transfer Money		(BDW)			50.00
2/28/2018	(KAW)		Spotify		Childcare:Furnishings			-9.99
2/28/2018	(KAW)		Amazon.com		Childcare:Furnishings			-12.95
";
}