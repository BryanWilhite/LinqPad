<Query Kind="Program">
  <NuGetReference>Tavis.UriTemplates</NuGetReference>
  <Namespace>Tavis.UriTemplates</Namespace>
</Query>

void Main()
{
    var apiPath = "api/v2/{BrokerDealerId}/{PartyId}/Household/{HouseholdId}/Accounts";
    var template = new UriTemplate(apiPath);
    template.BindByPosition("BID1124", "PID6677", "HH3456").Dump();
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