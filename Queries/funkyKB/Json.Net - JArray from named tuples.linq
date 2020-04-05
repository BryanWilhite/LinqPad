<Query Kind="Statements">
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>Newtonsoft.Json.Linq</Namespace>
</Query>

IEnumerable<(int id, string name)> data = new[]
{
    (1, "one"),
    (2, "two"),
    (3, "three"),
};

var jA = JArray.FromObject(data);
jA.ToString().Dump(nameof(jA));

/*
   ⚠ the output of `jA` is not expected

   📖 https://github.com/JamesNK/Newtonsoft.Json/issues/1230 
*/
