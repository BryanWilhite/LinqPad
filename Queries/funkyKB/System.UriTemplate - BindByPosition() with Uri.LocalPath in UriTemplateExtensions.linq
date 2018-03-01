<Query Kind="Program">
  <NuGetReference>System.ServiceModel.Primitives</NuGetReference>
</Query>

void Main()
{
    var template = new UriTemplate(@"..\..\OracleTableMetadata\{tableName}.json");
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

        var originalString = template.ToString();
        if (originalString.StartsWith("."))
        {
            var delimiter = originalString.Contains('/') ? '/' : '\\';
            var array = originalString.Split(delimiter);
            array.Dump();
            if (array.Count(i => i == ".") > 1) throw new FormatException("The relative URI has an incorrect format");
            if (array.Count(i => i == ".") == 1) return $".{delimiter}{result}";
            if (array.Count(i => i == ".") == 0)
                return string.Concat(array
                    .Where(i => i == "..")
                    .Aggregate((a, i) => $"{a}{delimiter}{i}{delimiter}"),
                    result)
                    ;
        }

        return result;
    }
}