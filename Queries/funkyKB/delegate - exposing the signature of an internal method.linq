<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Net.Http.dll</Reference>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

/*
    This stupid delegate trick uses *two* public members in `MyClass`
    to hide *one* member in `MyBaseClass`.
*/
async Task Main()
{
    var myThing = new MyClass();
    
    var result = await myThing.RequestNumberAsyncHandler(2);
    
    result.Dump();
}

public class MyClass: MyBaseClass
{
    public MyClass()
    {
        this.RequestNumberAsyncHandler = this.RequestNumberAsync;
    }

    public readonly RequestNumberAsyncDelegate RequestNumberAsyncHandler;

    public delegate Task<int> RequestNumberAsyncDelegate(int startingNumber);
}

public class MyBaseClass
{
    internal async Task<int> RequestNumberAsync(int startingNumber)
    {
        return await Task.FromResult(startingNumber + 42);
    }
}
