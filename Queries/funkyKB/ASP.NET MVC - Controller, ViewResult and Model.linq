<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Net.Http.dll</Reference>
  <NuGetReference>Microsoft.AspNet.Mvc</NuGetReference>
  <Namespace>System.Web.Mvc</Namespace>
</Query>

/*
    [https://docs.microsoft.com/en-us/dotnet/api/system.web.mvc?view=aspnet-mvc-5.2]
    [https://docs.microsoft.com/en-us/aspnet/mvc/overview/older-versions-1/unit-testing/creating-unit-tests-for-asp-net-mvc-applications-cs]
*/
void Main()
{
    var controller = new ProductsController();
    var indexResult = controller.Index() as ViewResult;
    Debug.Assert(indexResult.ViewName.Equals(nameof(ProductsController.Index)));

    var productResult1 = controller.Details(2) as ViewResult;
    Debug.Assert(productResult1.ViewName.Equals(nameof(ProductsController.Details)));
    Debug.Assert((productResult1.Model as Product).Name.Equals("Yo-yo"));

    var productResult2 = controller.Details(42) as HttpNotFoundResult;
    productResult2.Dump();
}

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Category { get; set; }
    public decimal Price { get; set; }
}

public class ProductsController : Controller
{
    Product[] products = new Product[]
    {
        new Product { Id = 1, Name = "Tomato Soup", Category = "Groceries", Price = 1 },
        new Product { Id = 2, Name = "Yo-yo", Category = "Toys", Price = 3.75M },
        new Product { Id = 3, Name = "Hammer", Category = "Hardware", Price = 16.99M }
    };

    public ActionResult Index()
    {
        return View(nameof(this.Index));
    }

    public ActionResult Details(int Id)
    {
        var data = products.FirstOrDefault(i => i.Id == Id);
        if(data == null) return this.HttpNotFound();

        return View(nameof(this.Details), data);
    }
}