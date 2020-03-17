<Query Kind="Statements">
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>Newtonsoft.Json.Linq</Namespace>
</Query>

var jsonArray = new []
{
    @"{ ""one"": 1 }",
    @"{ ""two"": 2 }",
    @"{ ""three"": 3 }",
    @"{ ""four"": 4 }",
};

var jA = JArray.FromObject(jsonArray.Select(json => JObject.Parse(json)));

var jO = jA[2].Value<JObject>();

jO.Dump();
