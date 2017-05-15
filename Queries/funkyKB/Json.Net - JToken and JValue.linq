<Query Kind="Statements">
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>Newtonsoft.Json.Linq</Namespace>
</Query>

/*
    For background, see “JToken is not a reference of JObject?”
    [http://stackoverflow.com/questions/38005957/jtoken-is-not-a-reference-of-jobject/38016360#38016360]
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
var jT = jO["item"]["foo"];
var jV = (JValue)jT;

jT = "5"; // this is the same as jT = new JValue("5")
jV.Value = "600"; // this holds the reference as expected

jO.ToString().Dump("jO with indirect reference set");

jT.Dump("jT");

jO["item"]["foo"] = "700";

jO.ToString().Dump("jO with direct set");