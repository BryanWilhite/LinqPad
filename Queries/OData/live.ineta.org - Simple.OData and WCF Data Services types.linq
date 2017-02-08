<Query Kind="Program">
  <Connection>
    <ID>cf93aefd-6d80-4e18-9a9f-ca5356d175c3</ID>
    <Persist>true</Persist>
    <Driver>AstoriaAuto</Driver>
    <Server>http://live.ineta.org/InetaLiveService.svc/</Server>
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
        this.AfterResponse = request =>
        {
            request.Dump("Response");
            request.Content.ReadAsStringAsync().Dump("Response: Content");
        };
        
        this.BeforeRequest = request =>
        {
            request.Dump("Request");
        };
        this.UrlBase = "http://live.ineta.org/InetaLiveService.svc";
    }
}

async void Main()
{
    var client = new ODataClient(new Settings());
    
    IEnumerable<LiveVideo> videos = await client
        .For<LiveVideo>()
        .Expand(i => i.LivePresentation)
        .Filter(i => i.LivePresentation != null)
        .FindEntriesAsync();
    
    videos.Dump();
}