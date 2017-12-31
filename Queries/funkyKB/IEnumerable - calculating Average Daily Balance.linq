<Query Kind="Statements" />

var year = 2017;
var month = 10;
var register = new Dictionary<byte, double>
{
    {1, 77.54},
    {2, 77.54},
    {3, 66.55},
    {4, 64.05},
    {12, 59.4},
    {13, 54.41},
    {23, 104.41},
    {25, 98.69},
    {26, 91.32}
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