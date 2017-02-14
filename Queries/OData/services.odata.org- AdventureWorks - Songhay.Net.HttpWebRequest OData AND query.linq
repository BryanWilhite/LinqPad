<Query Kind="Statements">
  <Connection>
    <ID>cd884946-dbf0-42d1-ac90-d3a615f50162</ID>
    <Driver>AstoriaAuto</Driver>
    <Server>http://services.odata.org/AdventureWorksV3/AdventureWorks.svc</Server>
  </Connection>
  <NuGetReference>MoreLinq</NuGetReference>
  <NuGetReference>Songhay.Net.HttpWebRequest</NuGetReference>
  <NuGetReference>SonghayCore</NuGetReference>
  <Namespace>MoreLinq</Namespace>
  <Namespace>Songhay.Extensions</Namespace>
  <Namespace>System.Net</Namespace>
  <Namespace>Songhay.Models</Namespace>
</Query>

/*
CompanySales
    .Where(i => i.OrderYear == 2008)
    .Where(i => i.ProductCategory == "Clothing")
*/
var builder = new StringBuilder("http://services.odata.org/AdventureWorksV3/AdventureWorks.svc");
builder.Append("/CompanySales()?");
builder.Append("$filter=");
builder.Append("OrderYear eq 2008");
builder.Append(" and ");
builder.Append("ProductCategory eq 'Clothing'");

var uri = new Uri(builder.ToString(), UriKind.Absolute);
uri.Dump("request URI");

var request = ((HttpWebRequest)WebRequest.Create(uri));

request.Accept = MimeTypes.ApplicationAtomXml;
request.ContentType = MimeTypes.ApplicationXml;

var xml = request.DownloadToString();

var odataApp = XDocument.Parse(xml);
odataApp.Dump();