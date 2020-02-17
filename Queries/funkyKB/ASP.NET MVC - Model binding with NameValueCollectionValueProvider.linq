<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Net.Http.dll</Reference>
  <NuGetReference>Microsoft.AspNet.Mvc</NuGetReference>
  <Namespace>System.Web.Mvc</Namespace>
  <Namespace>System.Collections.Specialized</Namespace>
</Query>

void Main()
{
    var formCollection = new NameValueCollection
    {
        { "Name", "My Product" }, // the Name (in `NameValueCollection`) has an “empty prefix” (instead of "Product.Name")
        { "Category", "My Category" },
        { "Price", "11.99" },
    };

    /*
        “ValueProviders are created from the HttpContext Request Form, Route, QueryString, etc. properties.
        You can also provide your own value providers…”
        [ https://stackoverflow.com/a/3541185/22944 ]
    */
    var valueProvider = new NameValueCollectionValueProvider(formCollection, null);
    var metadata = ModelMetadataProviders.Current.GetMetadataForType(null, typeof(Product));

    var bindingContext = new ModelBindingContext
    {
        FallbackToEmptyPrefix = true,
        ModelName = nameof(Product),
        ValueProvider = valueProvider,
        ModelMetadata = metadata
    };

    var controllerContext = new ControllerContext();
    var binder = new FormToProductBinder();

    var instance = binder.BindModel(controllerContext, bindingContext) as Product;
    instance.Dump();
}

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Category { get; set; }
    public decimal Price { get; set; }
}

public class FormToProductBinder : ClassModelBinder
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

public class ClassModelBinder : DefaultModelBinder
{
    public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
    {
        if (bindingContext == null)
        {
            throw new ArgumentNullException(
                nameof(bindingContext),
                $"The expected {nameof(ModelBindingContext)} is not here.");
        }

        /*
            Without `ModelBindingContext.FallbackToEmptyPrefix = true`
            `instance` will be null because `NameValueCollection` names
            have no `Product.*` prefix:
        */
        var instance = base.BindModel(controllerContext, bindingContext);

        var properties = bindingContext.ModelType.GetProperties().Where(a => a.CanWrite);
        foreach (var propertyInfo in properties)
        {
            /*
                This pattern appears to have gone away over nine years ago:
                
                    .TryGetValue(propertyInfo.Name, propertyInfo.DeclaringType, out var outValue)
                
                [https://stackoverflow.com/questions/4149805/valueprovider-does-not-contain-a-definition-for-trygetvalue]
                [https://www.hanselman.com/blog/SplittingDateTimeUnitTestingASPNETMVCCustomModelBinders.aspx]
                [https://stackoverflow.com/a/5039199/22944]
            */

            var providerResult = bindingContext.ValueProvider.GetValue(propertyInfo.Name);
            this.SetValue(instance, propertyInfo, providerResult);
        }

        return instance;
    }

    protected virtual void SetValue(object instance, PropertyInfo propertyInfo, ValueProviderResult providerResult)
    {
        if (providerResult == null) return;
        propertyInfo.SetValue(instance, providerResult.AttemptedValue, null);
    }
}