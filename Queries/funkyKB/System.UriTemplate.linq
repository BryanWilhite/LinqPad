<Query Kind="Statements">
  <NuGetReference>System.ServiceModel.Primitives</NuGetReference>
</Query>

//https://msdn.microsoft.com/en-us/library/system.uritemplate(v=vs.110).aspx

var template = new UriTemplate("weather/{state}/{city}?forecast={day}");
var prefix = new Uri("http://localhost");

template.PathSegmentVariableNames.ToArray().Dump("PathSegmentVariableNames");
template.QueryValueVariableNames.ToArray().Dump("QueryValueVariableNames");

var uri = template.BindByPosition(prefix, "Washington", "Redmond", "Today");
uri.Dump("BindByPosition");

uri = template.BindByName(prefix, new Dictionary<string, string>
{
    { "state", "Washington" },
    { "city", "Redmond" },
    { "day", "Today" },
});
uri.Dump("BindByName");