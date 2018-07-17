<Query Kind="Program">
  <NuGetReference Prerelease="true">morelinq</NuGetReference>
  <NuGetReference>EntityFramework</NuGetReference>
  <NuGetReference>Microsoft.AspNet.OData</NuGetReference>
  <NuGetReference>Microsoft.AspNet.WebApi.Owin</NuGetReference>
  <NuGetReference>Microsoft.Net.Http</NuGetReference>
  <NuGetReference>Microsoft.Owin.SelfHost</NuGetReference>
  <NuGetReference>Microsoft.Owin</NuGetReference>
  <NuGetReference>Owin</NuGetReference>
  <NuGetReference>System.Net.Http</NuGetReference>
  <Namespace>Microsoft.AspNet.OData.Builder</Namespace>
  <Namespace>Microsoft.AspNet.OData.Extensions</Namespace>
  <Namespace>Microsoft.AspNet.OData.Routing</Namespace>
  <Namespace>Microsoft.AspNet.OData</Namespace>
  <Namespace>Microsoft.Owin.Hosting</Namespace>
  <Namespace>Newtonsoft.Json.Linq</Namespace>
  <Namespace>Owin</Namespace>
  <Namespace>System.Net.Http</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
  <Namespace>System.Web.Http.Controllers</Namespace>
  <Namespace>System.Web.Http.Dispatcher</Namespace>
  <Namespace>System.Web.Http</Namespace>
</Query>

void Main()
{
    var baseAddress = "http://localhost:9000/";

    var client = new HttpClient();
    try
    {
        using (WebApp.Start<Startup>(url: baseAddress))
        {
            HttpResponseMessage response;

            Action<string> getOwin = path =>
            {
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri(baseAddress + path)
                };

                response = client.SendAsync(request).Result;
                response.Dump("GET:");
                response.Content.ReadAsStringAsync().Result.Dump();
            };

            getOwin("odata/Special01/$metadata");

            Action<string, string> postOwin = (path, content) =>
            {
                var request = new HttpRequestMessage
                {
                    Content = new StringContent(content, Encoding.UTF8, "application/json"),
                    Method = HttpMethod.Post,
                    RequestUri = new Uri(baseAddress + path)
                };

                response = client.SendAsync(request).Result;
                response.Dump("POST:");
                response.Content.ReadAsStringAsync().Result.Dump();
            };

            var jA = JArray.FromObject(
                new[]
                {
                    new Product { Category = "Canned", Name = "Tuna in Water", Price = 4.99M },
                    new Product { Category = "Canned", Name = "Tuna in Canola Oil", Price = 3.99M },
                    new Product { Category = "Canned", Name = "Tuna in Organic Olive Oil", Price = 6.99M },
                }
            );
            var postData = (new JObject { {"Products", jA} }).ToString();

            postOwin("odata/Special04/ProductsPostMany", postData);
        }
    }
    finally
    {
        client.Dispose();
    }
}

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Category { get; set; }
}

public class ProductController : ODataController
{
    public ProductController()
    {
        string.Empty.Dump("controller constructed");
    }

    [HttpPost]
    [ODataRoute(nameof(ProductController.ProductsPostMany))]
    public async Task<IHttpActionResult> ProductsPostMany(ODataActionParameters parameters)
    {
        if (parameters == null)
        {
            string.Empty.Dump($"{nameof(ODataActionParameters)} is null :(");
            return this.InternalServerError();
        }

        parameters.Dump(nameof(ODataActionParameters));
        var key = $"{typeof(Product).Name}s";
        if(!parameters.ContainsKey(key)) return this.InternalServerError();
        var data = parameters[key] as IEnumerable<Product>;

        await Task.Run(() => data.Dump("save in invisible database"));

        return this.Ok(data);
    }

    protected override void Initialize(HttpControllerContext controllerContext)
    {
        base.Initialize(controllerContext);
        controllerContext.RouteData.Values["specialId"].Dump("routePrefix templated value");
    }
}

public class ControllerResolver : DefaultHttpControllerTypeResolver
{
    public override ICollection<Type> GetControllerTypes(IAssembliesResolver assembliesResolver)
    {
        var types = Assembly.GetExecutingAssembly().GetExportedTypes();
        var controllerType = typeof(System.Web.Http.Controllers.IHttpController);
        var list = types.Where(i => controllerType.IsAssignableFrom(i)).ToList();
        list.Add(typeof(MetadataController));
        list.Dump(this.GetType().Name);
        return list;
    }
}

public class Startup
{
    public void Configuration(IAppBuilder appBuilder)
    {
        var config = new HttpConfiguration
        {
            IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always
        };
        config.Services.Replace(typeof(IHttpControllerTypeResolver), new ControllerResolver());

        var builder =
            (new ODataConventionModelBuilder())
            .WithProductEntity();

        var model = builder.GetEdmModel();

        /*
            Activate OData routing conventions:
            For detail, see [https://docs.microsoft.com/en-us/aspnet/web-api/overview/odata-support-in-aspnet-web-api/odata-routing-conventions]
        */
        config.MapODataServiceRoute(
            routeName: "ODataRoute",
            routePrefix: "odata/{specialId}",
            model: model
        );

        appBuilder.UseWebApi(config);

        config.EnsureInitialized();
    }
}

static class ODataConventionModelBuilderExtensions
{
    public static ODataConventionModelBuilder WithProductEntity(this ODataConventionModelBuilder builder)
    {
        if (builder == null) return null;

        builder.EntitySet<Product>(typeof(Product).Name);
        builder
            .Action(nameof(ProductController.ProductsPostMany))
            .Returns<IHttpActionResult>()
            .CollectionParameter<Product>($"{typeof(Product).Name}s");

        return builder;
    }
}

/*
    For detail, see https://stackoverflow.com/questions/31117411/odata-action-accepting-list-parameter-is-always-null
*/