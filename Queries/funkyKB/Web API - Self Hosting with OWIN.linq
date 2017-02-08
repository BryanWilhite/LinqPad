<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Net.Http.dll</Reference>
  <NuGetReference>Microsoft.AspNet.WebApi.OwinSelfHost</NuGetReference>
  <Namespace>Microsoft.Owin.Hosting</Namespace>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>Owin</Namespace>
  <Namespace>System.Net</Namespace>
  <Namespace>System.Net.Http</Namespace>
  <Namespace>System.Net.Http.Formatting</Namespace>
  <Namespace>System.Net.Http.Headers</Namespace>
  <Namespace>System.Web.Http</Namespace>
  <Namespace>System.Web.Http.Dispatcher</Namespace>
</Query>

/*
    Based on “Use OWIN to Self-Host ASP.NET Web API 2”
    [http://www.asp.net/web-api/overview/hosting-aspnet-web-api/use-owin-to-self-host-web-api]
    
    see also: [http://www.strathweb.com/2013/04/hosting-asp-net-web-api-in-linqpad/]
*/

void Main()
{
    var baseAddress = "http://localhost:9000/"; 

    var client = new HttpClient();
    try
    {
        using (WebApp.Start<Startup>(url: baseAddress)) 
        { 
    
            HttpResponseMessage response;

            response = client.GetAsync(baseAddress + "api/values").Result; 
            response.Dump(); 
            response.Content.ReadAsStringAsync().Result.Dump(); 

            response = client.GetAsync(baseAddress + "api/values/5").Result; 
            response.Dump(); 
            response.Content.ReadAsStringAsync().Result.Dump(); 

            response = client.GetAsync(baseAddress + "api/products").Result; 
            response.Dump(); 
            response.Content.ReadAsStringAsync().Result.Dump(); 

            response = client.GetAsync(baseAddress + "api/products/1").Result; 
            response.Dump(); 

            response.Content.ReadAsStringAsync().Result.Dump(); 
            response = client.PostAsJsonAsync(baseAddress + "api/values/5",
                "my poster").Result;
            response.Dump(); 
            response.Content.ReadAsStringAsync().Result.Dump(); 

            response = client.PutAsync(baseAddress + "api/values/5",
                "my putter",
                new JsonMediaTypeFormatter()).Result;
            response.Dump(); 
            response.Content.ReadAsStringAsync().Result.Dump(); 
    
            response = client.DeleteAsync(baseAddress + "api/values/5").Result;
            response.Content.ReadAsStringAsync().Result.Dump(); 
        }
    }
    finally
    {
        client.Dispose();
    }
}

public class ControllerResolver : DefaultHttpControllerTypeResolver 
{
    public override ICollection<Type> GetControllerTypes(IAssembliesResolver assembliesResolver) 
    {
        var types = Assembly.GetExecutingAssembly().GetExportedTypes();
        var controllerType = typeof(System.Web.Http.Controllers.IHttpController);
        var list = types.Where(i => controllerType.IsAssignableFrom(i)).ToList();
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
        config.Routes.MapHttpRoute(
            name: "DefaultApi",
            routeTemplate: "api/{controller}/{id}",
            defaults: new { id = RouteParameter.Optional }
        );
        appBuilder.UseWebApi(config);
    }
}

public class ValuesController : ApiController 
{ 
    // GET api/values 
    public IEnumerable<string> Get() 
    { 
        return new string[] { "value1", "value2" }; 
    } 

    // GET api/values/5 
    public string Get(int id) 
    { 
        return "value"; 
    } 

    // POST api/values 
    public void Post([FromBody]string value) 
    {
        value.Dump("post");
    } 

    // PUT api/values/5 
    public void Put(int id, [FromBody]string value) 
    { 
        id.Dump("put");
        value.Dump("put");
    } 

    // DELETE api/values/5 
    public void Delete(int id) 
    { 
        id.Dump("delete");
    } 
}

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Category { get; set; }
    public decimal Price { get; set; }
}

public class ProductsController : ApiController
{
    Product[] products = new Product[]  
    {  
        new Product { Id = 1, Name = "Tomato Soup", Category = "Groceries", Price = 1 },  
        new Product { Id = 2, Name = "Yo-yo", Category = "Toys", Price = 3.75M },  
        new Product { Id = 3, Name = "Hammer", Category = "Hardware", Price = 16.99M }  
    };

    public IEnumerable<Product> GetAllProducts()
    {
        return products;
    }

    public Product GetProductById(int id)
    {
        var product = products.FirstOrDefault((p) => p.Id == id);
        if (product == null)
        {
            throw new HttpResponseException(HttpStatusCode.NotFound);
        }
        return product;
    }

    public IEnumerable<Product> GetProductsByCategory(string category)
    {
        return products.Where(p => string.Equals(p.Category, category,
                StringComparison.OrdinalIgnoreCase));
    }
}