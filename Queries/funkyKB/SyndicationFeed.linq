<Query Kind="Statements">
  <Reference>&lt;RuntimeDirectory&gt;\System.ServiceModel.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Xaml.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.ServiceModel.Internals.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Runtime.Serialization.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Configuration.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\SMDiagnostics.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.IdentityModel.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.DirectoryServices.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\Microsoft.Transactions.Bridge.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Web.Services.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.EnterpriseServices.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.IdentityModel.Selectors.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Web.ApplicationServices.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Messaging.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Runtime.DurableInstancing.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.ServiceProcess.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Net.Http.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.ServiceModel.Activation.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Security.dll</Reference>
  <Namespace>System.ServiceModel.Syndication</Namespace>
</Query>

//reference: [http://dotnetslackers.com/articles/aspnet/How-to-create-a-syndication-feed-for-your-website.aspx]

var items = new List<SyndicationItem>
{
    new SyndicationItem
    {
        Content = TextSyndicationContent.CreatePlaintextContent("This is plain test content for first item."),
        PublishDate = DateTime.Now,
        Summary = TextSyndicationContent.CreatePlaintextContent("Item summary for first item…"),
        Title = TextSyndicationContent.CreatePlaintextContent("First Item Title")
    },
    new SyndicationItem
    {
        Content = TextSyndicationContent.CreatePlaintextContent("This is plain test content for second item."),
        PublishDate = DateTime.Now,
        Summary = TextSyndicationContent.CreatePlaintextContent("Item summary for second item…"),
        Title = TextSyndicationContent.CreatePlaintextContent("Second Item Title")
    },
    new SyndicationItem
    {
        Content = TextSyndicationContent.CreateXhtmlContent("This is <strong>XHTML</strong> test content for <em>third</em> item."),
        PublishDate = DateTime.Now,
        Summary = TextSyndicationContent.CreatePlaintextContent("Item summary for third item…"),
        Title = TextSyndicationContent.CreatePlaintextContent("Third Item Title")
    }
};

var feed = new SyndicationFeed(items)
{
    Description = TextSyndicationContent.CreatePlaintextContent("My feed description."),
    Title = TextSyndicationContent.CreatePlaintextContent("My Feed")
};

feed.Authors.Add(new SyndicationPerson
{
    Email = "rasx@songhaysystem.com",
    Name = "Bryan Wilhite",
    Uri = "http://SonghaySystem.com"
});

if((new List<SyndicationItem>(feed.Items)).Count != 3) throw new Exception("The expected number of Syndication items is not here.");

feed.Items.ToList().ForEach(i =>
{
    i.Authors.Add(feed.Authors.First());
});

var formatter = new Rss20FeedFormatter(feed);

var settings = new XmlWriterSettings
{
    CheckCharacters = true,
    CloseOutput = true,
    ConformanceLevel = ConformanceLevel.Document,
    Encoding = Encoding.UTF8,
    Indent = true,
    IndentChars = "    ",
    NamespaceHandling = NamespaceHandling.OmitDuplicates,
    NewLineChars = "\r\n",
    NewLineHandling = NewLineHandling.Replace,
    NewLineOnAttributes = true,
    OmitXmlDeclaration = false
};

var buffer = new StringBuilder();
var output = string.Empty;
using (var writer = XmlWriter.Create(buffer, settings))
{
    formatter.WriteTo(writer); // or feed.SaveAsRss20(writer);
    writer.Flush();
    writer.Close();
    output = buffer.ToString();
}
output.Dump();

if(output.Equals(string.Empty)) throw new Exception("The expected output is not here.");