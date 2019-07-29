<Query Kind="Program">
  <NuGetReference>System.Net.Http</NuGetReference>
  <NuGetReference>Microsoft.Extensions.DependencyInjection</NuGetReference>
  <NuGetReference>Microsoft.Extensions.Http</NuGetReference>
  <Namespace>System.Net.Http</Namespace>
  <Namespace>Microsoft.Extensions.DependencyInjection</Namespace>
</Query>

void Main()
{
    var factory = provider.GetService<IHttpClientFactory>();
    var httpClient = factory.CreateClient();
    httpClient.DefaultRequestHeaders.Dump("by default, request headers are empty");
}

static IServiceProvider provider = new ServiceCollection().AddHttpClient().BuildServiceProvider();

/*
    see “Use HttpClientFactory to implement resilient HTTP requests”
    [https://docs.microsoft.com/en-us/dotnet/standard/microservices-architecture/implement-resilient-applications/use-httpclientfactory-to-implement-resilient-http-requests]
*/