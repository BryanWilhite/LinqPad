<Query Kind="Statements" />

var year = 2018;
var month = 11;
var register = new Dictionary<byte, double>
{
{1, 195.74},
{3, 186.83},
{9, 176.84},
{12, 171.59},
{13, 166.6},
{18, 160.61},
{28, 210.61},
{30, 355.64},
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