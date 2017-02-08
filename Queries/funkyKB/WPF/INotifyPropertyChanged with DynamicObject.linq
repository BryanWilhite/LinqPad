<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.ComponentModel.DataAnnotations.dll</Reference>
  <NuGetReference>Prism</NuGetReference>
  <Namespace>System.ComponentModel</Namespace>
  <Namespace>System.Globalization</Namespace>
  <Namespace>System.Windows.Controls</Namespace>
  <Namespace>System.Windows.Markup</Namespace>
  <Namespace>Microsoft.Practices.Prism.Mvvm</Namespace>
  <Namespace>System.Windows.Data</Namespace>
  <Namespace>System.Dynamic</Namespace>
</Query>

/*
    based on “Automatic INotifyPropertyChanged with DynamicObject”
    [http://codeflow49.blogspot.com/2010/06/automatic-inotifypropertychanged-with.html]

    lab49 from 2009: http://blog.lab49.com/archives/3893
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
    
    dynamic myViewModel = new NotifyPropertyChangedProxy<MyModel>(new MyModel());
    (myViewModel as INotifyPropertyChanged).PropertyChanged += (s, args) => args.Dump();
    view.DataContext = myViewModel;
    view.Dump();
}

//from https://gist.github.com/jpolvora/2730528
public class NotifyPropertyChangedProxy<T> : DynamicObject, INotifyPropertyChanged where T : class
{
    public T WrappedObject { get; private set; }

    private readonly Dictionary<string, PropertyInfo> _cache = new Dictionary<string, PropertyInfo>();
    private bool _allCached;

    public NotifyPropertyChangedProxy(T wrappedObject)
    {
        if (wrappedObject == null)
            throw new ArgumentNullException("wrappedObject");

        WrappedObject = wrappedObject;
    }

    #region INotifyPropertyChanged support

    protected virtual void OnPropertyChanged(string propertyName)
    {
        var handler = PropertyChanged;
        if (handler != null)
        {
            handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    #endregion

    public override IEnumerable<string> GetDynamicMemberNames()
    {
        if (!_allCached)
        {
            var properties = WrappedObject.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
            foreach (var property in properties.Where(property => !_cache.ContainsKey(property.Name)))
            {
                _cache[property.Name] = property;
            }
        }
        _allCached = true;
        return _cache.Keys;
    }

    public override bool TryGetMember(GetMemberBinder binder, out object result)
    {
        // Locate property by name

        PropertyInfo propertyInfo;
        if (!_cache.TryGetValue(binder.Name, out propertyInfo))
        {
            propertyInfo = WrappedObject.GetType().GetProperty(binder.Name, BindingFlags.Instance | BindingFlags.Public | (binder.IgnoreCase ? BindingFlags.IgnoreCase : 0));
        }

        if (propertyInfo == null || !propertyInfo.CanRead)
        {
            result = null;
            return false;
        }

        result = propertyInfo.GetValue(WrappedObject, null);
        return true;
    }

    public override bool TrySetMember(SetMemberBinder binder, object value)
    {
        // Locate property by name

        PropertyInfo propertyInfo;
        if (!_cache.TryGetValue(binder.Name, out propertyInfo))
        {
            propertyInfo = WrappedObject.GetType().GetProperty(binder.Name, BindingFlags.Instance | BindingFlags.Public | (binder.IgnoreCase ? BindingFlags.IgnoreCase : 0));
        }

        if (propertyInfo == null || !propertyInfo.CanWrite)
            return false;

        object newValue = value;
        // Check the types are compatible
        Type propertyType = propertyInfo.PropertyType;
        if (!propertyType.IsAssignableFrom(value.GetType()))
        {
            newValue = Convert.ChangeType(value, propertyType);
        }

        propertyInfo.SetValue(WrappedObject, newValue, null);
        OnPropertyChanged(binder.Name);
        return true;
    }
}
    
public class MyModel
{
    public double? MyNumeral { get; set; }

    public string MyText { get; set; }
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