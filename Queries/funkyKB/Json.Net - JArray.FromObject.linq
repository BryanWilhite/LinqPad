<Query Kind="Program">
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>Newtonsoft.Json.Linq</Namespace>
</Query>

void Main()
{
    var jsonPropertyName = "my-array";

    var data = new[]
    {
        new MyDataClass { One = "1.0", Two = "2.0", Three = "3.0" },
        new MyDataClass { One = "1.1", Two = "2.1", Three = "3.1" },
        new MyDataClass { One = "1.2", Two = "2.2", Three = "3.2" },
    };

    var jA = JArray.FromObject(data);
    var jO = new JObject
    {
        { jsonPropertyName, jA }
    };
    jO.ToString().Dump();
}

class MyDataClass
{
    public string One { get; set; }
    public string Two { get; set; }
    public string Three { get; set; }
}