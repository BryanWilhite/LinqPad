<Query Kind="Statements">
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>Newtonsoft.Json.Linq</Namespace>
</Query>

var data = new JObject
{
    { "PropertyA", "one" },
    { "PropertyB", "two" },
    { "PropertyC", "three" },
    { "PropertyD", "four" }
};

var jO = JObject.FromObject(data);
jO.ToString().Dump("jO");

jO.Remove("PropertyC");
jO.ToString().Dump("removed PropertyC");

jO.Add("PropertyF", "six");
jO.ToString().Dump("added PropertyF");