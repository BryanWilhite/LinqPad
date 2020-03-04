<Query Kind="Program">
  <NuGetReference>Microsoft.AspNet.WebApi.Core</NuGetReference>
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <NuGetReference>NSubstitute</NuGetReference>
  <Namespace>System.Web.Http.ModelBinding</Namespace>
  <Namespace>System.Web.Http</Namespace>
  <Namespace>System.Net</Namespace>
  <Namespace>System.Web.Http.Controllers</Namespace>
  <Namespace>NSubstitute</Namespace>
  <Namespace>System.Web.Http.Routing</Namespace>
  <Namespace>System.Net.Http</Namespace>
  <Namespace>System.Web.Http.Metadata</Namespace>
  <Namespace>System.Web.Http.Metadata.Providers</Namespace>
  <Namespace>Newtonsoft.Json.Linq</Namespace>
</Query>

void Main()
{
    string requestBody = @"
{
    ""Name"": ""Hammer"",
    ""Category"": ""Hardware"",
    ""Price"": 18.99
}".Trim();

    var httpActionContext = GetHttpActionContext(requestBody);
    var modelMetadata = new ModelMetadata(
        new EmptyModelMetadataProvider(),
        GetType(),
        () => new Product(),
        typeof(Product),
        propertyName: null
    );

    var bindingContext = new ModelBindingContext
    {
        FallbackToEmptyPrefix = true,
        ModelMetadata = modelMetadata
    };

    var binder = new ProductJsonModelBinder();

    var isBound = binder.BindModel(httpActionContext, bindingContext);
    if (isBound) bindingContext.Model.Dump();
}

static HttpActionContext GetHttpActionContext(string requestBody)
{
    const string mediaType = "application/json";

    var httpRequestMessage = new HttpRequestMessage
    {
        Content = new StringContent(requestBody, Encoding.UTF8, mediaType)
    };

    var httpConfiguration = new HttpConfiguration();

    var httpControllerContext = new HttpControllerContext(
        httpConfiguration,
        Substitute.For<IHttpRouteData>(),
        httpRequestMessage);

    return new HttpActionContext(httpControllerContext, new ReflectedHttpActionDescriptor());
}

public class ProductJsonModelBinder : IModelBinder
{
    public bool BindModel(HttpActionContext actionContext, ModelBindingContext bindingContext)
    {
        Type incomingType = bindingContext.ModelType;

        object bindingContextModel = bindingContext.Model;
        if (bindingContextModel == null)
            throw new NullReferenceException("The expected binding context model is not here.");

        JObject jIncoming = GetIncomingJson(actionContext);
        if (jIncoming == null)
            throw new NullReferenceException("The expected incoming JSON is not here.");

        MemberInfo[] members = incomingType.GetMembers().Where(IsWritable).ToArray();

        foreach (var memberInfo in members)
        {
            jIncoming.TryGetValue(memberInfo.Name, out JToken jToken);

            if (GetMemberType(memberInfo) == typeof(decimal))
            {
                SetMember(memberInfo, bindingContextModel, jToken?.Value<decimal>());
            }
            else
            {
                SetMember(memberInfo, bindingContextModel, jToken?.Value<string>());
            }
        }

        return true;
    }

    static JObject GetIncomingJson(HttpActionContext actionContext)
    {
        string json = actionContext.Request.Content.ReadAsStringAsync().Result;
        try
        {
            return JObject.Parse(json);
        }
        catch
        {
            return null;
        }
    }

    static Type GetMemberType(MemberInfo memberInfo)
    {
        switch (memberInfo)
        {
            case FieldInfo fieldInfo:
                return fieldInfo.FieldType;
            case PropertyInfo propertyInfo:
                return propertyInfo.PropertyType;
            default:
                return null;
        }
    }

    static bool IsWritable(MemberInfo memberInfo)
    {
        switch (memberInfo)
        {
            case FieldInfo fieldInfo:
                return fieldInfo.IsPublic;
            case PropertyInfo propertyInfo:
                return propertyInfo.CanWrite;
            default:
                return false;
        }
    }

    static void SetMember(MemberInfo memberInfo, object instance, object value)
    {
        switch (memberInfo)
        {
            case FieldInfo fieldInfo:
                fieldInfo.SetValue(instance, value);
                break;
            case PropertyInfo propertyInfo:
                propertyInfo.SetValue(instance, value, null);
                break;
        }
    }
}

public class Product
{
    public int Id { get; set; }
    public string Name;
    public string Category { get; set; }
    public decimal Price { get; set; }
}

/*
    “Model binding is used to read from the query string, while formatters are used to read from the request body.”
    [ https://www.infoworld.com/article/3133728/understand-parameter-binding-in-aspnet-web-api.html ]

    [ https://docs.microsoft.com/en-us/aspnet/web-api/overview/formats-and-model-binding/parameter-binding-in-aspnet-web-api#model-binders ]
    [ https://blog.learningtree.com/creating-a-custom-web-api-model-binder/ ]
    [ https://blogs.msdn.microsoft.com/jmstall/2012/04/18/mvc-style-parameter-binding-for-webapi/ ]
    [ https://stackoverflow.com/questions/11875912/asp-net-web-api-model-binding ]
*/