<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.ComponentModel.DataAnnotations.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\WPF\PresentationFramework.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Xaml.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\WPF\WindowsBase.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\WPF\PresentationCore.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Configuration.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\WPF\UIAutomationProvider.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\WPF\UIAutomationTypes.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\WPF\ReachFramework.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\WPF\PresentationUI.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\WPF\System.Printing.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\Accessibility.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Deployment.dll</Reference>
  <NuGetReference>Prism</NuGetReference>
  <Namespace>System.ComponentModel</Namespace>
  <Namespace>System.Globalization</Namespace>
  <Namespace>System.Windows.Controls</Namespace>
  <Namespace>System.Windows.Markup</Namespace>
  <Namespace>Microsoft.Practices.Prism.Mvvm</Namespace>
  <Namespace>System.Windows.Data</Namespace>
</Query>

/*
    based on WPF Validation by Azim Zahir
    [http://www.codeproject.com/Articles/321745/WPF-Validation]
*/
void Main()
{
    var xaml = @"
<UserControl
    xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation""
    xmlns:x=""http://schemas.microsoft.com/winfx/2006/xaml""
    xmlns:sys=""clr-namespace:System;assembly=mscorlib"">
    <StackPanel Width=""320"">
        <TextBox Name=""MyNumeralBox""
            FontFamily=""Consolas""
            FontSize=""32pt""
            Text=""{Binding Path=MyNumeral, Mode=TwoWay, NotifyOnValidationError=True, TargetNullValue='Enter Numeral'}""
            />
        <TextBox Name=""MyTextBox""
            FontSize=""32pt""
            Margin=""0 8pt""
            Text=""{Binding Path=MyText, Mode=TwoWay, TargetNullValue='Enter Text'}""
            />
    </StackPanel>
</UserControl>
";

    var view = (UserControl)XamlReader.Parse(xaml);

    var textbox = (TextBox)view.FindName("MyNumeralBox");
    textbox.GotFocus += (s, args) =>
    {
        var expression = textbox.GetBindingExpression(TextBox.TextProperty);
        Validation.ClearInvalid(expression);
    };
    
    var binding = BindingOperations.GetBinding(textbox, TextBox.TextProperty);
    binding.ValidationRules.Add(new DoubleValidator());
    
    Validation.AddErrorHandler(textbox, (s, args) =>
    {
        var tb = s as TextBox;
        if(tb == null) return;
        tb.ToolTip = (args.Action == ValidationErrorEventAction.Added) ?
            args.Error.ErrorContent.ToString()
            :
            string.Empty;
    });
    /*
        Use the Validation.Error attached property in XAML.
    */
    
    view.Loaded += (s, args) =>
    {
        textbox.Focus();
        textbox.SelectAll();
    };
    view.DataContext = new MyViewModel();
    view.Dump();
}

public class MyViewModel : BindableBase
{
    public double? MyNumeral
    {
        get { return this._myNumeral; }
        set { this.SetProperty(ref this._myNumeral, value); }
    }

    public string MyText
    {
        get { return this._myText; }
        set { this.SetProperty(ref this._myText, value); }
    }

    double? _myNumeral;
    string _myText;
}

public class DoubleValidator : ValidationRule
{
    public override ValidationResult Validate (object value, CultureInfo cultureInfo)
    {
        var number = default(double);
        try
        {
            number = Convert.ToDouble(value.ToString());
        }
        catch (Exception)
        {
            return new ValidationResult(isValid:false, errorContent:"Value must be numeric");
        }
        if (number == 0)
        {
            return new ValidationResult(isValid:false, errorContent:"Value must be non-zero");
        }
        return new ValidationResult(true, null);
    }
}