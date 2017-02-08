<Query Kind="Statements">
  <Reference>&lt;RuntimeDirectory&gt;System.Dynamic.dll</Reference>
  <Namespace>System.Dynamic</Namespace>
</Query>

dynamic x = new ExpandoObject();

x.Foo = 123;
int i = x.Foo;
Console.WriteLine (i);

x.Far = DateTime.Now;
DateTime dt = x.Far;
Console.WriteLine (dt);

var dict = (IDictionary<string,object>) x;
dict.Dump();
