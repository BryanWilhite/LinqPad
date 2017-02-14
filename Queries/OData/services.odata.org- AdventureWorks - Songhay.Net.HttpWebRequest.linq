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
  <Namespace>Songhay.Models</Namespace>
  <Namespace>System.Net</Namespace>
</Query>

var uri = new Uri("http://services.odata.org/AdventureWorksV3/AdventureWorks.svc", UriKind.Absolute);

var request = ((HttpWebRequest)WebRequest.Create(uri));

request.Accept = MimeTypes.ApplicationXml;
request.ContentType = MimeTypes.ApplicationXml;

var xml = request.DownloadToString();

var odataApp = XDocument.Parse(xml);
odataApp.Dump();

XNamespace app = "http://www.w3.org/2007/app";
XNamespace atom = "http://www.w3.org/2005/Atom";

"".Dump("Collections");
odataApp.Root
    .Elements(app + "workspace")
    .Elements(app + "collection")
    .ForEach(i =>
    {
        var href = i.Attribute("href").Value;
        var title = i.Element(atom + "title").Value;
        $"title: {title}; href: {href}".Dump();
    });