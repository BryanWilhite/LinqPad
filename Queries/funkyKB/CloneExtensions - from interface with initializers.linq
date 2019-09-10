<Query Kind="Program">
  <NuGetReference>CloneExtensions</NuGetReference>
  <Namespace>CloneExtensions</Namespace>
</Query>

void Main()
{
    var original = new MyFour { Two = "2", Four = "4" };
    original.GetClone().Dump("clone");

    var initializers = new Dictionary<Type, Func<object, object>>() {
        { typeof(IMyThree), o => new MyFour() }
    };

    var interfaceInstance = original as IMyThree;
    interfaceInstance
        .GetClone(initializers)
        .Dump("clone via interface");
}

interface IMyThree
{
    string One { get; set; }
    string Two { get; set; }
    string Three {get; set;}
}

class MyFour: IMyThree
{
    public string One { get; set; }
    public string Two { get; set; }
    public string Three { get; set; }
    public string Four { get; set; }
}
