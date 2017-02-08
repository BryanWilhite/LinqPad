<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\WPF\PresentationCore.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\WPF\PresentationFramework.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Xaml.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\WPF\WindowsBase.dll</Reference>
  <Namespace>System.Windows</Namespace>
  <Namespace>System.Windows.Markup</Namespace>
</Query>

void Main()
{
    var context = new ParserContext();
    context.XmlnsDictionary.Add("", "http://schemas.microsoft.com/winfx/2006/xaml/presentation");
    
    var xaml = @"
<TextBox
    FontFamily=""Consolas""
    FontSize=""32""
    Text=""{Binding Name, Mode=OneWay}""
    />
";
    var element = (FrameworkElement)XamlReader.Parse(xaml, context);
    element.DataContext = new { Name = "Test" };
    
    //PanelManager.StackWpfElement(element);
    
    element.Dump();
}
