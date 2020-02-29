<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Net.Http.dll</Reference>
  <NuGetReference>Microsoft.AspNet.WebApi.SelfHost</NuGetReference>
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>System.Net</Namespace>
  <Namespace>System.Net.Http</Namespace>
  <Namespace>System.Web.Http</Namespace>
  <Namespace>System.Web.Http.ValueProviders</Namespace>
  <Namespace>System.Web.Http.ModelBinding</Namespace>
  <Namespace>System.Web.Http.Controllers</Namespace>
</Query>

/*
    “Model binding is used to read from the query string, while formatters are used to read from the request body.”
    [ https://www.infoworld.com/article/3133728/understand-parameter-binding-in-aspnet-web-api.html ]

    [ https://docs.microsoft.com/en-us/aspnet/web-api/overview/formats-and-model-binding/parameter-binding-in-aspnet-web-api#model-binders ]
    [ https://blog.learningtree.com/creating-a-custom-web-api-model-binder/ ]
    [ https://blogs.msdn.microsoft.com/jmstall/2012/04/18/mvc-style-parameter-binding-for-webapi/ ]
    [ https://stackoverflow.com/questions/11875912/asp-net-web-api-model-binding ]
*/
void Main()
{
    var actionContext = new HttpActionContext();

    var bindingContext = new ModelBindingContext
    {
        FallbackToEmptyPrefix = true,
        ModelName = nameof(Product),
        //ValueProvider = valueProvider,
        //ModelMetadata = metadata
    };
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