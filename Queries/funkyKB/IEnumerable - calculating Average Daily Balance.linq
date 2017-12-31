<Query Kind="Statements" />

var year = 2017;
var month = 11;
var register = new Dictionary<byte, double>
{
    {1, 141.32},
    {2, 141.32},
    {3, 136.57},
    {7, 136.57},
    {8, 132.2},
    {10, 111.33},
    {12, 101.61},
    {28, 96.62}
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