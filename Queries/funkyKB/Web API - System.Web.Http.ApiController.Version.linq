<Query Kind="Statements">
  <NuGetReference>Microsoft.AspNet.WebApi.SelfHost</NuGetReference>
  <Namespace>System.Web.Http</Namespace>
</Query>

// ℹ `ApiController` is in 📦 `Microsoft.AspNet.WebApi.SelfHost`
typeof(ApiController).Assembly.GetName().Version.Dump();