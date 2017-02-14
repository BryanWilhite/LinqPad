<Query Kind="Statements">
  <Connection>
    <ID>ac16a4d3-96f3-48ea-a646-789436ce83cb</ID>
    <Driver>AstoriaAuto</Driver>
    <Server>http://www.nerddinner.com/Services/OData.svc/</Server>
  </Connection>
  <NuGetReference>MoreLinq</NuGetReference>
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <NuGetReference>Songhay.Net.HttpWebRequest</NuGetReference>
  <NuGetReference>SonghayCore</NuGetReference>
  <Namespace>System.Net</Namespace>
  <Namespace>MoreLinq</Namespace>
  <Namespace>Songhay.Extensions</Namespace>
  <Namespace>Songhay.Models</Namespace>
  <Namespace>System.Net</Namespace>
  <Namespace>Newtonsoft.Json.Linq</Namespace>
</Query>

var uri = new Uri("http://www.nerddinner.com/Services/OData.svc/", UriKind.Absolute);

var request = ((HttpWebRequest)WebRequest.Create(uri));

request.Accept = MimeTypes.ApplicationJson;
request.ContentType = MimeTypes.ApplicationJson;

var json = request.DownloadToString();
json.Dump("raw JSON");

var odataApp = JObject.Parse(json);

"".Dump("Collections");
odataApp["d"]
    .ToObject<JObject>()
    .GetJArray("EntitySets", throwException: true)
    .ForEach(i => $"set: {i}".Dump());
