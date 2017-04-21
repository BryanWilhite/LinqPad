<Query Kind="Statements">
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>Newtonsoft.Json.Linq</Namespace>
</Query>

/*
    “From Json.NET 4.5 and onwards dates are written using the ISO 8601 format by default,
    and using this converter [IsoDateTimeConverter] is unnecessary.”
    [http://www.newtonsoft.com/json/help/html/DatesInJSON.htm]
*/

var json = @"
{
    ""item"": {
        ""myDate"": null,
        ""foo"": ""BAR""
    }
}
";

var jO = JObject.Parse(json);
jO["item"]["myDate"] = DateTime.Now;

jO.ToString().Dump("jO");