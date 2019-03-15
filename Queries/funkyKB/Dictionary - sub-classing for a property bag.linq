<Query Kind="Program" />

void Main()
{
    var dict = new MyPropertyBag<string>();
    
    dict["bryan"] = "told you so";
    
    dict.Dump();
}

class MyPropertyBag<TValue> : Dictionary<string, TValue>
{
}