<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Net.Http.dll</Reference>
  <NuGetReference>Microsoft.AspNet.Mvc</NuGetReference>
  <Namespace>System.Web.Mvc</Namespace>
</Query>

/*
    [ https://stackoverflow.com/a/3009807/22944 ]
*/
void Main()
{
    typeof(Controller).Assembly.GetName().Version.Dump();
}
