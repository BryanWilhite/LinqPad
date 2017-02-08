<Query Kind="Statements" />

//Should get the next Saturday day.
var now = DateTime.Now;
var selectedDate = DateTime.Today.GetNextWeekday(DayOfWeek.Saturday);

Assert.IsTrue(now.Day < selectedDate.Day, "The expected month day is not here.");
Assert.IsTrue(selectedDate.DayOfWeek == DayOfWeek.Saturday, "The expected week day is not here.");
Assert.IsTrue((selectedDate.Day - now.Day) < 7, "The expected day interval is not here.");

//Should get the specified start/end dates around next Saturday.
var selectedDate = DateTime.Today.GetNextWeekday(DayOfWeek.Saturday);
var days = (int)365 / 2;
var startDate = selectedDate.AddDays(-days);
var endDate = selectedDate.AddDays(days);

Assert.IsTrue(endDate > startDate, "The expected greater End Date is not here.");
Assert.IsTrue((endDate - startDate).Days == (days * 2), "The expected interval of days is not here.");
