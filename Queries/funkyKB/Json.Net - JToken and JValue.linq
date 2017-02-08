<Query Kind="Statements">
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>Newtonsoft.Json.Linq</Namespace>
</Query>

/*
    http://stackoverflow.com/questions/38005957/jtoken-is-not-a-reference-of-jobject/38016360#38016360
*/

var json = @"
{
    ""item"": {
        ""foo"": ""4"",
        ""bar"": ""42""
    }
}
";

var jO = JObject.Parse(json);
var jToken = jO["item"]["foo"];
var jValue = (JValue)jToken;

jToken = "5"; // this is the same as jToken = new JValue("5")
jValue.Value = "600"; // this holds the reference as expected

jO.ToString().Dump("jO");

jToken.Dump("jToken");