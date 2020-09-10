<Query Kind="Statements" />

var start = DateTime.Now.AddMonths(-17);
var end = DateTime.Now;

var years = end.Year - start.Year;
var months = end.Month - start.Month;

years.Dump(nameof(years));
months.Dump(nameof(months));

/*
    I think this is fine for intervals of years and months.

    To get an interval totally in months:
    
    - consider NodaTime: <https://stackoverflow.com/a/32565746/22944>
    - approximate with a StackOverflow answer: <https://stackoverflow.com/a/4639057/22944>

    There is a reason why `System.Timespan` has no `Months` interval.
*/
