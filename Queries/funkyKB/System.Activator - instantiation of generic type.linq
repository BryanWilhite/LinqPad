<Query Kind="Program" />

void Main()
{
    var instance = new MyClass<int> { Value = 42 };
    
    var genericType = typeof(MyClass<>).MakeGenericType(typeof(int));
    var activatedInstance = Activator.CreateInstance(genericType) as MyClass<int>;
    
    activatedInstance.Value = 42;
    
    Debug.Assert(instance.Value == activatedInstance.Value);
}

class MyClass<T>
{
    public T Value { get; set; }
}
