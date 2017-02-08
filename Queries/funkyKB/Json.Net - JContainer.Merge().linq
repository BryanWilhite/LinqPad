<Query Kind="Statements">
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>Newtonsoft.Json.Linq</Namespace>
</Query>

var json1 = @"
{
    ""item"": {
        ""foo"": ""4"",
    }
}
";

var json2 = @"
{
    ""item"": {
        ""bar"": ""42""
    }
}
";

var jO1 = JObject.Parse(json1);
var jO2 = JObject.Parse(json2);

jO1.Merge(jO2);

jO1.ToString().Dump();