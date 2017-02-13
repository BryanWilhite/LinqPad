<Query Kind="Statements">
  <Reference>&lt;RuntimeDirectory&gt;\WPF\PresentationCore.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\WPF\PresentationFramework.dll</Reference>
  <Reference Relative="..\..\..\Content\DLLs\WpfResources\Songhay.Portable.Resources.dll">Songhay.Portable.Resources.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Xaml.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\WPF\WindowsBase.dll</Reference>
  <NuGetReference>Microsoft.Expression.Blend.SDK.WPF</NuGetReference>
  <Namespace>System.Drawing.Text</Namespace>
  <Namespace>System.Net</Namespace>
  <Namespace>System.Windows.Controls</Namespace>
  <Namespace>System.Windows.Markup</Namespace>
  <Namespace>System.Windows.Media</Namespace>
</Query>

var dll = Assembly.GetAssembly(typeof(Songhay.Portable.Resources.ResourceModule));
dll.GetManifestResourceNames().Dump();

var resourceName = dll.GetManifestResourceNames().First(i => i.EndsWith("FontAwesome.otf"));
using (var stream = dll.GetManifestResourceStream(resourceName))
{
    var localData = new PrivateFontCollection();
    var fontdata = new byte[stream.Length];
    stream.Read(fontdata, 0, (int)stream.Length);
    unsafe
    {
        fixed(byte * pFontData = fontdata)
        {
            localData.AddMemoryFont((System.IntPtr)pFontData, fontdata.Length);
        }
    }
    localData.Dump();
    
    var textBox = new TextBox
    {
        FontFamily = new System.Windows.Media.FontFamily(localData.Families.First().Name),
        FontSize = 64,
        Text = System.Net.WebUtility.HtmlDecode("&#xf09b;")
    };
    textBox.Dump();
}

var xaml = @"
<UserControl
    xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation"">
    <TextBox
        FontFamily=""/Songhay.Portable.Resources;Component/Fonts/#FontAwesome""
        FontSize=""64""
        Text=""&#xf09b;""
        />
</UserControl>
";

var control = (UserControl)XamlReader.Parse(xaml);
control.Dump();