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
        { "Name", "My Product" },
        { "Category", "My Category" },
        { "Price", "11.99" },
    };

    var valueProvider = new NameValueCollectionValueProvider(formCollection, null);
    var metadata = ModelMetadataProviders.Current.GetMetadataForType(null, typeof(Product));

    var bindingContext = new ModelBindingContext
    {
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

public class FormToProductBinder : ObjectModelBinder
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

public class ObjectModelBinder : DefaultModelBinder
{
    public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
    {
        if (bindingContext == null)
        {
            throw new ArgumentNullException(
                nameof(bindingContext),
                $"The expected {nameof(ModelBindingContext)} is not here.");
        }

        var instance = Activator.CreateInstance(bindingContext.ModelType);

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
            if (providerResult == null) continue;
            this.SetValue(instance, propertyInfo, providerResult);
        }

        return instance;
    }

    protected virtual void SetValue(object instance, PropertyInfo propertyInfo, ValueProviderResult providerResult)
    {
        propertyInfo.SetValue(instance, providerResult.AttemptedValue, null);
    }
}