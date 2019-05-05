<Query Kind="Program">
  <NuGetReference>Unity</NuGetReference>
  <Namespace>Unity</Namespace>
</Query>

/*
    The Unity Container can resolve an instance
    without previous registration.
*/
void Main()
{
    var container = new UnityContainer();
    var instance = container.Resolve<MyClass>();
    instance.MyMethod().Dump();
}

interface IMyInterface {
    string MyMethod();
}

class MyClass : IMyInterface {
    public string MyMethod() => "Hello world!";
}
