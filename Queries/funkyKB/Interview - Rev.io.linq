<Query Kind="Statements" />

/*
    I did pretty well on their Hacker Rank test.
    The only issue that authentically concerns me was my inability
    to think about ‘decimal’ months under a ceremonial stopwatch:
    
    2020/09/15 to 2020/11/30 is approximately .5 + 1 + 1 months
*/

var startDate = new DateTime(2020, 9, 15);
var endDate = new DateTime(2020, 11, 30);

var count = (endDate.Month - startDate.Month);
var months = Enumerable
    .Range(startDate.Month, count + 1)
    .Select(m =>
    {
        decimal fractionOfMonth = 0m;
        if(m == startDate.Month)
        {
            var daysInMonth = DateTime.DaysInMonth(startDate.Year, startDate.Month);
            fractionOfMonth = startDate.Day / (decimal)daysInMonth;
        }
        else if (m == endDate.Month)
        {
            var daysInMonth = DateTime.DaysInMonth(endDate.Year, endDate.Month);
            fractionOfMonth = endDate.Day / (decimal)daysInMonth;
        }
        else
        {
            fractionOfMonth = 1;
        }

        return fractionOfMonth;
    })
    .Sum(m => m);

months.Dump(nameof(months));