<Query Kind="Statements" />

var year = 2017;
var month = 1;
var register = new Dictionary<byte, double>
{
    { 1, 133.44 },
    { 10, 118.62 },
    { 12, 118.73 },
    { 19, 145.23 },
    { 22, 165.23 },
    { 27, 90.11 }
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