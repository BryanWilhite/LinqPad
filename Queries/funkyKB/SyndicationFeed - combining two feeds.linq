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

//reference: http://stackoverflow.com/questions/79197/combining-two-syndicationfeeds

SyndicationFeed feed;
SyndicationFeed feed2;

var feedUri = new Uri("http://stackoverflow.com/feeds/tag/jquery");
using (var reader = XmlReader.Create(feedUri.AbsoluteUri))
{
    feed = SyndicationFeed.Load(reader);
}

if(feed.Items.Count() <= 0) throw new Exception("The expected feed items are not here.");

var feed2Uri = new Uri("http://stackoverflow.com/feeds/tag/wpf");
using (var reader2 = XmlReader.Create(feed2Uri.AbsoluteUri))
{
    feed2 = SyndicationFeed.Load(reader2);
}

if(feed2.Items.Count() <= 0) throw new Exception("The expected feed items are not here.");

var feedsCombined = new SyndicationFeed(feed.Items.Union(feed2.Items));

if(!(feedsCombined.Items.Count() == feed.Items.Count() + feed2.Items.Count()))
    throw new Exception("The expected number of combined feed items are not here.");

var builder = new StringBuilder();
using (var writer = XmlWriter.Create(builder))
{
    feedsCombined.SaveAsRss20(writer);
    writer.Flush();
    writer.Close();
}

var xmlString = builder.ToString();
xmlString.Dump();

if(!(new Func<bool>(
    () =>
    {
        var test = false;

        var xDoc = XDocument.Parse(xmlString);
        var count = xDoc.Root.Element("channel").Elements("item").Count();
        test = (count == feedsCombined.Items.Count());

        return test;
    }
).Invoke())) throw new Exception("The expected number of RSS items are not here.");