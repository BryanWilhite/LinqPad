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
    <ListBox>
        <ListBox.ItemsSource>
            <x:Array Type=""sys:String"">
                <sys:String>lorem ipsum</sys:String>
                <sys:String>two</sys:String>
                <sys:String>three</sys:String>
                <sys:String>four</sys:String>
            </x:Array>
        </ListBox.ItemsSource>
    </ListBox>
</UserControl>
";

var view = (UserControl)XamlReader.Parse(xaml);
view.Dump();