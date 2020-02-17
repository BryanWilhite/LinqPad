<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Net.Http.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Web.dll</Reference>
  <NuGetReference>Microsoft.AspNet.Mvc</NuGetReference>
  <NuGetReference>Moq</NuGetReference>
  <Namespace>Moq</Namespace>
  <Namespace>Newtonsoft.Json.Linq</Namespace>
  <Namespace>System.Web</Namespace>
  <Namespace>System.Web.Mvc</Namespace>
  <Namespace>System.Web.Routing</Namespace>
</Query>

void Main()
{
    var httpContextWrapper = this.GetEmptyHttpContextWrapper();

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

HttpContextWrapper GetEmptyHttpContextWrapper()
{
    // [ https://stackoverflow.com/a/9512095/22944 ]
    var httpRequest = new HttpRequest(string.Empty, "http://mySomething", string.Empty);
    var responseWriter = new StringWriter();
    var httpResponse = new HttpResponse(responseWriter);
    var httpContext = new HttpContext(httpRequest, httpResponse);
    var httpContextWrapper = new HttpContextWrapper(httpContext);
    
    return httpContextWrapper;
    
    /*
        “The HttpRequestWrapper class derives from the HttpRequestBase class
        and serves as a wrapper for the HttpRequest class.
        This class exposes the functionality of the HttpRequest class
        and exposes the HttpRequestBase type.
        The HttpRequestBase class enables you to replace the original implementation
        of the HttpRequest class in your application with a custom implementation,
        such as when you perform unit testing outside the ASP.NET pipeline.”

        [ https://docs.microsoft.com/en-us/dotnet/api/system.web.httprequestwrapper?view=netframework-4.8#remarks ]
        
        “The point is that ‘vintage’ `HttpContext` does not implement `HttpContextBase`,
        and isn’t virtual, and therefore cannot be Mocked.
        `HttpContextBase` was introduced in 3.5 as a mockable alternative.
        But there’s still the problem that vintage `HttpContext` doesn't implement `HttpContextBase`.”
        [ https://stackoverflow.com/a/5464628/22944 ]
        [ http://www.splinter.com.au/httpcontext-vs-httpcontextbase-vs-httpcontext/ ]
    */
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