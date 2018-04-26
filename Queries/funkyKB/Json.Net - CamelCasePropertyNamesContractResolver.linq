<Query Kind="Program">
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>Newtonsoft.Json.Linq</Namespace>
  <Namespace>Newtonsoft.Json.Serialization</Namespace>
</Query>

void Main()
{
    var mine = new MyClass
    {
        OneProperty = "One",
        TwoProperty = "Two",
        ThreeProperty = 7
    };

    var serializerSettings = new JsonSerializerSettings
    {
        ContractResolver = new CamelCasePropertyNamesContractResolver(),
        Formatting = Newtonsoft.Json.Formatting.Indented
    };

    var json = JsonConvert.SerializeObject(mine, serializerSettings);
    json.Dump();
}

class MyClass
{
    public string OneProperty { get; set; }
    public string TwoProperty { get; set; }
    public int ThreeProperty { get; set; }
}
