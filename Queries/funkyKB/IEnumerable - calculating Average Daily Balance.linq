<Query Kind="Statements" />

var register = new Dictionary<DateTime, double>
{
    { new DateTime(2017, 1, 1), 133.44 },
    { new DateTime(2017, 1, 10), 118.62 },
    { new DateTime(2017, 1, 12), 118.73 },
    { new DateTime(2017, 1, 19), 145.23 },
    { new DateTime(2017, 1, 22), 165.23 },
    { new DateTime(2017, 1, 27), 90.11 }
};

var registerDays = register.Keys.Select(i => i.Day);

if (registerDays.Min() != 1) throw new DataException("The expected opening balance is not here.");

var expandedRegister = Enumerable
    .Range(1, DateTime.DaysInMonth(2017, 1))
    .ToDictionary(
        i => new DateTime(2017, 1, i),
        i =>
        {
            return 0;
        }
    );

expandedRegister.Dump();