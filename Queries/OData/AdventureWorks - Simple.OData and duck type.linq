<Query Kind="Program">
  <Connection>
    <ID>cd884946-dbf0-42d1-ac90-d3a615f50162</ID>
    <Persist>true</Persist>
    <Driver>AstoriaAuto</Driver>
    <Server>http://services.odata.org/AdventureWorksV3/AdventureWorks.svc</Server>
  </Connection>
  <Reference Relative="..\..\LINQPad Plugins\Songhay.dll">D:\~dataRoot\LINQpad\LINQPad Plugins\Songhay.dll</Reference>
  <Reference Relative="..\..\LINQPad Plugins\SonghayCore.dll">D:\~dataRoot\LINQpad\LINQPad Plugins\SonghayCore.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Net.Http.dll</Reference>
  <NuGetReference>Simple.OData.Client</NuGetReference>
  <Namespace>Simple.OData.Client</Namespace>
  <Namespace>Simple.OData.Client.Extensions</Namespace>
  <Namespace>Songhay.Extensions</Namespace>
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
        
        this.UrlBase = "http://services.odata.org/AdventureWorksV3/AdventureWorks.svc";
    }
}

class CompanySale:vCompanySales
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