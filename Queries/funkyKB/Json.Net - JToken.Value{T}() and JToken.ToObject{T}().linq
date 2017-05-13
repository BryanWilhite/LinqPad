<Query Kind="Statements">
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>Newtonsoft.Json.Linq</Namespace>
</Query>

var json = @"
{
    ""item"": {
        ""one"": ""42"",
        ""two"": ""42.3"",
        ""three"": ""2017-03-24T17:28:39.177"",
        ""four"": ""the number four"",
        ""five"": ""true"",
        ""six"": {
            ""uno"": 1,
            ""dos"": 2,
            ""tres"": 3
        }
    }
}
";

var jO = JObject.Parse(json);

jO["item"]["one"].Value<int>().Dump("int");
jO["item"]["two"].Value<double>().Dump("double");
jO["item"]["three"].Value<DateTime>().Dump("DateTime");
jO["item"]["four"].Value<string>().Dump("string");
jO["item"]["five"].Value<bool>().Dump("bool");
jO["item"]["six"].ToObject<Dictionary<string, int>>().Dump("dictionary");
jO["item"]["six"].ToObject<JObject>().Dump("dictionary as JObject");