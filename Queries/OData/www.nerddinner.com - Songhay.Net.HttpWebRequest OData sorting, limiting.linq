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

var builder = new StringBuilder("http://www.nerddinner.com/Services/OData.svc/");
builder.Append("/Dinners()?");
builder.Append("$filter=");
builder.Append("Country eq 'India'");
builder.Append("&");
builder.Append("$orderby=EventDate desc");
builder.Append("&");
builder.Append("$top=5");

var uri = new Uri(builder.ToString(), UriKind.Absolute);
uri.Dump("request URI");

var request = ((HttpWebRequest)WebRequest.Create(uri));

request.Accept = MimeTypes.ApplicationJson;
request.ContentType = MimeTypes.ApplicationJson;

var json = request.DownloadToString();
json.Dump("raw JSON");
