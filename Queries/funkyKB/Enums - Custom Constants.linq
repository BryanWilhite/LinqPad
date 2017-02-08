<Query Kind="Program" />

enum MyEnum
{
    One = 1,
    Two = 2,
    Three = 3,
}

void DumpSequence(Type enumType)
{
    var sequence = Enum.GetValues(enumType);
    
    foreach (var e in sequence)
    {
       ((int)e).Dump();
       e.Dump();
    }
}

void Main()
{
    var enumType = typeof(MyEnum);
    this.DumpSequence(enumType);
}
