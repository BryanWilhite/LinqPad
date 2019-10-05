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

jO = new JObject(jProperties.Select(i =>
	{
		var propertyName = string.Concat(i.Name.Substring(0,1).ToLower(), i.Name.Substring(1));
		return new JProperty(propertyName, i.Value);
	}));
jO.ToString().Dump("camel case");