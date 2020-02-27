<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Web.dll</Reference>
  <NuGetReference>Microsoft.AspNet.WebApi.SelfHost</NuGetReference>
  <Namespace>System.Web.Http</Namespace>
  <Namespace>System.Net</Namespace>
  <Namespace>System.Web.Routing</Namespace>
  <Namespace>System.Web.Http.SelfHost</Namespace>
  <Namespace>System.Net.Http</Namespace>
</Query>

void Main()
{
    /*
        📖 “Self-Host ASP.NET Web API 1 (C#)”
        [ https://docs.microsoft.com/en-us/aspnet/web-api/overview/older-versions/self-host-a-web-api ]
    */
    var uri = new Uri("http://localhost/api/products");
    var requestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

    var config = new HttpSelfHostConfiguration("http://localhost/");
    config.Routes.MapHttpRoute(
        "API Default",
        "api/{controller}/{id}",
        new { id = RouteParameter.Optional });
    
    var routeData = config.Routes.GetRouteData(requestMessage);
    routeData.Dump();
}
