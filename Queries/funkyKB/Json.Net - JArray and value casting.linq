<Query Kind="Statements">
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>Newtonsoft.Json.Linq</Namespace>
</Query>

var jsonPropertyName = "my-array";

var data = new[]
{
        1.0d,
        1.1d,
        1.2d
    };

var jA = JArray.FromObject(data);
var jO = new JObject
    {
        { jsonPropertyName, jA }
    };
jO.ToString().Dump("To JArray");

var array = jA.Select(jT => (double)jT).ToArray();
array.Dump("To value Array");
