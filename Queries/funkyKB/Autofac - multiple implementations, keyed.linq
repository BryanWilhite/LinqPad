<Query Kind="Program">
  <NuGetReference>Autofac</NuGetReference>
  <NuGetReference>Autofac.Extras.Attributed</NuGetReference>
  <Namespace>Autofac</Namespace>
  <Namespace>Autofac.Extras.Attributed</Namespace>
</Query>

/*
    “Named and Keyed Services”
    [http://autofac.readthedocs.org/en/latest/advanced/keyed-services.html]
*/

const string A = "a";
const string B = "b";
const string MyApp = "MyApp";

void Main()
{
    var builder = new ContainerBuilder();
    builder
        .RegisterType<MyClassA>()
        .As<IMyInterface>()
        .InstancePerLifetimeScope()
        .Keyed<IMyInterface>(A);
    builder
        .RegisterType<MyClassB>()
        .As<IMyInterface>()
        .InstancePerLifetimeScope()
        .Keyed<IMyInterface>(B);
    builder
        .RegisterType<MyAppDomain>()
        .Named<MyAppDomain>(MyApp)
        .WithAttributeFilter();
/*
    for details on WithAttributeFilter() see “WithKey Attribute”
    [http://autofac.readthedocs.org/en/latest/advanced/metadata.html?highlight=withkey]
*/

    var container = builder.Build();

    var instance = container.ResolveKeyed<IMyInterface>(A);
    instance.AddTheNumbers().Dump();
    
    var myApp = container.ResolveNamed<MyAppDomain>(MyApp);
    myApp.Dump();
}

interface IMyInterface
{
    int AddTheNumbers();
}

class MyClassA : IMyInterface
{
    public int AddTheNumbers() { return 1 + 2; }
}

class MyClassB : IMyInterface
{
    public int AddTheNumbers() { return 3 + 4; }
}

class MyAppDomain
{
    public MyAppDomain([WithKey(A)]IMyInterface aInstance, [WithKey(B)]IMyInterface bInstance)
    {
        this.ANumber = aInstance.AddTheNumbers();
        this.BNumber = bInstance.AddTheNumbers();
    }

    public int ANumber { get; private set; }

    public int BNumber { get; private set; }

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.AppendFormat("ANumber: {0}", this.ANumber);
        sb.AppendFormat(", BNumber: {0}", this.BNumber);
        return sb.ToString();
    }
}