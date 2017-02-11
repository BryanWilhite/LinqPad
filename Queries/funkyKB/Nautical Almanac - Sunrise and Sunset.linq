<Query Kind="Statements">
  <Reference Relative="..\..\Content\dlls\NauticalAlmanac\NauticalAlmanac.dll">NauticalAlmanac.dll</Reference>
  <Namespace>NauticalAlmanac</Namespace>
</Query>

var date = DateTime.Now;

("Current time zone: " + TimeZone.CurrentTimeZone.StandardName).Dump();
var daylightChanges = TimeZone.CurrentTimeZone.GetDaylightChanges(date.Year);

// The used latitude and longitude here are for Kortrijk / Belgium
// UtcOffset +1 for Belgium
//lat:SunTime.DegreesToAngle(50, 49, 41), long:SunTime.DegreesToAngle(3, 15, 54)

var latitude = 34.0556; //SunTime.DegreesToAngle(34, 3, 20);
var longitude = 118.4169; //SunTime.DegreesToAngle(118, 25, 0);
var utcOffset = Math.Abs(TimeZone.CurrentTimeZone.GetUtcOffset(date).Hours);
if (TimeZone.IsDaylightSavingTime(date, daylightChanges)) utcOffset++;
var suntime = new SunTime(latitude, longitude, utcOffset, daylightChanges, date);

("Requested sunrise/sunset times for " + date.ToLongDateString()).Dump();
("The sun will rise at " + suntime.RiseTime.ToShortTimeString()).Dump();
("The sun will set at " + suntime.SetTime.ToShortTimeString()).Dump();

//Google search text: `latitude longitude century city`
//=> 34.0556° N [34°, 3', 20"], 118.4169° W [118°, 25', 1"]

/*
    How To Convert a Decimal to Sexagesimal

    You'll often find degrees given in decimal degrees (121.135°) instead of the more common degrees, minutes, and seconds (121°8'6"). However, it's easy to convert from a decimal to the sexagesimal system.

    Difficulty Level: average      Time Required: 5 minutes

    Here's How:

        The whole units of degrees will remain the same (i.e. in 121.135° longitude, start with 121°).
        Multiply the decimal by 60 (i.e. .135 * 60 = 8.1).
        The whole number becomes the minutes (8').
        Take the remaining decimal and multiply by 60. (i.e. .1 * 60 = 6).
        The resulting number becomes the seconds (6"). Seconds can remain as a decimal.
        Take your three sets of numbers and put them together, using the symbols for degrees (°), minutes (‘), and seconds (") (i.e. 121°8'6" longitude)

    [http://geography.about.com/library/howto/htdegrees.htm]
*/