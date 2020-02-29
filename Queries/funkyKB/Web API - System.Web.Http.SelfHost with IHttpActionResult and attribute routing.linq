<Query Kind="Program">
  <NuGetReference>Microsoft.AspNet.WebApi.SelfHost</NuGetReference>
  <NuGetReference>Microsoft.AspNet.WebApi.WebHost</NuGetReference>
  <Namespace>System.Net.Http</Namespace>
  <Namespace>System.Web.Http</Namespace>
  <Namespace>System.Web.Http.SelfHost</Namespace>
  <Namespace>System.Net</Namespace>
</Query>

/*
    Based on “Self-Host ASP.NET Web API 1”
    [http://www.asp.net/web-api/overview/older-versions/self-host-a-web-api]

    WARNING: “By default, listening at a particular HTTP address requires administrator privileges.”

    “Attribute Routing in ASP.NET Web API” by Mike Wasson
    📖 [ https://docs.microsoft.com/en-us/aspnet/web-api/overview/web-api-routing-and-actions/attribute-routing-in-web-api-2#enabling-attribute-routing ]
*/
void Main()
{
    var config = new HttpSelfHostConfiguration("http://localhost/");
    config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;
    config.MapHttpAttributeRoutes();

    using (HttpSelfHostServer server = new HttpSelfHostServer(config))
    using (HttpClient client = new HttpClient())
    {
        server.OpenAsync().Wait();

        try
        {
            var location = "http://localhost/api/products";
            using (HttpResponseMessage response = client.GetAsync(location).Result)
            {
                response.EnsureSuccessStatusCode();
                var products = response.Content.ReadAsAsync<IEnumerable<Product>>().Result;
                products.Dump();
            }

            location = "http://localhost/api/products/2";
            using (HttpResponseMessage response = client.GetAsync(location).Result)
            {
                response.EnsureSuccessStatusCode();
                var product = response.Content.ReadAsAsync<Product>().Result;
                product.Dump();
            }

            location = "http://localhost/api/products/categories/hardware";
            using (HttpResponseMessage response = client.GetAsync(location).Result)
            {
                response.EnsureSuccessStatusCode();
                var products = response.Content.ReadAsAsync<IEnumerable<Product>>().Result;
                products.Dump();
            }
        }
        finally
        {
            server.CloseAsync().Wait();
        }
    }
}

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Category { get; set; }
    public decimal Price { get; set; }
}

[RoutePrefix("api")]
public class ProductsController : ApiController
{
    Product[] products = new Product[]
    {
        new Product { Id = 1, Name = "Tomato Soup", Category = "Groceries", Price = 1 },
        new Product { Id = 2, Name = "Yo-yo", Category = "Toys", Price = 3.75M },
        new Product { Id = 3, Name = "Hammer", Category = "Hardware", Price = 16.99M },
        new Product { Id = 4, Name = "Jackhammer", Category = "Hardware", Price = 149.99M },
    };

    [HttpGet]
    [Route("products")]
    public IHttpActionResult GetAllProducts()
    {
        return this.Ok(products);
    }

    [HttpGet]
    [Route("products/{id}")]
    public IHttpActionResult GetProductById(int id)
    {
        var product = products.FirstOrDefault((p) => p.Id == id);
        if (product == null) return this.NotFound();
        return this.Ok(product);
    }

    [HttpGet]
    [Route("products/categories/{category}")]
    public IHttpActionResult GetProductsByCategory(string category)
    {
        var result = products.Where(p => string.Equals(p.Category, category,
                StringComparison.OrdinalIgnoreCase));
        return this.Ok(result);
    }
}