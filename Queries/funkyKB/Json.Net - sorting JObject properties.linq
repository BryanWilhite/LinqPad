<Query Kind="Statements">
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>Newtonsoft.Json.Linq</Namespace>
</Query>

var data = new
{
    PropertyC = "three",
    PropertyD = "four",
    PropertyB = "two",
    PropertyA = "one",
};

var jO = JObject.FromObject(data);
jO.ToString().Dump("sorted");

var jProperties = jO.Properties();
jProperties.Dump("IEnumerable<jProperty>");

jO = new JObject(jProperties.OrderBy(i => i.Name));
jO.ToString().Dump("sorted");