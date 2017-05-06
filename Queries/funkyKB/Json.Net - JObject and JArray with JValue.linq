<Query Kind="Statements">
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>Newtonsoft.Json.Linq</Namespace>
</Query>

var rootPropertyName = "root";
var jO = JObject.Parse($@"{{ ""{rootPropertyName}"": [] }}");
jO.ToString().Dump("JObject");

var jA = (JArray)jO[rootPropertyName];
jA.Add("one");
jA.Add("two");
jA.Add("two");
jA.Add("three");
jO.ToString().Dump("JArray with strings added");

jA.OfType<string>().Dump("what?");

jA.OfType<JValue>().Dump("oh, there they are :)");

jA.OfType<JValue>().Distinct().Dump("distinctly");