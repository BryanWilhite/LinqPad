<Query Kind="Program" />

void Main()
{
    var genericType = typeof(MyClass<>).MakeGenericType(typeof(int));
    var activatedInstance = Activator.CreateInstance(genericType);
    var fieldInfo = genericType.GetField(nameof(MyClass<object>.Value));
    fieldInfo.SetValue(activatedInstance, 42);
    activatedInstance.Dump();
}

class MyClass<T>
{
    public T Value;
}