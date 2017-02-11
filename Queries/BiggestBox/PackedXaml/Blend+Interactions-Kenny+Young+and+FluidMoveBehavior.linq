<Query Kind="Statements">
  <Reference Relative="..\..\..\Content\dlls\Microsoft.Expression\Microsoft.Expression.Interactions.dll">Microsoft.Expression.Interactions.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\WPF\PresentationCore.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\WPF\PresentationFramework.dll</Reference>
  <Reference Relative="..\..\..\Content\dlls\System.Windows.Interactivity\System.Windows.Interactivity.dll">System.Windows.Interactivity.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Xaml.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\WPF\WindowsBase.dll</Reference>
  <NuGetReference>SonghayCore</NuGetReference>
  <Namespace>Songhay.Models</Namespace>
  <Namespace>System.Windows.Controls</Namespace>
</Query>

var name = "Blend+Interactions-Kenny+Young+and+FluidMoveBehavior";
var uri = Util.CurrentQuery.GetLinqPadBiggestBoxPackedXamlUri(name);

var frame = new Frame();
frame.Navigate(uri);
frame.Dump();