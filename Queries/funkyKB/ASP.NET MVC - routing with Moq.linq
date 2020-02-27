<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Web.dll</Reference>
  <NuGetReference>Microsoft.AspNet.Mvc</NuGetReference>
  <NuGetReference>Moq</NuGetReference>
  <Namespace>Moq</Namespace>
  <Namespace>System.Web</Namespace>
  <Namespace>System.Web.Mvc</Namespace>
  <Namespace>System.Web.Routing</Namespace>
</Query>

void Main()
{
    var controller = new ProductController();
    
    var routes = new RouteCollection();
    routes.MapRoute(
        "Default",
        "{controller}/{action}/{id}",
        new
        {
            controller = nameof(ProductController).Replace(nameof(Controller), string.Empty),
            action = nameof(ProductController.Index),
            id = string.Empty
        }
    );

    var httpContextBaseMock = new Mock<HttpContextBase>();
    this.SetupMock(httpContextBaseMock);
    
    var routeData = routes.GetRouteData(httpContextBaseMock.Object);
    routeData.Dump();
}

void SetupMock(Mock<HttpContextBase> httpContextBaseMock)
{
    // 📚[ https://deanhume.com/test-your-mvc-routes-with-moq/ ]
    httpContextBaseMock
        .Setup(mock => mock.Request.AppRelativeCurrentExecutionFilePath)
        .Returns (() => "~/Product/Details/2");
}

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Category { get; set; }
    public decimal Price { get; set; }
}

public class ProductController : Controller
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
        if (data == null) return this.HttpNotFound();

        return View(nameof(this.Details), data);
    }
}