<Query Kind="Program">
  <NuGetReference>SonghayCore</NuGetReference>
  <Namespace>Newtonsoft.Json.Linq</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
  <Namespace>Songhay.Extensions</Namespace>
</Query>

async Task Main()
{
    var activity = new MyBusinessLogic();
    
    var output = await activity.StartAsync(MyBusinessLogic
        .InputPairsForOrchestration
        .TryGetValueWithKey(
            MyBusinessLogic.ProcedureNameGetBusinessValueOneAsync,
            throwException: true).Invoke(new object[] { 3, 4 })
            );

    output.Dump();

    output = await activity.StartAsync(MyBusinessLogic
        .InputPairsForOrchestration
        .TryGetValueWithKey(
            MyBusinessLogic.ProcedureNameGetBusinessValueOneAsync,
            throwException: true).Invoke(new object[] { 1, 2, 3 })
            );

    output.Dump();

    output = await activity.StartAsync(MyBusinessLogic
        .InputPairsForOrchestration
        .TryGetValueWithKey(
            MyBusinessLogic.ProcedureNameGetBusinessValueTwoAsync,
            throwException: true).Invoke(1)
            );

    output.Dump();
}

class MyBusinessLogic
{
    public const string ProcedureNameGetBusinessValueOneAsync = nameof(GetBusinessValueOneAsync);

    public const string ProcedureNameGetBusinessValueTwoAsync = nameof(GetBusinessValueTwoAsync);

    public static readonly Dictionary<string, Func<object, JObject>> InputPairsForOrchestration =
        new Dictionary<string, Func<object, JObject>>
        {
            {
                ProcedureNameGetBusinessValueOneAsync,
                o =>
                {
                    if(!(o is object[] oArray))
                        throw new InvalidOperationException("The expected object array is not here.");

                    if(oArray.Count() == 2)
                    {
                        var x = oArray[0];
                        var y = oArray[1];
                        return  GetInput(ProcedureNameGetBusinessValueOneAsync, new { x, y });
                    }

                    if(oArray.Count() == 3)
                    {
                        var x = oArray[0];
                        var y = oArray[1];
                        var z = oArray[2];
                        return  GetInput(ProcedureNameGetBusinessValueOneAsync, new { x, y, z });
                    }

                    throw new InvalidOperationException("The expected object array size is not here.");
                }
            },
            {
                ProcedureNameGetBusinessValueTwoAsync,
                x => GetInput(ProcedureNameGetBusinessValueTwoAsync, new { x })
            }
        };

    public static JObject GetInput(string procedureName, params object[] input)
    {
        return JObject.FromObject(new { procedureName, input });
    }

    internal static readonly Dictionary<string, Func<JObject, JObject>> InputPairsForActivity =
        new Dictionary<string, Func<JObject, JObject>>
        {
            {
                MyBusinessLogic.ProcedureNameGetBusinessValueOneAsync,
                input => input.GetInput()
            },
            {
                MyBusinessLogic.ProcedureNameGetBusinessValueTwoAsync,
                input => input.GetInput()
            }
        };

    public MyBusinessLogic()
    {
        this.indicesMethodsWithOutput = new Dictionary<string, Func<JObject, Task<JObject>>>
        {
            {
                ProcedureNameGetBusinessValueOneAsync,
                input => this.GetBusinessValueOneAsync(
                    InputPairsForActivity.TryGetValueWithKey(
                        ProcedureNameGetBusinessValueOneAsync,
                            throwException: true).Invoke(input)
                    )
            },
            {
                ProcedureNameGetBusinessValueTwoAsync,
                input => this.GetBusinessValueTwoAsync(
                    InputPairsForActivity.TryGetValueWithKey(
                        ProcedureNameGetBusinessValueTwoAsync,
                            throwException: true).Invoke(input)
                    )
            }
        };
    }

    public async Task<JObject> StartAsync(JObject input)
    {
        string procedureName = input.GetProcedureName();

        var hasIO = this.indicesMethodsWithOutput.TryGetValue(procedureName, out var funcIO);
        if (hasIO) return await funcIO(input);

        throw new InvalidOperationException($"{nameof(JObject)} input is unexpected");
    }

    internal readonly Dictionary<string, Func<JObject, Task<JObject>>> indicesMethodsWithOutput;

    internal async Task<JObject> GetBusinessValueOneAsync(JObject input)
    {
        int output;

        if (input.HasBusinessValueOneTrioArgs())
        {
            var args = input.GetBusinessValueOneTrioArgs();
            output = await GetBusinessValueOneAsync(args.x, args.y, args.z);
        }
        else
        {
            var args = input.GetBusinessValueOneDuoArgs();
            output = await GetBusinessValueOneAsync(args.x, args.y);
        }

        return JObject.FromObject(new { output });
    }

    internal async Task<int> GetBusinessValueOneAsync(int x, int y) => await Task.FromResult(x + y);

    internal async Task<int> GetBusinessValueOneAsync(int x, int y, int z) => await Task.FromResult(x + y + z);

    internal async Task<JObject> GetBusinessValueTwoAsync(JObject input)
    {
        var output = await GetBusinessValueTwoAsync(input.GetBusinessValueTwoArg());

        return JObject.FromObject(new { output });
    }

    internal async Task<int> GetBusinessValueTwoAsync(int x) => await Task.FromResult(x * 42);
}

public static partial class JObjectExtensions
{
    public static JObject GetInput(this JObject jObject) =>
        jObject
            .GetJArray("input")
            .FirstOrDefault()
            .GetValue<JObject>();

    public static (int x, int y) GetBusinessValueOneDuoArgs(this JObject jObject)
    {
        int x;
        int y;

        x = jObject.GetValue<int>(nameof(x));
        y = jObject.GetValue<int>(nameof(y));

        return (x, y);
    }

    public static (int x, int y, int z) GetBusinessValueOneTrioArgs(this JObject jObject)
    {
        int x;
        int y;
        int z;

        x = jObject.GetValue<int>(nameof(x));
        y = jObject.GetValue<int>(nameof(y));
        z = jObject.GetValue<int>(nameof(z));

        return (x, y, z);
    }

    public static int GetBusinessValueTwoArg(this JObject jObject)
    {
        int x;

        x = jObject.GetValue<int>(nameof(x));

        return x;
    }

    public static string GetProcedureName(this JObject jObject) =>
        jObject
            .GetValue<string>("procedureName");


    public static bool HasBusinessValueOneTrioArgs(this JObject jObject) => jObject.HasProperty("z");
}
