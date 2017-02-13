<Query Kind="Program">
  <Connection>
    <ID>cd884946-dbf0-42d1-ac90-d3a615f50162</ID>
    <Driver>AstoriaAuto</Driver>
    <Server>http://services.odata.org/AdventureWorksV3/AdventureWorks.svc</Server>
  </Connection>
  <Reference>&lt;RuntimeDirectory&gt;\System.Net.Http.dll</Reference>
  <NuGetReference>Simple.OData.Client</NuGetReference>
  <Namespace>Simple.OData.Client</Namespace>
  <Namespace>Simple.OData.Client.Extensions</Namespace>
  <Namespace>System.Linq</Namespace>
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

async void Main()
{
    var client = new ODataClient(new Settings());

    IEnumerable<CompanySale> sales = await client
        .For<CompanySale>()
        .Filter(i => (i.OrderYear == 2008) && (i.ProductCategory == "Clothing"))
        .FindEntriesAsync();

    sales.Dump();
}