<Query Kind="Program" />

/*
    This sample shows me how generic methods can clash
    with traditional methods, violating the rules of overloading
    at compile time.
*/
void Main()
{
    var myImp = new MyImplementation();
    myImp.MyMethod(42).Dump();
}

interface IMyBase
{
    void MyMethod(string myInput);
}

interface IMySub<TInput, TOutput> :  IMyBase
{
    TOutput MyMethod(TInput myInput);
}

class MyImplementation : IMySub<int, string> // change `TInput` to `string`
{
    public void MyMethod(string myInput)
    {
        throw new NotImplementedException();
    }
    
    public string MyMethod(int myInput) // change `TInput` to `string`
    {
        return "Yes?";
    }
}
