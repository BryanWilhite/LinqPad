<Query Kind="Statements">
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>Newtonsoft.Json.Linq</Namespace>
</Query>

var json = @"
{
    ""item"": {
        ""foo"": ""4"",
    }
}
";

var jO1 = JObject.Parse(json);
var jA = new JArray();

jA.Add(JObject.Parse(@"{ ""x"": 2, ""y"": 7 }"));
jA.Add(JObject.Parse(@"{ ""x"": 45, ""y"": 89 }"));
jO1.Add("items", jA);

jO1.ToString().Dump();