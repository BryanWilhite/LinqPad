<Query Kind="Statements">
  <Reference>&lt;RuntimeDirectory&gt;\System.ServiceModel.dll</Reference>
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <NuGetReference>SonghayCore</NuGetReference>
  <NuGetReference>System.Net.Http</NuGetReference>
  <Namespace>Newtonsoft.Json.Linq</Namespace>
  <Namespace>Songhay.Extensions</Namespace>
  <Namespace>Songhay.Models</Namespace>
  <Namespace>System.Net.Http</Namespace>
</Query>

// System.Net.Http (from Microsoft)
// https://www.nuget.org/packages/System.Net.Http/
// Provides a programming interface for modern HTTP applications…

var jO = Util.CurrentQuery.GetLinqPadMeta();
jO = (JObject)jO["LinqPadMeta"]["secrets"]["songhay-system-service"];

var apiMetadata = jO["apiMetadata"].ToObject<RestApiMetadata>();

var claimsSet = jO["claims"].ToObject<Dictionary<string, string>>();
var headers = new Dictionary<string, string>
{
    { claimsSet["headerKey"], apiMetadata.ApiKey }
};

var uriTemplate = new UriTemplate(apiMetadata.UriTemplates["hi!"]);
var requestUri = uriTemplate.BindByPosition(apiMetadata.ApiBase);
var requestMessage = new HttpRequestMessage
{
    RequestUri = requestUri,
    Method = HttpMethod.Get
};

headers.ForEachInEnumerable(i => requestMessage.Headers.Add(i.Key, i.Value));

var client = new HttpClient();

HttpResponseMessage response = client.SendAsync(requestMessage).Result;
var content = response.Content.ReadAsStringAsync().Result;
content.Dump();