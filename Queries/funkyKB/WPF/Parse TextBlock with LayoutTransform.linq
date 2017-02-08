<Query Kind="Statements">
  <Reference>&lt;RuntimeDirectory&gt;\WPF\PresentationCore.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\WPF\PresentationFramework.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Xaml.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\WPF\WindowsBase.dll</Reference>
  <Namespace>System.Windows.Controls</Namespace>
  <Namespace>System.Windows.Markup</Namespace>
</Query>

var xaml = @"
<UserControl
    xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation"">
    <TextBlock
        Background=""DarkGreen"" Foreground=""WhiteSmoke""
        FontFamily=""Consolas""
        FontSize=""64""
        HorizontalAlignment=""Center"" VerticalAlignment=""Center""
        Text=""Hello World!""
        TextAlignment=""Center"">
        <TextBlock.LayoutTransform>
            <RotateTransform
                Angle=""45""
                CenterX=""0.5"" CenterY=""0.5""
                />
        </TextBlock.LayoutTransform>
    </TextBlock>
</UserControl>
";

var element = (UserControl)XamlReader.Parse(xaml);
element.Dump();