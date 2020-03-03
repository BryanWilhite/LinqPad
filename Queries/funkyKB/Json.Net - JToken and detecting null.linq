<Query Kind="Statements">
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>Newtonsoft.Json.Linq</Namespace>
</Query>

var jO = JObject.Parse(@"{ ""one"": """", ""two"": 1.0, ""three"": null }");

var jT = (JToken)jO["one"];
jT.Type.Dump(jT.Parent.Path);

jT = (JToken)jO["two"];
jT.Type.Dump(jT.Parent.Path);

jT = (JToken)jO["three"];
jT.Type.Dump(jT.Parent.Path); // detect null
(jT.Type == JTokenType.Null).Dump();