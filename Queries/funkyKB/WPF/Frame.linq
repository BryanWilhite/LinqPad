<Query Kind="Statements">
  <Reference>&lt;RuntimeDirectory&gt;\WPF\PresentationCore.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\WPF\PresentationFramework.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Xaml.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\WPF\WindowsBase.dll</Reference>
  <Namespace>System.Windows.Controls</Namespace>
</Query>

var path = Path.Combine(Util.CurrentQuery.GetLinqPadDirectoryInfo().FullName,
  @"Content\Xaml\HelloWorld.xaml");
var uri = new Uri($@"file:///{path}");
var frame = new Frame();
frame.Navigate(uri);
frame.Dump();