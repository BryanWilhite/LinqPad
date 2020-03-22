<Query Kind="Program">
  <Namespace>Newtonsoft.Json.Linq</Namespace>
</Query>

void Main()
{
    GetInput("MyProcedure", "uno").ToString().Dump();
    GetInput("MyProcedure", new JObject { { "one", "uno" } }).ToString().Dump();
    GetInput("MyProcedure", new { one = "uno" }).ToString().Dump();
    GetInput("MyProcedure", (new Dictionary<string, object> { { "one", new { three = 3.0 } } })).ToString().Dump();
    GetInput("MyProcedure", new KeyValuePair<string, string>("one", "uno")).ToString().Dump();
}

public static JObject GetInput(string procedureName, params object[] input)
{
    return JObject.FromObject(new { procedureName, input });
}