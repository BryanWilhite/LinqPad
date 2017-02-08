<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\WPF\PresentationCore.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\WPF\PresentationFramework.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Xaml.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\WPF\WindowsBase.dll</Reference>
  <NuGetReference>Prism</NuGetReference>
  <Namespace>System.Windows.Controls</Namespace>
  <Namespace>System.Windows.Markup</Namespace>
  <Namespace>Microsoft.Practices.Prism.Mvvm</Namespace>
  <Namespace>Microsoft.Practices.Prism.Commands</Namespace>
  <Namespace>System.Windows</Namespace>
  <Namespace>System.Windows.Input</Namespace>
</Query>

void Main()
{
    var xaml = @"
    <UserControl
        xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation""
        xmlns:x=""http://schemas.microsoft.com/winfx/2006/xaml""
        xmlns:sys=""clr-namespace:System;assembly=mscorlib"">
        <UserControl.Resources>
            <Style TargetType=""{x:Type TextBlock}"">
                <Setter Property=""TextWrapping"" Value=""Wrap"" />
            </Style>
            <Style x:Key=""LinkButtonStyle"" TargetType=""{x:Type Button}"">
                <!--
                    Based on “WPF—Link Button Style”
                    from Elangovan's Blog
                    http://blogs.elangovanr.com/post/WPF-Link-Button-Style.aspx
                -->
                <Setter Property=""Template"">
                    <Setter.Value>
                        <ControlTemplate TargetType=""{x:Type Button}"">
                            <TextBlock
                                Text=""{TemplateBinding Property=Content}""
                                TextDecorations=""Underline""
                                />
                        </ControlTemplate>
                    </Setter.Value>
                </Setter>
                <Setter Property=""Foreground"" Value=""Blue"" />
                <Setter Property=""Cursor"" Value=""Hand"" />
                <Style.Triggers>
                    <Trigger Property=""IsMouseOver"" Value=""true"">
                        <Setter Property=""Foreground"" Value=""Red"" />
                    </Trigger>
                </Style.Triggers>
            </Style>
        </UserControl.Resources>
        <Grid>
            <Grid.RowDefinitions>
                <RowDefinition />
                <RowDefinition />
            </Grid.RowDefinitions>
            <Grid Grid.Row=""0"">
                <TextBlock>
                    <Run Text=""The"" />
                    <Hyperlink Command=""{Binding HelloWorldCommand}"">
                        <Run Text=""Hyperlink Button"" />
                    </Hyperlink>
                    <Run Text=""can be displayed inside of a"" />                    
                    <Run FontFamily=""Consolas"" Text=""TextBlock"" />
                    <Run Text=""element because it supports types deriving from"" />
                    <Run FontFamily=""Consolas"" Text=""System.Windows.Documents.Inline"" />
                    <Run Text=""."" />
                </TextBlock>
            </Grid>
            <Grid Grid.Row=""1"">
                <TextBlock>
                    <Run Text=""The"" />
                    <Button
                        Command=""{Binding HelloWorldCommand}""
                        Content=""Link Button""
                        Style=""{StaticResource LinkButtonStyle}""
                        />
                    <Run Text=""is ultimately a historically-essential hack that takes advantage of the ability to nest"" />
                    <Run FontFamily=""Consolas"" Text=""TextBlock"" />
                    <Run Text=""elements."" />
                </TextBlock>
            </Grid>
        </Grid>
    </UserControl>
    ";
    
    var view = (UserControl)XamlReader.Parse(xaml);
    view.DataContext = new MyViewModel();
    view.Dump();
}

public class MyViewModel : BindableBase
{
    public MyViewModel()
    {
        this.HelloWorldCommand = new DelegateCommand(()=>MessageBox.Show("Hello world!"));
    }
    
    public ICommand HelloWorldCommand { get; private set; }
}
/*
    System.Windows.Documents.TextElement is the sub-class
    of System.Windows.Documents.Inline (and Block).

    “The following types inherit from the TextElement class and may be used
    to display the content described in this overview. Bold¹, Figure, Floater,
    Hyperlink¹, InlineUIContainer¹, Italic¹, LineBreak¹, List, ListItem,
    Paragraph, Run¹, Section, Span¹, Table, Underline¹.”
    [¹these are Inline types]
    [https://msdn.microsoft.com/en-us/library/aa970786(v=vs.110).aspx#Types_that_Share_this_Content_Model]
    
    “You can use TextBlock to display small amounts of text.
    If you want to display large amounts of text, use the FlowDocumentReader,
    FlowDocumentPageViewer, or FlowDocumentScrollViewer controls.”
    [https://msdn.microsoft.com/en-us/library/bb613548(v=vs.110).aspx]
    
    “TextBlock is designed to be lightweight, and is geared specifically
    at integrating small portions of flow content into a user interface (UI).
    TextBlock is optimized for single-line display, and provides good performance
    for displaying up to a few lines of content.
    …After TextBlock, FlowDocumentScrollViewer is the next lightest-weight control
    for displaying flow content, and simply provides a scrolling content area with minimal UI.
    FlowDocumentPageViewer is optimized around "page-at-a-time" viewing mode for flow content.
    Finally, FlowDocumentReader supports the richest set functionality
    for viewing flow content, but is correspondingly heavier-weight.”
    [https://msdn.microsoft.com/en-us/library/system.windows.controls.textblock(v=vs.110).aspx]
*/