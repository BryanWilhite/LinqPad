<Query Kind="Statements">
  <NuGetReference>Simple.OData.Client</NuGetReference>
  <Namespace>Simple.OData.Client</Namespace>
  <Namespace>Simple.OData.Client.Extensions</Namespace>
  <Namespace>System.Linq</Namespace>
</Query>

var client = new ODataClient("http://services.odata.org/AdventureWorksV3/AdventureWorks.svc");

var x = ODataDynamic.Expression;

IEnumerable<dynamic> companySales = await client.For(x.CompanySales)
    .Filter((x.OrderYear == 2008) && (x.ProductCategory == "Clothing"))
    .FindEntriesAsync();

foreach (var dyn in companySales)
{
    var anonym = new
    {
        OrderYear = dyn.OrderYear,
        ProductCategory = dyn.ProductCategory,
        ProductSubCategory = dyn.ProductSubCategory,
        Sales = dyn.Sales
    };
    anonym.Dump();
}