<Query Kind="Statements" />

var year = 2018;
var month = 08;
var register = new Dictionary<byte, double>
{
{1, 159.48},
{2, 159.48},
{9, 149.49},
{12, 192.42},
{13, 187.43},
{21, 175.94},
{22, 169.95},
{31, 162.75},
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