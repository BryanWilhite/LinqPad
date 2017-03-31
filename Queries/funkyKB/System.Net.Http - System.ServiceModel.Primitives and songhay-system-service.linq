<Query Kind="Statements">
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <NuGetReference>SonghayCore</NuGetReference>
  <NuGetReference>System.Net.Http</NuGetReference>
  <NuGetReference>System.ServiceModel.Primitives</NuGetReference>
  <Namespace>Newtonsoft.Json.Linq</Namespace>
  <Namespace>Songhay.Extensions</Namespace>
  <Namespace>Songhay.Models</Namespace>
  <Namespace>System.Net.Http</Namespace>
</Query>

/*
    The HTTP Client in use is System.Net.Http (from Microsoft)
    [https://www.nuget.org/packages/System.Net.Http/]
    “Provides a programming interface for modern HTTP applications…”
    
    To get UriTemplate via NuGet, System.ServiceModel.Primitives
    (from the WCF team at Microsoft) is referenced.
    
    NOTE: there are other UriTemplate implementations
    (e.g. UriTemplate.Core) but these do not have a BindByPosition() method
    and are concerned with the RFC 6570 URI Template [https://tools.ietf.org/html/rfc6570].
*/

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