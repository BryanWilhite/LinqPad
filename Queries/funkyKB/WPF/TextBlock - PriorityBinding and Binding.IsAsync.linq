<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\WPF\PresentationCore.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\WPF\PresentationFramework.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Xaml.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\WPF\WindowsBase.dll</Reference>
  <NuGetReference>Prism.WPF</NuGetReference>
  <Namespace>System.Windows.Controls</Namespace>
  <Namespace>System.Windows.Markup</Namespace>
  <Namespace>Prism.Mvvm</Namespace>
</Query>

/*
    How to: Implement PriorityBinding
    [https://msdn.microsoft.com/en-us/library/ms753174.aspx]
*/
void Main()
{
    var xaml = @"
    <UserControl
        xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation""
        xmlns:x=""http://schemas.microsoft.com/winfx/2006/xaml""
        xmlns:sys=""clr-namespace:System;assembly=mscorlib"">
        <StackPanel>
            <TextBlock>
                <TextBlock.Text>
                    <PriorityBinding>
                        <Binding Path=""SlowestDP"" IsAsync=""True""/>
                        <Binding Path=""SlowerDP"" IsAsync=""True""/>
                        <Binding Path=""FastDP"" />
                    </PriorityBinding>
                </TextBlock.Text>
            </TextBlock>
        </StackPanel>
    </UserControl>
    ";
    
    var view = (UserControl)XamlReader.Parse(xaml);
    view.DataContext = new MyViewModel();
    view.Dump();
}

class MyViewModel : BindableBase
{
    public MyViewModel()
    {
        this._fastDP = "hurry up and wait...";
        this._slowerDP = "still waiting...";
        this._slowestDP = "finally!";
    }
    
    public string FastDP
    {
        get { return this._fastDP; }
        set { this.SetProperty(ref this._fastDP, value); }
    }
    
    public string SlowerDP
    {
        get { Thread.Sleep(3000); return this._slowerDP; }
        set { this.SetProperty(ref this._slowerDP, value); }
    }
    
    public string SlowestDP
    {
        get { Thread.Sleep(5000); return this._slowestDP; }
        set { this.SetProperty(ref this._slowestDP, value); }
    }
    
    string _fastDP;
    string _slowerDP;
    string _slowestDP;
}