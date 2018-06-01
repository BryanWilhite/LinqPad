<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\WPF\PresentationCore.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\WPF\PresentationFramework.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Xaml.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\WPF\WindowsBase.dll</Reference>
  <NuGetReference>Prism.WPF</NuGetReference>
  <Namespace>System.Windows.Controls</Namespace>
  <Namespace>System.Windows.Markup</Namespace>
  <Namespace>Prism.Mvvm</Namespace>
  <Namespace>Prism.Commands</Namespace>
  <Namespace>System.Windows</Namespace>
  <Namespace>System.Windows.Input</Namespace>
</Query>

// ref: [http://stackoverflow.com/questions/1287995/whats-the-difference-between-contentcontrol-and-contentpresenter]
void Main()
{
    var xaml = @"
    <UserControl
        xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation""
        xmlns:x=""http://schemas.microsoft.com/winfx/2006/xaml""
        xmlns:sys=""clr-namespace:System;assembly=mscorlib"">
        <UserControl.Resources>
            <Style TargetType=""{x:Type TextBlock}"">
                <Setter Property=""Margin"" Value=""16"" />
                <Setter Property=""TextWrapping"" Value=""Wrap"" />
                <Setter Property=""Width"" Value=""128"" />
            </Style>
            <Style x:Key=""DefaultRectangleStyle"" TargetType=""{x:Type Rectangle}"">
                <Setter Property=""Height"" Value=""64"" />
                <Setter Property=""Width"" Value=""64"" />
            </Style>
            <ControlTemplate x:Key=""ControlTemplateOne"" TargetType=""{x:Type ContentControl}"">
                <StackPanel Orientation=""Horizontal"">
                    <Rectangle Fill=""Blue"" Style=""{StaticResource DefaultRectangleStyle}"" />
                    <ContentPresenter />
                </StackPanel>
            </ControlTemplate>
            <ControlTemplate x:Key=""ControlTemplateTwo"" TargetType=""{x:Type ContentControl}"">
                <StackPanel Orientation=""Horizontal"">
                    <ContentPresenter />
                    <Rectangle Fill=""Red"" Style=""{StaticResource DefaultRectangleStyle}"" />
                </StackPanel>
            </ControlTemplate>
        </UserControl.Resources>
        <Grid Width=""512"">
            <Grid.RowDefinitions>
                <RowDefinition />
                <RowDefinition />
                <RowDefinition />
            </Grid.RowDefinitions>
            <ContentControl Grid.Row=""0""
                Template=""{StaticResource ControlTemplateOne}"">
                <TextBlock Text=""{Binding DisplayText}"" />
            </ContentControl>
            <ContentControl Grid.Row=""1""
                Template=""{StaticResource ControlTemplateOne}"">
                <TextBlock Text=""{Binding DisplayTextTwo}"" />
            </ContentControl>
            <ContentControl Grid.Row=""2""
                Template=""{StaticResource ControlTemplateTwo}"">
                <TextBlock Text=""{Binding DisplayText}"" />
            </ContentControl>
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
        this.DisplayText = @"
This data can be bound and displayed in the content of a ContentControl.
It can also be displayed a different ContentControl with a different layout of ContentPresenter.
".Trim();
        this.DisplayTextTwo = @"
This other data can be bound and displayed in the content of the same ContentControl.
".Trim();
    }

    public string DisplayText { get; private set; }

    public string DisplayTextTwo { get; private set; }
}
