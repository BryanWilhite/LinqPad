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
  <Reference Relative="..\..\..\Content\dlls\WpfExtensions\WpfExtensions.dll">D:\~dropBox\Dropbox\LinqPad\Content\dlls\WpfExtensions\WpfExtensions.dll</Reference>
  <NuGetReference>SonghayCore</NuGetReference>
  <Namespace>Songhay.Wpf.Extensions</Namespace>
  <Namespace>System.Windows.Controls</Namespace>
</Query>

void Main()
{
    var glyphs = Util.CurrentQuery.LoadBiggestBoxSvgGlyphs("fontawesome-webfont");
    (new ScrollViewer()).WithBiggestBoxSvg(glyphs).Dump();
}