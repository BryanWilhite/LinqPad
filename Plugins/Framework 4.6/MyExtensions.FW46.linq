<Query Kind="Program">
  <NuGetReference>MoreLinq</NuGetReference>
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>LINQPad.ObjectModel</Namespace>
  <Namespace>Newtonsoft.Json.Linq</Namespace>
</Query>

void Main()
{
}

public static class DirectoryInfoExtensions
{
    public static DirectoryInfo FindAncestor(this DirectoryInfo info, string ancestorDirectoryName)
    { //see:http://stackoverflow.com/a/29695891/22944
        if (info == null) return null;
        if (info.Parent == null) return null;
        if (info.Parent.Name == ancestorDirectoryName) return info.Parent;
        return info.Parent.FindAncestor(ancestorDirectoryName);
    }
}

public static class QueryExtensions
{
    public static Uri GetLinqPadBiggestBoxPackedXamlUri(this Query query, string fileName)
    {
        var info = query.GetLinqPadDirectoryInfo();
        var path = Path.Combine(info.FullName,
            $@"Content\XAML\BiggestBox PackedXaml\{fileName}.xaml");
        var uri = new Uri($"file:///{path}");
        return uri;
    }

    public static JObject GetLinqPadMeta(this Query query)
    {
        var info = query.GetLinqPadDirectoryInfo();
        var linqPadMetaPath = Path.Combine(info.FullName, "Queries", "LinqPadMeta.json");
        var linqPadMeta = JObject.Parse(File.ReadAllText(linqPadMetaPath));
        return linqPadMeta;
    }

    public static string GetLinqPadMetaSecret(this Query query, string context, string secretName)
    {
        var linqPadMeta = query.GetLinqPadMeta();
        var secretsSet = linqPadMeta["LinqPadMeta"]["secrets"][context].ToObject<Dictionary<string, string>>();
        if (!secretsSet.Keys.Contains(secretName))
            throw new Exception($"key {secretName} is not found; are you on the right device?");

        var root = secretsSet[secretName];
        return root;
    }

    public static string GetLinqPadMetaFolder(this Query query, string folderName)
    {
        var linqPadMeta = query.GetLinqPadMeta();
        var foldersSet = linqPadMeta["LinqPadMeta"]["folders"].ToObject<Dictionary<string, string>>();
        var computerName = Environment.GetEnvironmentVariable("COMPUTERNAME");
        var folderSetKey = $"{computerName}:{folderName}";
        if (!foldersSet.Keys.Contains(folderSetKey))
            throw new Exception($"key {folderSetKey} is not found; are you on the right device?");

        var root = foldersSet[folderSetKey];
        return root;
    }

    public static DirectoryInfo GetLinqPadDirectoryInfo(this Query query)
    {
        var info = new DirectoryInfo(LINQPad.Util.CurrentQueryPath);
        return info.FindAncestor("LinqPad");
    }

    public static string GetLinqPadOpenXmlPath(this Query query, string fileName)
    {
        var info = query.GetLinqPadDirectoryInfo();
        var path = Path.Combine(info.FullName, $@"Content\OpenXml\{fileName}.xml");
        return path;
    }

    public static string LoadHtml(this Query query, string fileName)
    {
        var info = query.GetLinqPadDirectoryInfo();
        var path = Path.Combine(info.FullName, $@"Content\Html\{fileName}");
        return File.ReadAllText(path);
    }

    public static IEnumerable<XElement> LoadBiggestBoxSvgGlyphs(this Query query, string fileName)
    {
        var info = query.GetLinqPadDirectoryInfo();
        var path = Path.Combine(info.FullName, $@"Content\SVG\{fileName}.svg");

        //You cannot stop .NET from condensing entities so load as string:
        var xml = File.ReadAllText(path).Replace("unicode=\"&#", "unicode=\"&amp;#");
        var xDoc = XDocument.Parse(xml);

        XNamespace svg = "http://www.w3.org/2000/svg";
        var glyphs = xDoc.Descendants(svg + "glyph");
        if (!glyphs.Any()) glyphs = xDoc.Descendants("glyph");

        return glyphs;
    }
}

public static class XElementExtensions
{
    public static string ToAttributeValueOrDefault(this XElement element, string attributeName, string defaultValue)
    {
        var s = element.ToAttributeValueOrNull(attributeName);
        if (string.IsNullOrEmpty(s)) return defaultValue;
        return s;
    }

    public static string ToAttributeValueOrNull(this XElement element, string attributeName)
    {
        if (element == null) return null;

        string s = null;
        var attr = element.Attribute(attributeName);
        if (attr != null) s = attr.Value;
        if (string.IsNullOrEmpty(s)) return null;
        return s;
    }
}