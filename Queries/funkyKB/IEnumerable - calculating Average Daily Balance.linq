<Query Kind="Statements" />

var year = 2018;
var month = 06;
var register = new Dictionary<byte, double>
{
{1, 121.23},
{3, 121.23},
{9, 171.23},
{11, 161.24},
{12, 203.26},
{15, 198.27},
{16, 191.45},
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