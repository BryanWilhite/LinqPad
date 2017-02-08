<Query Kind="Statements">
  <Reference>&lt;RuntimeDirectory&gt;\WPF\PresentationCore.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\WPF\PresentationFramework.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Xaml.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\WPF\WindowsBase.dll</Reference>
  <Namespace>System.Windows</Namespace>
  <Namespace>System.Windows.Controls</Namespace>
  <Namespace>System.Windows.Markup</Namespace>
  <Namespace>System.Windows.Media</Namespace>
  <Namespace>System.Windows.Shapes</Namespace>
</Query>

var context = new ParserContext();
context.XmlnsDictionary.Add("", "http://schemas.microsoft.com/winfx/2006/xaml/presentation");

var canvas = new Canvas
{
    Background = new SolidColorBrush(Color.FromRgb(255, 255, 0)),
    Width = 320, Height = 240
};

var xaml = @"
<TextBlock
    Canvas.Left=""20"" Canvas.Top=""30"" Panel.ZIndex=""0""
    FontSize=""36pt"" Foreground=""Green""
    Text=""Hello world!""
    />
";

var text = (TextBlock)XamlReader.Parse(xaml, context);
canvas.Children.Add(text);

xaml = @"
<TextBlock
    Canvas.Left=""35"" Canvas.Top=""45"" Panel.ZIndex=""1""
    FontSize=""48pt"" Foreground=""LightGreen""
    Text=""Wow!""
    />
";

text = (TextBlock)XamlReader.Parse(xaml, context);
canvas.Children.Add(text);

canvas.Dump();