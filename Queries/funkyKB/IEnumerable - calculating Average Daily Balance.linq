<Query Kind="Statements" />

var year = 2018;
var month = 10;
var register = new Dictionary<byte, double>
{
{1, 204},
{3, 204},
{5, 181.71},
{7, 175.41},
{9, 225.41},
{12, 219.38},
{16, 214.39},
{18, 207.4},
{21, 201.73},
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