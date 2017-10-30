<Query Kind="Statements">
  <NuGetReference>System.Net.Http</NuGetReference>
  <Namespace>System.Net.Http</Namespace>
  <Namespace>System.Net.Http.Headers</Namespace>
</Query>

/*
    https://chriskirby.net/blog/running-your-azure-webjobs-with-the-kudu-api
*/

var @base = Util.CurrentQuery.GetLinqPadMetaSecret("azure", "b-roll-hook-base");
var hook = Util.CurrentQuery.GetLinqPadMetaSecret("azure", "b-roll-youtube-update-hook");
var claim = Util.CurrentQuery.GetLinqPadMetaSecret("azure", "b-roll-youtube-update-claim");

HttpClient client = new HttpClient();

client.BaseAddress = new Uri(@base, UriKind.Absolute);

// the creds from my .publishsettings file
var byteArray = Encoding.ASCII.GetBytes(claim);

var base64String = Convert.ToBase64String(byteArray);
base64String.Dump();

client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64String);

// POST to the run action for my job
var response = client.PostAsync(hook, null).Result;
response.Dump();