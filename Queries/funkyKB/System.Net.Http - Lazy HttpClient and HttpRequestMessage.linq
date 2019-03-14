<Query Kind="Program">
  <NuGetReference>System.Net.Http</NuGetReference>
  <Namespace>System.Net.Http</Namespace>
  <Namespace>System.Net.Http.Headers</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

async void Main()
{
    var request = new HttpRequestMessage(HttpMethod.Get, new Uri("https://google.com", UriKind.Absolute));
    await request.GetServerResponseAsync().Dump();
}

public static class HttpRequestMessageExtensions
{
    public static async Task<string> GetServerResponseAsync(this HttpRequestMessage request)
    {

        var response = await GetHttpClient().SendAsync(request);

        var content = await response.Content.ReadAsStringAsync();
        return content;

    }

    private static HttpClient GetHttpClient() => HttpClientLazy.Value;

    private static readonly Lazy<HttpClient> HttpClientLazy =
        new Lazy<HttpClient>(() => new HttpClient(), LazyThreadSafetyMode.PublicationOnly);
}
