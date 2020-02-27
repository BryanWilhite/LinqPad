<Query Kind="Program">
  <NuGetReference>Microsoft.AspNet.WebApi.SelfHost</NuGetReference>
  <Namespace>System.Net.Http</Namespace>
  <Namespace>System.Web.Http</Namespace>
  <Namespace>System.Web.Http.SelfHost</Namespace>
  <Namespace>System.Net</Namespace>
</Query>

/*
    Based on “Self-Host ASP.NET Web API 1”
    [http://www.asp.net/web-api/overview/older-versions/self-host-a-web-api]
    
    WARNING: “By default, listening at a particular HTTP address requires administrator privileges.”
    
    and “Writing tests for an ASP.NET Web API service”
    📖 [ http://blogs.msdn.com/b/youssefm/archive/2013/01/28/writing-tests-for-an-asp-net-webapi-service.aspx ]
    
    and “In memory client, host and integration testing of your Web API service”
    📖 [ http://blogs.msdn.com/b/kiranchalla/archive/2012/05/06/in-memory-client-amp-host-and-integration-testing-of-your-web-api-service.aspx ]
    
    📖 [ https://www.strathweb.com/2013/04/hosting-asp-net-web-api-in-linqpad/ ]
*/
void Main()
{
    var config = new HttpSelfHostConfiguration("http://localhost/");
    config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;
    config.Routes.MapHttpRoute("API Default", "api/{controller}/{id}", new { id = RouteParameter.Optional });
    
    using (HttpSelfHostServer server = new HttpSelfHostServer(config))
    using (HttpClient client = new HttpClient())
    {
        server.OpenAsync().Wait();

        var location = "http://localhost/api/products";
        using (HttpResponseMessage response = client.GetAsync(location).Result)
        {
            response.EnsureSuccessStatusCode();
            var products = response.Content.ReadAsAsync<IEnumerable<Product>>().Result;
            products.Dump();
        }

        server.CloseAsync().Wait();
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