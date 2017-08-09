<Query Kind="Program">
  <NuGetReference>System.Net.Http</NuGetReference>
  <NuGetReference>EntityFramework</NuGetReference>
  <NuGetReference>Microsoft.AspNet.OData</NuGetReference>
  <NuGetReference>Microsoft.AspNet.WebApi.Client</NuGetReference>
  <NuGetReference>Microsoft.AspNet.WebApi.Owin</NuGetReference>
  <NuGetReference>Microsoft.Net.Http</NuGetReference>
  <NuGetReference>Microsoft.Owin</NuGetReference>
  <NuGetReference>Microsoft.Owin.SelfHost</NuGetReference>
  <NuGetReference Prerelease="true">morelinq</NuGetReference>
  <NuGetReference>Owin</NuGetReference>
  <Namespace>Microsoft.Owin.Hosting</Namespace>
  <Namespace>MoreLinq</Namespace>
  <Namespace>Owin</Namespace>
  <Namespace>System.Net.Http</Namespace>
  <Namespace>System.Web.Http</Namespace>
  <Namespace>System.Web.Http.Dispatcher</Namespace>
  <Namespace>System.Web.OData</Namespace>
  <Namespace>System.Web.OData.Builder</Namespace>
  <Namespace>System.Web.OData.Extensions</Namespace>
  <Namespace>System.Web.OData.Query</Namespace>
  <Namespace>System.Web.OData.Routing</Namespace>
  <Namespace>System.Web.Http.Controllers</Namespace>
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
                response.Dump();
                response.Content.ReadAsStringAsync().Result.Dump();
            };

            getOwin("odata/Special01/$metadata"); //gets “Service Metadata Document”
            getOwin("odata/Special02/Product?$count=true"); //maps to ProductController.Get()
            getOwin("odata/Special03/Product?$count=true&$skip=3");
            getOwin("odata/Special04/Product(2)"); //maps to ProductController.Get(key)
            getOwin("odata/Special05/Product?$filter=Category+eq+'Bakery'+and+indexof(Name,'Tortillas')+ne+-1");
        }
    }
    finally
    {
        client.Dispose();
    }
}

public interface IProduct
{
    int Id { get; set; }
    string Name { get; set; }
    decimal Price { get; set; }
    string Category { get; set; }
}

public class Product : IProduct
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Category { get; set; }
}

public class ProductRepository
{
    public IQueryable<IProduct> LoadProducts()
    {
        var data = new[]
        {
            new Product{ Id = 1, Category = "Bakery", Name = "Spinach/Rice Tortillas", Price = 6.99M },
            new Product{ Id = 2, Category = "Bakery", Name = "Organic Corn Tortillas", Price = 1.99M },
            new Product{ Id = 3, Category = "Bakery", Name = "Flower Flour Thins", Price = 5.99M },
            new Product{ Id = 4, Category = "Canned", Name = "Ranchero Beans", Price = 1.69M },
            new Product{ Id = 5, Category = "Canned", Name = "Wild Caught Sardines in Water", Price = 3.99M },
            new Product{ Id = 6, Category = "Canned", Name = "Wild Caught Salmon in Water", Price = 6.99M },
            new Product{ Id = 7, Category = "Grocery", Name = "Organic Swiss Chard", Price = 2.89M },
            new Product{ Id = 8, Category = "Grocery", Name = "Organic Kale", Price = 2.99M },
            new Product{ Id = 9, Category = "Grocery", Name = "Organic Red Potatoes", Price = 2.49M },
        };

        return data.AsQueryable();
    }
}

public class ProductController : ODataController
{
    public ProductController()
    {
        this._repository = new ProductRepository();
        this._repository.Dump("controller and repository loaded");
    }

    [EnableQuery(PageSize = 3, AllowedQueryOptions = AllowedQueryOptions.All)]
    [HttpGet]
    public IHttpActionResult Get()
    {
        return this.Ok(this._repository.LoadProducts());
    }

    [EnableQuery]
    [HttpGet]
    public IHttpActionResult Get([FromODataUri] int key) //parameter must be named “key”
    {
        var data = this._repository.LoadProducts().SingleOrDefault(p => p.Id == key);
        if(data == null) return this.NotFound();
        return this.Ok(data);
    }

    protected override void Initialize(HttpControllerContext controllerContext)
    {
        base.Initialize(controllerContext);
        this._repository.Dump("controller initialized");
        controllerContext.RouteData.Values["specialId"].Dump("routePrefix templated value");
    }

    ProductRepository _repository;
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
        var config = new HttpConfiguration();

        config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;
        config.Services.Replace(typeof(IHttpControllerTypeResolver), new ControllerResolver());

        var builder = (new ODataConventionModelBuilder()).WithProductEntity();

        /*
            The following is an optimizing, breaking change in OData 6.x.
            For detail, see https://stackoverflow.com/a/39533417/22944.
        */
        builder
            .EntityType<Product>()
            .Count()
            .Filter(QueryOptionSetting.Allowed);

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

        builder.EntitySet<Product>(typeof(Product).Name)
            .EntityType.DerivesFrom<IProduct>();

        return builder;
    }
}

/*
    Note that this is an OData >=v4 arrangement.
    In OData v4:

    * [Queryable] is replaced with [EnableQuery]
    * config.MapODataRoute() is replaced with config.MapODataServiceRoute()

    For detail, see https://www.strathweb.com/2014/02/getting-started-odata-v4-asp-net-web-api/
*/