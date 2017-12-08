<Query Kind="Program" />

/*
    This example shows one reason one would *not*
    define a static class of static/const members.
*/
void Main()
{
    MyDerived.BaseValue.Dump();
    MyDerived.DerivedValue.Dump();
}

class MyBase
{
    public const string BaseValue = "base";
}

class MyDerived : MyBase
{
    public const string DerivedValue = "derived";
}
