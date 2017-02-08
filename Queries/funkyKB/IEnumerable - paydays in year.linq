<Query Kind="Program" />

void Main()
{
    GeneratePaydays().Dump();
}

static IEnumerable<DateTime> GeneratePaydays()
{
    var paydayOne = new DateTime(2017, 1, 13);
    var paydayInterval = 14;
    var d = paydayOne;
    do
    {
        yield return d;
        d = d.AddDays(paydayInterval);
    }
    while (d.Year == paydayOne.Year);
}

/*

    foreach-looping is easily replaced by LINQ extension methods applied to existing enumerables;
    while-looping with yield return allows us to generate new enumerables 

*/