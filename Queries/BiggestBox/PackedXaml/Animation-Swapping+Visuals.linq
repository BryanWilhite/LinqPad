<Query Kind="Statements">
  <Reference>&lt;RuntimeDirectory&gt;\WPF\PresentationCore.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\WPF\PresentationFramework.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Xaml.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\WPF\WindowsBase.dll</Reference>
  <NuGetReference>Microsoft.Expression.Blend.SDK.WPF</NuGetReference>
  <Namespace>System.Windows.Controls</Namespace>
</Query>

var name = "Animation-Swapping+Visuals";
var uri = Util.CurrentQuery.GetLinqPadBiggestBoxPackedXamlUri(name);

var frame = new Frame();
frame.Navigate(uri);
frame.Dump();