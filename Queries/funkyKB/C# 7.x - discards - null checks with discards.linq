<Query Kind="Program" />

void Main()
{
    try
    {	        
        this.MyMethod(null);
    }
    catch (ArgumentNullException ex)
    {
        ex.Dump();
    }
}

public string MyMethod(MyThing thing)
{
    _ = thing ?? throw new ArgumentNullException(nameof(thing));

    return nameof(MyThing);
}

public class MyThing { }

// 📖 https://docs.microsoft.com/en-us/dotnet/csharp/discards
