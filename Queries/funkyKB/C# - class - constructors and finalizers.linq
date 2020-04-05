<Query Kind="Program" />

void Main()
{
    var c = new MyClass();
}

class MyClass
{
    public MyClass(){ "ctor".Dump(); }
    ~MyClass() { "finalizer".Dump(); }
}