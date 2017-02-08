<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Net.Http.dll</Reference>
  <NuGetReference>Microsoft.Net.Http</NuGetReference>
  <Namespace>System.IO</Namespace>
  <Namespace>System.Net.Http</Namespace>
</Query>

async void Main()
{
    var client = new HttpClient();
    var response = await client.GetAsync("http://www.weather.gov/xml/current_obs/KLAX.xml");
    var xml = await response.Content.ReadAsStringAsync();
    xml.Dump();
}