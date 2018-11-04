<Query Kind="Statements" />

// https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/dynamic

dynamic dyn = 1;
object obj = 1;

Console.WriteLine(dyn.GetType());
Console.WriteLine(obj.GetType());

// obj = obj + 3; // CS0019 Operator '+' cannot be applied to operands of type 'object' and 'int

dyn = dyn + 3;
Console.WriteLine($"`dyn + 3 = {dyn}` type: {dyn.GetType()}");

dyn = dyn + "3";
Console.WriteLine($@"`dyn + ""3"" = {dyn}` type: {dyn.GetType()}");

dyn = float.Parse(dyn + ".33") + 3;
Console.WriteLine($@"`float.Parse(dyn + "".33"") + 3 = {dyn}` type: {dyn.GetType()}");

Console.WriteLine($"`dyn is dynamic = {dyn is dynamic}`");
// CS1981 Using 'is' to test compatibility with 'dynamic' is essentially identical to testing compatibility with 'Object' and will succeed for all non-null values

dynamic dynamicLocalFunction(dynamic d)
{
    // A dynamic local variable.
    dynamic local = "Local variable";
    int two = 2;

    if (d is int)
    {
        return local;
    }
    else
    {
        return two;
    }
}

dyn = dynamicLocalFunction("two");
Console.WriteLine($@"dynamicLocalFunction(""two""): `{dyn}` type: {dyn.GetType()}");

dyn = dynamicLocalFunction(3);
Console.WriteLine($@"dynamicLocalFunction(3): `{dyn}` type: {dyn.GetType()}");

Console.WriteLine($"typeof(List<dynamic>): {typeof(List<dynamic>)}");

//Console.WriteLine(typeof(dynamic)); // CS1962 The typeof operator cannot be used on the dynamic type

var dynList = new List<dynamic> { "one", 1, DateTime.Now };
dynList.Select(i => $"{i} type: {i.GetType()}").Dump("dynamic list");
