<Query Kind="Statements" />

var year = 2019;
var month = 6;
var register = new Dictionary<byte, double>
{
{1, 349.64},
{2, 329.65},
{3, 379.65},
{7, 369.66},
{12, 363.73},
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