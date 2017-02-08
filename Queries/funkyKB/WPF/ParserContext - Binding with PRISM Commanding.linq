<Query Kind="Statements">
  <Reference>&lt;RuntimeDirectory&gt;\WPF\PresentationCore.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\WPF\PresentationFramework.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Xaml.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\WPF\WindowsBase.dll</Reference>
  <NuGetReference>Prism</NuGetReference>
  <Namespace>Microsoft.Practices.Prism.Commands</Namespace>
  <Namespace>System.Windows</Namespace>
  <Namespace>System.Windows.Markup</Namespace>
  <Namespace>System.Windows.Controls</Namespace>
</Query>

var context = new ParserContext();
context.XmlnsDictionary.Add("", "http://schemas.microsoft.com/winfx/2006/xaml/presentation");

var xaml = @"
<StackPanel Width=""240"">
    <Button
        Command=""{Binding Command}""
        Content=""Run Command""
        />
    <TextBox
        FontFamily=""Consolas""
        FontSize=""32""
        Text=""{Binding Name, Mode=OneWay}""
        />
</StackPanel>
";

var panel = (StackPanel)XamlReader.Parse(xaml, context);

var command = new DelegateCommand(()=>
{
    var textBox = panel.Children.OfType<TextBox>().First();
    textBox.Text = "Tested!";
});

var viewModel = new
{
    Command = command,
    Name = "Test"
};

panel.DataContext = viewModel;
panel.Dump();