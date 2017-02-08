<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\WPF\PresentationCore.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\WPF\PresentationFramework.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Xaml.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\WPF\WindowsBase.dll</Reference>
  <Namespace>System.Windows</Namespace>
  <Namespace>System.Windows.Controls</Namespace>
  <Namespace>System.Windows.Markup</Namespace>
  <Namespace>System.Windows.Shapes</Namespace>
  <Namespace>System.Windows.Media.Animation</Namespace>
</Query>

void Main()
{
    var context = new ParserContext();
    context.XmlnsDictionary.Add("", "http://schemas.microsoft.com/winfx/2006/xaml/presentation");
    
    var xaml = @"
    <StackPanel Width=""320"">
        <Canvas Width=""320"" Height=""240"">
            <Rectangle Canvas.Left=""5"" Name=""RedTangle""
                Fill=""Red""
                Width=""96"" Height=""96""
                />
        </Canvas>
        <Button Name=""GoCommand""
            Content=""Go!""
            />
    </StackPanel>
    ";
    
    var panel = (StackPanel)XamlReader.Parse(xaml, context);
    var button = (Button)panel.FindName("GoCommand");
    var rectangle = (Rectangle)panel.FindName("RedTangle");
    
    button.Click += (s, args)=>
    {
        var x1 = 5;
        var x2 = 100; //destination
        var isLeftX1 = (rectangle.GetLeftPropertyValue() == x1);
        
        if(isLeftX1)
        {
            rectangle.AnimateLeftProperty(x2, (s2, args2) =>
            {
                rectangle.RemoveAllAnimationClocksFromLeftProperty();
                rectangle.SetLeftPropertyValue(x2);
                rectangle.GetLeftPropertyValue().Dump("completed");
            }, 2500);
        }
        else
        {
            rectangle.SetLeftPropertyValue(x1);
        }
    };
    
    panel.Dump();
}

public static class DependencyObjectExtensions
{
    public static double GetLeftPropertyValue(this DependencyObject dependencyObject)
    {
        if (dependencyObject == null) return default(double);

        var current = (double)dependencyObject.GetValue(Canvas.LeftProperty);
        return current;
    }

    public static void IncrementLeftProperty(this DependencyObject dependencyObject, double increment)
    {
        var currentTop = dependencyObject.GetLeftPropertyValue();
        dependencyObject.SetLeftPropertyValue(currentTop + increment);
    }

    public static void SetLeftPropertyValue(this DependencyObject dependencyObject, double value)
    {
        if(dependencyObject == null) return;

        dependencyObject.SetValue(Canvas.LeftProperty, value);
    }
}

public static class UIElementExtensions
{
    public static void AnimateLeftProperty(this UIElement element, EventHandler completionEventHandler, double increment)
    {
        element.AnimateLeftProperty(increment, completionEventHandler, durationInMilliseconds: 100);
    }

    public static void AnimateLeftProperty(this UIElement element, double increment, EventHandler completionEventHandler, double durationInMilliseconds)
    {
        if (element == null) return;

        var duration = new Duration(TimeSpan.FromMilliseconds(durationInMilliseconds));
        var current = element.GetLeftPropertyValue();
        var animation = new DoubleAnimation(current + increment, duration)
        {
            EasingFunction = new BounceEase
            {
                Bounces = 1,
                Bounciness = 1.2,
                EasingMode = EasingMode.EaseOut
            }
        };
        if(completionEventHandler != null) animation.Completed += completionEventHandler;
        element.BeginAnimation(Canvas.LeftProperty, animation);
    }
    
    public static void RemoveAllAnimationClocksFromLeftProperty(this UIElement element)
    {
        if (element == null) return;
        element.BeginAnimation(Canvas.LeftProperty, null);
        //“Specify the property being animated as the first parameter, and null as the second. This will remove all animation clocks from the property.”
        //ref: http://msdn.microsoft.com/en-us/library/system.windows.media.animation.handoffbehavior(v=vs.110).aspx
    }
}