<Query Kind="Statements" />

var year = 2018;
var month = 03;
var register = new Dictionary<byte, double>
{
{1, 79.03},
{3, 73.28},
{7, 123.28},
{9, 120.13},
{12, 111.48},
{19, 104.39},
{22, 98.54},
{27, 148.54},
{28, 128.89},
{31, 125.39},
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