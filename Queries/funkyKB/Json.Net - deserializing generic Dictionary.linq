<Query Kind="Statements">
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>Newtonsoft.Json.Linq</Namespace>
</Query>

var json = @"{}";
var data = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
data.Dump("empty Dictionary");

json = @"{ ""one"":""uno"", ""two"":""dos"" }";
data = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
data.Dump("Dictionary<string, string>");

json = @"{ ""one"":[""uno"", ""un""], ""two"":[""dos"", ""deux""] }";
var data2 = JsonConvert.DeserializeObject<Dictionary<string, string[]>>(json);
data2.Dump("Dictionary<string, string[]>");

var jO = JObject.Parse(json);
jO.ToObject<Dictionary<string, string[]>>().Dump("Dictionary<string, string[]>");

json = @"{ ""set"": { ""one"":""uno"", ""two"":""dos"" } }";
jO = JObject.Parse(json);
jO["set"].ToObject<Dictionary<string, string>>().Dump("Dictionary<string, string[]>");

json = @"{ 1:""uno"", 2:""dos"" }";
var data3 = JsonConvert.DeserializeObject<Dictionary<int, string>>(json);
data3.Dump("Dictionary<int, string>");
