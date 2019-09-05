<Query Kind="Program">
  <NuGetReference>xunit</NuGetReference>
  <Namespace>Xunit</Namespace>
</Query>

void Main()
{
    //Should get the next Saturday day.
    var now = DateTime.Now;
    var selectedDate = DateTime.Today.GetNextWeekday(DayOfWeek.Saturday);
    new
    {
        now,
        selectedDate
    }.Dump();
    
    Assert.True(now.Day < selectedDate.Day, "The expected month day is not here.");
    Assert.True(selectedDate.DayOfWeek == DayOfWeek.Saturday, "The expected week day is not here.");
    Assert.True((selectedDate.Day - now.Day) < 7, "The expected day interval is not here.");
    
    //Should get the specified start/end dates around next Saturday.
    selectedDate = DateTime.Today.GetNextWeekday(DayOfWeek.Saturday);
    var days = (int)365 / 2;
    var startDate = selectedDate.AddDays(-days);
    var endDate = selectedDate.AddDays(days);
    new
    {
        selectedDate,
        days,
        startDate,
        endDate
    }.Dump();

    Assert.True(endDate > startDate, "The expected greater End Date is not here.");
    Assert.True((endDate - startDate).Days == (days * 2), "The expected interval of days is not here.");
}

static partial class DateTimeExtensions
{
    public static DateTime GetNextWeekday(this DateTime start, DayOfWeek day)
    {
        // The (... + 7) % 7 ensures we end up with a value in the range [0, 6]
        int daysToAdd = ((int)day - (int)start.DayOfWeek + 7) % 7;
        return start.AddDays(daysToAdd);
    }
}
