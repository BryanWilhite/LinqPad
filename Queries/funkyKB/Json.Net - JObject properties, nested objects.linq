<Query Kind="Statements">
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>Newtonsoft.Json.Linq</Namespace>
</Query>

var data = new JObject
{
    { "PropertyA", "one" },
    { "PropertyB", "two" },
    { "Nested", new JObject
        {
            { "PropertyD", "four" }
        }
    }
};

var jO = JObject.FromObject(data);
jO.ToString().Dump("jO");
