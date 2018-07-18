<Query Kind="Program">
  <NuGetReference>Tavis.UriTemplates</NuGetReference>
  <Namespace>Tavis.UriTemplates</Namespace>
</Query>

void Main()
{
    var apiBase = "https://azure.search.windows.net/";
    var apiPath = "{componentName}/{itemName}{?api_version}";
    var template = new UriTemplate(string.Concat(apiBase, apiPath));
    template.BindByPosition("my-component", "my-item", "2015-02-28").Dump();
}

static class UriTemplateExtensions
{
    public static string BindByPosition(this UriTemplate template, params string[] values)
    {
        if (template == null) throw new ArgumentNullException("template", "The expected URI template is not here.");

        var keys = template.GetParameterNames();
        for (int i = 0; i < keys.Count(); i++)
        {
            template.AddParameter(keys.ElementAt(i), values.ElementAtOrDefault(i));
        }

        return template.Resolve();
    }
}