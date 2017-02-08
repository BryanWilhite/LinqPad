<Query Kind="Program">
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>Newtonsoft.Json.Linq</Namespace>
</Query>

void Main()
{
    var json = @"
{
    ""items"": [ ""one"", ""two"", ""three"" ]
}
";

    var jO = JObject.Parse(json);

    jO.ToString().Dump();

    var jA = jO.GetJArray("items", throwException: true);
    var jA_clone = jA.DeepClone();

    jA.Add("four");
    jA.ToString().Dump("original JArray");
    var e = jA.ElementAtOrDefault(3);
    if(e != null) e.ToString().Dump("4th element");

    jA_clone.ToString().Dump("cloned JArray");
    var e_clone = jA_clone.ElementAtOrDefault(3);
    if (e_clone != null) e_clone.ToString().Dump("4th element");
}

static class JObjectExtensions
{
    public static JArray GetJArray(this JObject jsonObject, string arrayPropertyName, bool throwException)
    {
        var token = jsonObject.GetJToken(arrayPropertyName, throwException);
        JArray jsonArray = null;
        if (token.HasValues) jsonArray = (JArray)token;
        else if (throwException) throw new FormatException(string.Format("The expected array “{0}” is not here.", arrayPropertyName));

        return jsonArray;
    }

    public static JToken GetJToken(this JObject jsonObject, string objectPropertyName, bool throwException)
    {
        if ((jsonObject == null) && !throwException) return null;
        if ((jsonObject == null) && throwException) throw new ArgumentNullException("jsonObject", "The expected JObject is not here.");
        if (string.IsNullOrEmpty(objectPropertyName)) throw new ArgumentNullException("objectPropertyName", "The expected property name is not here.");

        JToken token = null;
        if (!jsonObject.TryGetValue(objectPropertyName, out token) && throwException)
            throw new FormatException(string.Format("The expected property name “{0}” is not here.", objectPropertyName));

        return token;
    }
}