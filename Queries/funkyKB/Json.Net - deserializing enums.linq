<Query Kind="Program">
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>Newtonsoft.Json</Namespace>
</Query>

void Main()
{
    var json = @"
{
    ""MyDataRowVersion"":""Default"", 
    ""DbType"":""String"", 
    ""ParameterDirection"":""Output"", 
}
";

    var data = JsonConvert.DeserializeObject<MyData>(json);
    data.Dump();
}

class MyData
{
    public DataRowVersion MyDataRowVersion{ get; set; }

    public DbType DbType { get; set; }

    public ParameterDirection ParameterDirection { get; set; }
}