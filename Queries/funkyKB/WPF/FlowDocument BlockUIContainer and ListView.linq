<Query Kind="Statements">
  <Reference>&lt;RuntimeDirectory&gt;\WPF\PresentationCore.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\WPF\PresentationFramework.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Xaml.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\WPF\WindowsBase.dll</Reference>
  <Namespace>System.Windows.Controls</Namespace>
  <Namespace>System.Windows.Markup</Namespace>
</Query>

/*
    Flow Document Overview
    https://msdn.microsoft.com/en-us/library/aa970909%28v=vs.110%29.aspx
*/
var xaml = @"
<UserControl
    xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation""
    xmlns:x=""http://schemas.microsoft.com/winfx/2006/xaml""
    xmlns:sys=""clr-namespace:System;assembly=mscorlib"">
    <UserControl.Resources>
        <DataTemplate x:Key=""ListViewItemTemplate"">
            <TextBlock><Run>•&#160;</Run><Run Text=""{Binding Path=.}"" /></TextBlock>
        </DataTemplate>
    </UserControl.Resources>
    <FlowDocumentScrollViewer>
        <FlowDocument FontFamily=""Segoe UI"">
            <Paragraph>
                <Bold FontSize=""32pt""><Run Text=""{Binding Header, Mode=OneWay}"" /></Bold>
            </Paragraph>
            <BlockUIContainer>
                <ListView
                    ItemTemplate=""{StaticResource ListViewItemTemplate}""
                    ItemsSource=""{Binding Items}""
                    />
            </BlockUIContainer>
        </FlowDocument>
    </FlowDocumentScrollViewer>
</UserControl>
";

var view = (UserControl)XamlReader.Parse(xaml);
view.DataContext = new
{
    Header = "List Header",
    Items = new[]
    {
        "This is a “normal” list most developers would use.",
        "Resize the window to see how it does not flow as expected.",
        "This is simply rendered in a box (“block container”), inside the Flow Document.",
    }
};

view.Dump();