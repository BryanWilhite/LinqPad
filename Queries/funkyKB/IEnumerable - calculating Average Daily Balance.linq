<Query Kind="Statements" />

var year = 2018;
var month = 09;
var register = new Dictionary<byte, double>
{
{1, 152.76},
{4, 152.76},
{5, 144.21},
{12, 137.14},
{13, 132.15},
{20, 182.15},
{21, 224.26},
{25, 218.27},
{28, 214.22},
{30, 213.99},
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