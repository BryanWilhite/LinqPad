<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Net.Http.dll</Reference>
  <NuGetReference>Microsoft.AspNet.WebApi.SelfHost</NuGetReference>
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>System.Net</Namespace>
  <Namespace>System.Net.Http</Namespace>
  <Namespace>System.Web.Http</Namespace>
</Query>

/*
*/
void Main()
{
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