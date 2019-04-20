<Query Kind="Program">
  <NuGetReference>System.Net.Http</NuGetReference>
  <Namespace>System.Net.Http</Namespace>
  <Namespace>System.Net.Http.Headers</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
  <Namespace>Newtonsoft.Json.Linq</Namespace>
</Query>

async void Main()
{
    var request = new HttpRequestMessage(HttpMethod.Get, "https://google.com");
    await request.GetServerResponseAsync().Dump();
}

public static class HttpRequestMessageExtensions
{
    static HttpRequestMessageExtensions()
    {
        LazyHttpClient = new Lazy<HttpClient>(
            () => new HttpClient(),
            LazyThreadSafetyMode.PublicationOnly
        );
    }

    public static async Task<string> GetServerResponseAsync(this HttpRequestMessage request)
    {
        var response = await LazyHttpClient.Value.SendAsync(request);

        var content = await response.Content.ReadAsStringAsync();
        return content;
    }

    public static HttpRequestMessage WithBody(this HttpRequestMessage request, JObject body)
    {
        if (request == null) throw new NullReferenceException($"The expected {nameof(HttpRequestMessage)} is not here.");
        request.Content = new StringContent(body.ToString(), Encoding.UTF8, "application/json");
        return request;
    }

    private static readonly Lazy<HttpClient> LazyHttpClient;
}