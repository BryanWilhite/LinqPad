<Query Kind="Program">
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

void Main()
{
    var context = new ParserContext();
    context.XmlnsDictionary.Add("", "http://schemas.microsoft.com/winfx/2006/xaml/presentation");

    var g = new EllipseGeometry{ RadiusX = 64, RadiusY = 32 };
    var p = PathGeometry.CreateFromGeometry(g);
    var data = p.ToString();
 
    var xaml = $@"
<Canvas Background=""Yellow"" Width=""128"" Height=""64"">
    <Path
        Data=""{data}""
        Fill=""Black""
        Stretch=""Uniform""
        Width=""128"" Height=""64""
        />
</Canvas>
";
    //data.Dump();
    var element = (Canvas)XamlReader.Parse(xaml, context);
    element.Dump();
}