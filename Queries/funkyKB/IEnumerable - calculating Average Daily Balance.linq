<Query Kind="Statements" />

var year = 2017;
var month = 9;
var register = new Dictionary<byte, double>
{
    {1, 129.04},
    {5, 122.18},
    {7, 116.58},
    {11, 110.15},
    {12, 103.4},
    {14, 88.47},
    {18, 85.97},
    {20, 79.84},
    {27, 77.54}
};

var registerDays = register.Keys.Select(i => i).ToArray();

if (registerDays.Min() != 1) throw new DataException("The expected opening balance is not here.");

var expandedRegister = Enumerable
    .Range(1, DateTime.DaysInMonth(year, month))
    .ToDictionary(
        i => new DateTime(year, month, i),
        i =>
        {
            var lastRegisteredDay = registerDays.Where(j => i >= j).Max();
            return register[lastRegisteredDay];
        }
    );

var average = expandedRegister.Select(i => i.Value).Average();
Math.Round(average, 2).Dump("average daily balance");
expandedRegister.Dump();