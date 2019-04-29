<Query Kind="Statements">
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>Newtonsoft.Json.Linq</Namespace>
</Query>

var data = new
{
    query = new
    {
        nested = new
        {
            path = "myIndexNestedObject",
            query = new
            {
                match = new JObject
                {
                    { "myIndexNestedObject.myProperty", 999 }
                }
            }
        }
    }
};

var jO = JObject.FromObject(data);
jO.ToString().Dump("jO");