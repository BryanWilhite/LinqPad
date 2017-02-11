<Query Kind="Program">
  <Reference Relative="..\..\..\Content\dlls\Microsoft.Expression\Microsoft.Expression.Interactions.dll">D:\~dropBox\Dropbox\LinqPad\Content\dlls\Microsoft.Expression\Microsoft.Expression.Interactions.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\WPF\PresentationCore.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\WPF\PresentationFramework.dll</Reference>
  <Reference Relative="..\..\..\Content\dlls\System.Windows.Interactivity\System.Windows.Interactivity.dll">D:\~dropBox\Dropbox\LinqPad\Content\dlls\System.Windows.Interactivity\System.Windows.Interactivity.dll</Reference>
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
    var path = Path.Combine(Util.CurrentQuery.GetLinqPadMetaFolder("visionRoot"), @"webfonts\font-awesome-4.2.0\fonts\FontAwesome.otf");
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