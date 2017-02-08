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

void Main()
{
    var data = new List<IFoo>
    {
        new Aye { CoefficientOfFuBar = 0, Foo = "few" },
        new Bee { Bar = "staff", Foo = "few" },
    };
    
    data.Dump();
}