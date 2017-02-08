<Query Kind="Statements" />

var month = 1;
var year = 2017;
var paydayOne = 13;
var lastDayOfMonth = DateTime.DaysInMonth(year, month);
var paydayInterval = 14;

var paydays = Enumerable
    .Range(paydayOne, (lastDayOfMonth - paydayOne))
    .Where((d, i) => d == paydayOne || i % paydayInterval == 0)
    .Select(i => new DateTime(year, month, i))
    .Dump();