<Query Kind="Program">
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>Newtonsoft.Json.Linq</Namespace>
</Query>

void Main()
{
    var rootPropertyName = "root";
    var jO = JObject.Parse($@"{{ ""{rootPropertyName}"": [] }}");
    jO.ToString().Dump();
    
    var jA = (JArray)jO[rootPropertyName];
    jA.Add(JObject.Parse(@"{ ""x"":""one"" }"));
    jA.Add(JObject.Parse(@"{ ""x"":""two"" }"));
    jA.Add(JObject.Parse(@"{ ""x"":""three"" }"));

    jO.ToString().Dump();
}
