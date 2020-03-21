<Query Kind="Program" />

void Main()
{
    //https://stackoverflow.com/a/263719/22944
    //https://docs.microsoft.com/en-us/archive/blogs/ericlippert/immutability-in-c-part-one-kinds-of-immutability

    var readOnly = new ReadOnly<int>(42);
}

public class ReadOnly<T>
{
    public ReadOnly(T input)
    {
        this.Value = input;
    }
    
    public T Value { get; }
}