<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Net.Http.dll</Reference>
  <NuGetReference>Microsoft.AspNet.Mvc</NuGetReference>
  <Namespace>System.Web.Mvc</Namespace>
  <Namespace>System.Collections.Specialized</Namespace>
</Query>

/*
    [https://stackoverflow.com/a/5039199/22944]
	[https://www.hanselman.com/blog/SplittingDateTimeUnitTestingASPNETMVCCustomModelBinders.aspx]
*/
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
	var binder = new MyBinder();

	var actual = binder.BindModel(controllerContext, bindingContext) as Product;

}

public class Product
{
	public int Id { get; set; }
	public string Name { get; set; }
	public string Category { get; set; }
	public decimal Price { get; set; }
}

public class MyBinder : DefaultModelBinder
{

	public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
	{
		var boundModelObject = base.BindModel(controllerContext, bindingContext);
		boundModelObject.Dump();

		var properties = bindingContext.ModelType.GetProperties().Where(a => a.CanWrite);
		foreach (var propertyInfo in properties)
		{
			var providerResult = bindingContext.ValueProvider.GetValue(propertyInfo.Name);
			//providerResult.Dump();
			//.TryGetValue(propertyInfo.Name, propertyInfo.DeclaringType, out var outValue);
			//propertyInfo.SetValue(boundModelObject, providerResult?.AttemptedValue, null);
		}

		return boundModelObject;
	}
}
