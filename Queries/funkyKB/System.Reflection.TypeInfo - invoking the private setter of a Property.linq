<Query Kind="Program" />

void Main()
{
    var myInstance = new MyClass("one", "two");
    myInstance.Dump();

    typeof(MyClass).GetTypeInfo()
        .GetDeclaredProperty(nameof(MyClass.PropertyTwo))
        .SetValue(
            myInstance,
            "two spot three",
            invokeAttr: BindingFlags.NonPublic | BindingFlags.SetField | BindingFlags.Instance,
            binder: null,
            index: null,
            culture: null);
    myInstance.Dump();

    // [ https://stackoverflow.com/questions/1778405/net-reflection-set-private-property ]
}

class MyClass
{
    public MyClass(string propertyOne, string propertyTwo)
    {
        this.PropertyOne = propertyOne;
        this.PropertyTwo = propertyTwo;
    }

    public string PropertyOne { get; } // readonly private-set syntax

    public string PropertyTwo { get; private set; }
}
