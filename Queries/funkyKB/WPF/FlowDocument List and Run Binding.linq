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
    <FlowDocumentScrollViewer>
        <FlowDocument FontFamily=""Segoe UI"">
            <Paragraph>
                <Bold FontSize=""32pt""><Run Text=""{Binding Header, Mode=OneWay}"" /></Bold>
            </Paragraph>
            <List x:Name=""FlowDocumentList"" />
        </FlowDocument>
    </FlowDocumentScrollViewer>
</UserControl>
";

var view = (UserControl)XamlReader.Parse(xaml);
view.DataContext = new { Header = "List Header" };

var flowList = (System.Windows.Documents.List)view.FindName("FlowDocumentList");

var flowListItems = new[]
{
    new System.Windows.Documents.Run { Text = "The Flow Document List (FDL) is optimized for typography and graphic design." },
    new System.Windows.Documents.Run { Text = "The FDL has no item-templating support." },
    new System.Windows.Documents.Run { Text = "MVVM-style binding as most developers understand it happens on the inline Run view." },
}
.Select(i =>
{
    var p = new System.Windows.Documents.Paragraph();
    p.Inlines.Add(i);
    return p;
})
.Select(i =>
{
    var li = new System.Windows.Documents.ListItem();
    li.Blocks.Add(i);
    return li;
});

flowList.ListItems.AddRange(flowListItems);

view.Dump();