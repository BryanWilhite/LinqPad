<Query Kind="Statements">
  <NuGetReference>System.Web.Http.Common</NuGetReference>
  <Namespace>System.Web.Http.Common</Namespace>
</Query>

// System.Web.Http.Common (from Microsoft)
// http://www.nuget.org/packages/System.Web.Http.Common/
// This (tiny) package contains common libraries used by both ASP.NET MVC and ASP.NET Web API.
try
{	        
    throw new Exception("some web server exception that should not crash the server");
}
catch (Exception ex)
{
    Error.AsWarning(ex).Dump();
}
