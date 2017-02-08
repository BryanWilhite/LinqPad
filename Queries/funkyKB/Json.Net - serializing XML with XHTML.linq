<Query Kind="Statements">
  <Reference>&lt;RuntimeDirectory&gt;\Microsoft.Build.Framework.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\Microsoft.Build.Tasks.v4.0.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\Microsoft.Build.Utilities.v4.0.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.ComponentModel.DataAnnotations.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Configuration.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Design.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.DirectoryServices.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.DirectoryServices.Protocols.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.EnterpriseServices.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Runtime.Caching.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Security.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.ServiceProcess.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Web.ApplicationServices.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Web.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Web.RegularExpressions.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Web.Services.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Windows.Forms.dll</Reference>
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>System.Web</Namespace>
  <Namespace>Newtonsoft.Json.Linq</Namespace>
</Query>

var xml = @"
<root attr1=""attribute one"" attr2=""attribute two"">
    <title>this is the root</title>
    <xhtml>
        <div id=""wrapper"">
            <p style=""first""><em>hello</em> world!</p>
        </div>
    </xhtml>
</root>
";

var xDoc = XDocument.Parse(xml);

Func<XNode, string> escapeXhtml = node =>
{
    var s = string.Empty;
    StringWriter wr = null;
    try
    {
        wr = new StringWriter();
        using(var jsonWriter = new JsonTextWriter(wr))
        {
            jsonWriter.StringEscapeHandling = StringEscapeHandling.EscapeHtml;
            new JsonSerializer().Serialize(jsonWriter, node.ToString());
        }
        s = wr.ToString();
    }
    finally
    {
        if(wr != null) wr.Dispose();
    }
    return s;
};

var escaped_xhtml = escapeXhtml(xDoc.Root.Element("xhtml").Element("div"));
escaped_xhtml.Dump("escaped xhtml");

var placeholder = "@rx-xhtml";
xDoc.Root.Element("xhtml").Value = placeholder;

var json = JsonConvert.SerializeXNode(xDoc.Root, Newtonsoft.Json.Formatting.Indented);
var json_final =json
    .Replace(string.Format(@"""{0}""",placeholder), escaped_xhtml);   
json_final.Dump("json with escaped xhtml");

var jDoc = JObject.Parse(json_final);
jDoc.ToString().Dump("parsed JSON");
