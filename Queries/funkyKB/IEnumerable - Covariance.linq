<Query Kind="Program" />

interface IFoo
{
	string Foo { get; set; }
}

class Aye : IFoo
{
    public string Foo {get;set;}
    public double CoefficientOfFuBar {get;set;}
}

class Bee : IFoo
{
    public string Bar {get;set;}
    public string Foo {get;set;}
}

/*
    More types can be derived from IFoo than from Aye or Bee.

    “Covariance enables you to use a more derived type than that specified
    by the generic parameter. This allows for implicit conversion
    of classes that implement variant interfaces…”
    [https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/out-generic-modifier]
*/

void Main()
{
    var data = new List<IFoo> // derived from IEnumerable<out T>
    {
        new Aye { CoefficientOfFuBar = 0, Foo = "few" },
        new Bee { Bar = "staff", Foo = "few" },
    };
    
    data.Dump();
}