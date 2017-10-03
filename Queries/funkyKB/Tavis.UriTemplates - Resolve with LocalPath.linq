<Query Kind="Statements">
  <NuGetReference>Tavis.UriTemplates</NuGetReference>
  <Namespace>Tavis.UriTemplates</Namespace>
</Query>

var tableName = "MY_TABLE";
var template = new UriTemplate(@"OracleTableMetadata\{tableName}.json")
    .AddParameter(nameof(tableName), tableName)
    .Resolve();
template.Dump();

/*
	According to this corefx issue, https://github.com/dotnet/corefx/issues/7983,
	Microsoft will not support the WCF-like UriTemplate in System.ServiceModel.Primitives.
	Even though this NuGet package installs on .NET Core it did not build (for me).
	Nevertheless, Tavis.UriTemplates does not require a special extension method
	to support local paths of a URI.
*/
