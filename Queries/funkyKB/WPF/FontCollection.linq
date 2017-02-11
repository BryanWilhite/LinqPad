<Query Kind="Program">
  <Reference Relative="..\..\..\Plugins\Microsoft.Expression.Interactions.dll">Microsoft.Expression.Interactions.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\WPF\PresentationCore.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\WPF\PresentationFramework.dll</Reference>
  <Reference Relative="..\..\..\Content\dlls\System.Windows.Interactivity\System.Windows.Interactivity.dll">System.Windows.Interactivity.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Xaml.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\WPF\WindowsBase.dll</Reference>
  <NuGetReference>MoreLinq</NuGetReference>
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>MoreLinq</Namespace>
  <Namespace>Newtonsoft.Json.Linq</Namespace>
  <Namespace>System.Drawing.Text</Namespace>
  <Namespace>System.Net</Namespace>
  <Namespace>System.Windows.Controls</Namespace>
  <Namespace>System.Windows.Markup</Namespace>
  <Namespace>System.Windows.Media</Namespace>
</Query>

string GetVisionRoot()
{
    var linqPadQueryInfo = new DirectoryInfo(Path.GetDirectoryName(Util.CurrentQueryPath));
    var linqPadMetaPath = Path.Combine(linqPadQueryInfo.Parent.Parent.FullName, "LinqPadMeta.json");
    var linqPadMeta = JObject.Parse(File.ReadAllText(linqPadMetaPath));
    var foldersSet = linqPadMeta["LinqPadMeta"]["folders"].ToObject<Dictionary<string, string>>();
    var folderSetKey = string.Format("{0}:{1}", Environment.GetEnvironmentVariable("COMPUTERNAME"), "visionRoot");
    if (!foldersSet.Keys.Contains(folderSetKey)) throw new Exception(string.Format("key {0} is not found; are you on the right device?", folderSetKey));
    
    var visionRoot = foldersSet[folderSetKey];
    return visionRoot;
}

void Main()
{
    var osData = new InstalledFontCollection();
    osData.Dump();
    
    var fontFamily = osData.Families.First(i => i.Name == "Consolas");
    fontFamily.Dump();

    /*
        http://fortawesome.github.io/Font-Awesome/cheatsheet/
    */
    var localData = new PrivateFontCollection();
    var path = Path.Combine(GetVisionRoot(), @"webfonts\font-awesome-4.2.0\fonts\FontAwesome.otf");
    localData.AddFontFile(path);
    localData.Dump();
    
    var xaml = @"
    <UserControl
        xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation"">
        <TextBox
            FontSize=""64""
            Text=""{Binding Glyph, Mode=OneWay}""
            />
    </UserControl>
    ";
    
    var control = (UserControl)XamlReader.Parse(xaml);
    control.DataContext = new { Glyph = WebUtility.HtmlDecode("&#xf1fe;") };
    control.FontFamily = new FontFamily(localData.Families[0].Name);
    control.Dump();
}