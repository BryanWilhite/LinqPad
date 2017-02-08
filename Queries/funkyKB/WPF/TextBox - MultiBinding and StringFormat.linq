<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\WPF\PresentationCore.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\WPF\PresentationFramework.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Xaml.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\WPF\WindowsBase.dll</Reference>
  <NuGetReference>Prism</NuGetReference>
  <Namespace>System.Windows.Controls</Namespace>
  <Namespace>System.Windows.Markup</Namespace>
  <Namespace>Microsoft.Practices.Prism.Mvvm</Namespace>
</Query>

/*
    6 Things I Bet You Didn't Know About Data Binding in WPF
    [http://interactiveasp.net/blogs/natesstuff/archive/2009/01/21/6-things-i-bet-you-didn-t-know-about-data-binding-in-wpf.aspx]
*/
void Main()
{
    var xaml = @"
    <UserControl
        xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation""
        xmlns:x=""http://schemas.microsoft.com/winfx/2006/xaml""
        xmlns:sys=""clr-namespace:System;assembly=mscorlib"">
        <StackPanel>
            <TextBox FontSize=""32pt"">
                <TextBox.Text>
                    <MultiBinding Mode=""TwoWay"" StringFormat=""{}{0}, {1}"">
                        <Binding Path=""LastName"" />
                        <Binding Path=""FirstName"" />
                    </MultiBinding>
                </TextBox.Text>
            </TextBox>
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
        this._lastName = "Wilhite";
        this._firstName = "Bryan";
        
        this.PropertyChanged += (s, args) =>
        {
            args.Dump();
        };
    }
    
    public string FirstName
    {
        get { return this._firstName; }
        set { this.SetProperty(ref this._firstName, value); }
    }
    
    public string LastName
    {
        get { return this._lastName; }
        set { this.SetProperty(ref this._lastName, value); }
    }
    
    string _firstName;
    string _lastName;
}