<Query Kind="Statements">
  <NuGetReference>Google.Apis</NuGetReference>
  <NuGetReference>Google.Apis.Oauth2.v2</NuGetReference>
  <NuGetReference>Google.Apis.YouTube.v3</NuGetReference>
  <Namespace>Google.Apis.Auth.OAuth2</Namespace>
  <Namespace>Google.Apis.Services</Namespace>
  <Namespace>Google.Apis.YouTube.v3</Namespace>
</Query>

// https://www.nuget.org/packages/Google.Apis/
// https://github.com/google/google-api-dotnet-client
// https://developers.google.com/api-client-library/dotnet/apis/
// https://developers.google.com/youtube/v3/code_samples/dotnet
// https://console.developers.google.com/apis/credentials?project=b-roll-play-youtube

var user = Util.CurrentQuery.GetLinqPadMetaSecret("google", "user");
var scopes = new[] { YouTubeService.Scope.YoutubeReadonly };
var secrets = new ClientSecrets
{
    ClientId = Util.CurrentQuery.GetLinqPadMetaSecret("google", "clientId"),
    ClientSecret = Util.CurrentQuery.GetLinqPadMetaSecret("google", "clientSecret")
};
var credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
    secrets,
    scopes,
    user,
    CancellationToken.None
    ).Result;

var serviceInitializer = new BaseClientService.Initializer
{
    HttpClientInitializer = credential,
    ApplicationName = Util.CurrentQuery.GetLinqPadMetaSecret("google", "applicationName")
};

var service = new YouTubeService(serviceInitializer);
var request = service.Search.List("snippet");
request.ChannelId = "UCPeVq5Jb_BlNyaeii-ve5qg";
request.MaxResults = 3;
request.Type = "video";
request.Order = SearchResource.ListRequest.OrderEnum.Date;
var response = request.ExecuteAsync().Result;
response.Dump();