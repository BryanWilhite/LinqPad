<Query Kind="Program">
  <NuGetReference>System.ServiceModel.Primitives</NuGetReference>
</Query>

void Main()
{
    var template = new UriTemplate(@"OracleTableMetadata\{tableName}.json");
    var tableName = "MY_TABLE";
    template.ToLocalPath(tableName).Dump();
}

public static class UriTemplateExtensions
{
    public static string ToLocalPath(this UriTemplate template, params string[] values)
    {
        if (template == null) throw new ArgumentNullException("template", "The expected URI template is not here.");

        var tempDrive = new Uri("t:\\", UriKind.Absolute);
        var result = template
            .BindByPosition(tempDrive, values)
            .LocalPath
            .Replace(tempDrive.OriginalString, string.Empty);
        return result;
    }
}
