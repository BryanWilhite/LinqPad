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
context.XmlnsDictionary.Add("x", "http://schemas.microsoft.com/winfx/2006/xaml");

var g = new EllipseGeometry{ RadiusX = 64, RadiusY = 32 };
var p = PathGeometry.CreateFromGeometry(g);
var xaml = @"
<StackPanel Background=""Yellow"" Width=""320"">
    <StackPanel.Resources>
        <Style x:Key=""MarginStyle"" TargetType=""{{x:Type FrameworkElement}}"">
            <Setter Property=""Margin"" Value=""4,16"" />
        </Style>
    </StackPanel.Resources>
    <TextBlock TextWrapping=""Wrap"">
        <Run FontWeight=""Bold"">StreamGeometry:</Run>
        <Run>{0}</Run>
    </TextBlock>
    <Path
        Data=""{0}""
        Fill=""Black""
        Stretch=""Uniform""
        Style=""{{StaticResource MarginStyle}}""
        />
    <Rectangle
        Fill=""Black""
        Stretch=""Fill""
        Style=""{{StaticResource MarginStyle}}""
        Width=""128"" Height=""64"">
        <Rectangle.Clip>
            <PathGeometry Figures=""{0}"">
                <PathGeometry.Transform>
                    <TranslateTransform X=""64"" Y=""32"" />
                </PathGeometry.Transform>
            </PathGeometry>
        </Rectangle.Clip>
    </Rectangle>
</StackPanel>
";

xaml = string.Format(xaml, p.ToString());
var panel = (StackPanel)XamlReader.Parse(xaml, context);
panel.Dump();