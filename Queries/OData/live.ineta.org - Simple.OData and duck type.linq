<Query Kind="Program">
  <Reference>D:\~dataRoot\LINQpad\LINQPad Plugins\Songhay.dll</Reference>
  <Reference>D:\~dataRoot\LINQpad\LINQPad Plugins\SonghayCore.dll</Reference>
  <NuGetReference>Simple.OData.Client</NuGetReference>
  <Namespace>Simple.OData.Client</Namespace>
  <Namespace>Simple.OData.Client.Extensions</Namespace>
  <Namespace>Songhay.Extensions</Namespace>
  <Namespace>System.Linq</Namespace>
</Query>

class LiveAuthor
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Bio { get; set; }
}

async void Main()
{
    var client = new ODataClient("http://live.ineta.org/InetaLiveService.svc");
    
    IEnumerable<LiveAuthor> authors = await client
        .For<LiveAuthor>()
        .Filter(i => i.FirstName == "Scott")
        .FindEntriesAsync();
    
    authors.Dump();
}