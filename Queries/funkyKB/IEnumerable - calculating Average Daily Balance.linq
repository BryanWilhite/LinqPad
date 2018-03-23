<Query Kind="Statements" />

var year = 2017;
var month = 11;
var register = new Dictionary<byte, double>
{
{1, 100.86},
{3, 90.8},
{11, 80.81},
{12, 70.97},
{15, 65.98},
{16, 53.96},
{28, 93.97},
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