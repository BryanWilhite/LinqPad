<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Net.Http.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Web.dll</Reference>
  <NuGetReference>Microsoft.AspNet.Mvc</NuGetReference>
  <NuGetReference>Moq</NuGetReference>
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>System.Web.Mvc</Namespace>
  <Namespace>System.Collections.Specialized</Namespace>
  <Namespace>System.Web</Namespace>
  <Namespace>System.Web.Routing</Namespace>
  <Namespace>Newtonsoft.Json.Linq</Namespace>
  <Namespace>Moq</Namespace>
</Query>

void Main()
{
    var httpContextWrapper = this.GetEmptyHttpContextWrapper();
    var routeData = new RouteData();
    var controller = new ProductController();

    var controllerContextMock = new Mock<ControllerContext>(httpContextWrapper, routeData, controller);
	var input = JObject.FromObject(new { Name = "Hammer", Category = "Hardware", Price = 16.99M });
    this.SetupMock(controllerContextMock, ()=> input);

    var valueProvider = new JsonValueProviderFactory().GetValueProvider(controllerContextMock.Object);
    valueProvider.Dump();

    var metadata = ModelMetadataProviders.Current.GetMetadataForType(null, typeof(Product));

    var bindingContext = new ModelBindingContext
    {
        FallbackToEmptyPrefix = true,
        ModelName = nameof(Product),
        ValueProvider = valueProvider,
        ModelMetadata = metadata
    };

    var binder = new ValueProviderResultToProductBinder();

    var instance = binder.BindModel(controllerContextMock.Object, bindingContext) as Product;
    instance.Dump();
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

void SetupMock(Mock<ControllerContext> controllerContextMock, Func<JObject> inputGetter)
{
    controllerContextMock
        .Setup(mock => mock.HttpContext.Request.InputStream)
        .Returns(() =>
        {
            var ms = new MemoryStream();
            var sw = new StreamWriter(ms);
            
            var jO = inputGetter?.Invoke();
            
            sw.Write(jO?.ToString());
            sw.Flush();
            
            ms.Position = 0;
            return ms;
        });
    controllerContextMock
        .Setup(mock => mock.HttpContext.Request.ContentType)
        .Returns (() => "application/json");
}

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Category { get; set; }
    public decimal Price { get; set; }
}

public class ValueProviderResultToProductBinder : WriteablePropertiesModelBinder
{
    protected override void SetValue(object instance, PropertyInfo propertyInfo, ValueProviderResult providerResult)
    {
        if (propertyInfo.PropertyType == typeof(decimal))
        {
            propertyInfo.SetValue(instance, Convert.ToDecimal(providerResult.AttemptedValue), null);
        }
        else
        {
            base.SetValue(instance, propertyInfo, providerResult);
        }
    }
}

public class WriteablePropertiesModelBinder : DefaultModelBinder
{
    public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
    {
        if (bindingContext == null)
        {
            throw new ArgumentNullException(
                nameof(bindingContext),
                $"The expected {nameof(ModelBindingContext)} is not here.");
        }

        var instance = base.BindModel(controllerContext, bindingContext);

		bindingContext = this.ChangeBindingContext(bindingContext);
		instance = this.GetInstance(instance);

        var properties = this.GetProperties(bindingContext);
        foreach (var propertyInfo in properties)
        {
            var providerResult = bindingContext.ValueProvider.GetValue(propertyInfo.Name);
            this.SetValue(instance, propertyInfo, providerResult);
        }

        return instance;
    }

	protected virtual ModelBindingContext ChangeBindingContext(ModelBindingContext bindingContext) => bindingContext;

	protected virtual object GetInstance(object instance) => instance;
	
	protected virtual IEnumerable<PropertyInfo> GetProperties(ModelBindingContext bindingContext)
	{
		var properties = bindingContext.ModelType.GetProperties().Where(a => a.CanWrite);
		return properties;
	}

    protected virtual void SetValue(object instance, PropertyInfo propertyInfo, ValueProviderResult providerResult)
    {
        if (providerResult == null) return;
        propertyInfo.SetValue(instance, providerResult.AttemptedValue, null);
    }
    
    /*
        Fun, depressing fact: as of the year 2020
        there are at least three `IModelBinder` interfaces:
        
        - `System.Web.Mvc.IModelBinder` (the one used here)
        - `System.Web.ModelBinding.IModelBinder` (full-framework Windows?)
        - `System.Web.Http.ModelBinding.IModelBinder` (mostly ASP.NET Web API?)
        
        📖 [ https://stackoverflow.com/questions/21667319/why-is-there-both-a-system-net-http-and-system-web-http-namespace ]
    */
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