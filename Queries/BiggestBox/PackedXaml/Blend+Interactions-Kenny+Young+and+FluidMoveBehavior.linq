<Query Kind="Statements">
  <Reference>&lt;RuntimeDirectory&gt;\WPF\PresentationCore.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\WPF\PresentationFramework.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Xaml.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\WPF\WindowsBase.dll</Reference>
  <NuGetReference>Microsoft.Expression.Blend.SDK.WPF</NuGetReference>
  <NuGetReference>SonghayCore</NuGetReference>
  <Namespace>Songhay.Models</Namespace>
  <Namespace>System.Windows.Controls</Namespace>
</Query>

var name = "Blend+Interactions-Kenny+Young+and+FluidMoveBehavior";
var uri = Util.CurrentQuery.GetLinqPadBiggestBoxPackedXamlUri(name);

var frame = new Frame();
frame.Navigate(uri);
frame.Dump();