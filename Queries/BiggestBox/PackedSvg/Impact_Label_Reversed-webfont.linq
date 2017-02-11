<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\Accessibility.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\wpf\PresentationCore.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\WPF\PresentationFramework.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\wpf\PresentationUI.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\wpf\ReachFramework.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Configuration.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Deployment.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\wpf\System.Printing.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Xaml.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\wpf\UIAutomationProvider.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\wpf\UIAutomationTypes.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\wpf\WindowsBase.dll</Reference>
  <Reference Relative="..\..\..\Content\dlls\WpfExtensions\WpfExtensions.dll">C:\Users\wilhiteb\Dropbox\LinqPad\Content\dlls\WpfExtensions\WpfExtensions.dll</Reference>
  <NuGetReference>MoreLinq</NuGetReference>
  <NuGetReference>SonghayCore</NuGetReference>
  <Namespace>MoreLinq</Namespace>
  <Namespace>Songhay.Extensions</Namespace>
  <Namespace>Songhay.Wpf.Extensions</Namespace>
  <Namespace>System.Drawing.Text</Namespace>
  <Namespace>System.Windows</Namespace>
  <Namespace>System.Windows.Controls</Namespace>
  <Namespace>System.Windows.Documents</Namespace>
  <Namespace>System.Windows.Markup</Namespace>
  <Namespace>System.Windows.Media</Namespace>
</Query>

void Main()
{
    var glyphs = Util.CurrentQuery.LoadBiggestBoxSvgGlyphs("Impact_Label_Reversed-webfont");
    (new ScrollViewer()).WithBiggestBoxSvg(glyphs).Dump();
}
