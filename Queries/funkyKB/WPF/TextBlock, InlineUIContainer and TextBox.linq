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
    xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation""
    xmlns:x=""http://schemas.microsoft.com/winfx/2006/xaml""
    xmlns:sys=""clr-namespace:System;assembly=mscorlib"">
    <UserControl.Resources>
        <Style TargetType=""{x:Type TextBox}"">
            <Setter Property=""Margin"" Value=""0 0 0 -6pt"" />
        </Style>
    </UserControl.Resources>
    <Grid Width=""512"" Height=""480"">
        <TextBlock FontSize=""18pt"">
            <Run FontWeight=""Bold"" Text=""Path / File Name:"" />
            <InlineUIContainer>
                <StackPanel Orientation=""Horizontal"">
                    <TextBox Text=""./"" />
                    <TextBox Text=""file.opml"" />
                </StackPanel>
            </InlineUIContainer>
            <Run Text=""ok."" />
        </TextBlock>
    </Grid>
</UserControl>
";

var view = (UserControl)XamlReader.Parse(xaml);
view.Dump();