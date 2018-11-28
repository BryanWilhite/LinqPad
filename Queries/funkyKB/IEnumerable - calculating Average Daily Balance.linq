<Query Kind="Statements" />

var year = 2018;
var month = 04;
var register = new Dictionary<byte, double>
{
{1, 101.74},
{3, 101.74},
{8, 141.74},
{12, 133.75},
{21, 128.76},
{26, 178.76},
{27, 173.27},
{30, 168.94},
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