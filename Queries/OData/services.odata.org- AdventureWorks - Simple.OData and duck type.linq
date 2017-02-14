<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Net.Http.dll</Reference>
  <NuGetReference>Microsoft.Bcl.Async</NuGetReference>
  <NuGetReference>Simple.OData.Client</NuGetReference>
  <Namespace>Simple.OData.Client</Namespace>
  <Namespace>Simple.OData.Client.Extensions</Namespace>
  <Namespace>System.Linq</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

class Settings : ODataClientSettings
{
    public Settings()
    {
        this.BeforeRequest = request =>
        {
            request.Dump("Request");
        };

        this.BaseUri = new Uri("http://services.odata.org/AdventureWorksV3/AdventureWorks.svc", UriKind.Absolute);
    }
}

class CompanySale : vCompanySales
{
}

async Task Main()
{
    V3Adapter.Reference();
    var client = new ODataClient(new Settings());

    IEnumerable<CompanySale> sales = await client
        .For<CompanySale>()
        .Filter(i => (i.OrderYear == 2008) && (i.ProductCategory == "Clothing"))
        .FindEntriesAsync();

    sales.Dump();
}