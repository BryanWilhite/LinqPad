<Query Kind="Program">
  <NuGetReference>System.Net.Http</NuGetReference>
  <Namespace>System.Net.Http</Namespace>
  <Namespace>System.Net.Http.Headers</Namespace>
</Query>

void Main()
{
    httpClient = new HttpClient();
    httpClient.DefaultRequestHeaders.Dump("by default, request headers are empty");
}

static HttpClient httpClient;

/*
    “Although it implements the IDisposable interface it is actually a shared object.
    This means that under the covers it is reentrant) and thread safe.
    Instead of creating a new instance of HttpClient for each execution
    you should share a single instance of HttpClient
    for the entire lifetime of the application.”
    —Simon Timms
    [https://aspnetmonsters.com/2016/08/2016-08-27-httpclientwrong/]
*/