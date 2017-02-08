<Query Kind="Statements">
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>Newtonsoft.Json</Namespace>
</Query>

/*
    “JSON.net and Deserializing Anonymous Types”
    http://geekswithblogs.net/DavidHoerster/archive/2012/01/06/json.net-and-deserializing-anonymous-types.aspx
*/
var json = @"
[
    { ""id"": 100, ""typeID"": 4 },
    { ""id"": 101, ""typeID"": 3 }
]";

var definition = new[] { new { id = 0, typeID = 0 } };
var data = JsonConvert.DeserializeAnonymousType(json, definition);

data.Dump();
