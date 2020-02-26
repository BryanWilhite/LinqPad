<Query Kind="Program">
  <NuGetReference>NSubstitute</NuGetReference>
  <Namespace>NSubstitute</Namespace>
  <Namespace>NSubstitute.Exceptions</Namespace>
</Query>

// https://github.com/nsubstitute/NSubstitute

void Main()
{
    var mySubstitute = Substitute.For<MyClass>();

    try
    {
        mySubstitute.MyObjectField.MyField.Returns(mf => "sub field");
    }
    catch (CouldNotSetReturnDueToNoLastCallException ex)
    {
        ex.Dump();
    }

    try
    {
        mySubstitute.MyObjectField.MyMethod().Returns(mf => "sub method");
    }
    catch (CouldNotSetReturnDueToNoLastCallException ex)
    {
        ex.Dump();
    }
}

public class MyClass
{
    public readonly MyOtherClass MyObjectField = new MyOtherClass();
}

public class MyOtherClass
{
    public string MyField = "field";

    public string MyMethod() => "method";
}