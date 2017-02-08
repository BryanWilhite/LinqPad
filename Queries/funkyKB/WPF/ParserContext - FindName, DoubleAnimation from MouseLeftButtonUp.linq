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
  <Namespace>System.Windows.Input</Namespace>
  <Namespace>System.Windows.Threading</Namespace>
</Query>

void Main()
{
    var context = new ParserContext();
    context.XmlnsDictionary.Add("", "http://schemas.microsoft.com/winfx/2006/xaml/presentation");

    var xaml = @"
    <Canvas Background=""Whitesmoke"" Width=""320"" Height=""240"">
        <Rectangle Canvas.Left=""0"" Panel.ZIndex=""0""
            Name=""OrangeLeft""
            Fill=""Orange""
            ToolTip=""click and drag horizonatally""
            Width=""48"" Height=""240""
            />
        <Rectangle Canvas.Left=""272"" Panel.ZIndex=""0""
            Name=""OrangeRight""
            Fill=""Orange"" Opacity="".4""
            Width=""48"" Height=""240""
            />
        <Rectangle Canvas.Left=""0"" Canvas.Top=""72"" Panel.ZIndex=""1""
            Name=""RedTangle""
            Fill=""Red"" Opacity="".8""
            Width=""96"" Height=""96""
            />
    </Canvas>
    ";

    var canvas = (Canvas)XamlReader.Parse(xaml, context);
    var orangeLeft = (Rectangle)canvas.FindName("OrangeLeft");
    var orangeRight = (Rectangle)canvas.FindName("OrangeRight");
    var redtangle = (Rectangle)canvas.FindName("RedTangle");
    var hasBeenMoved = false;

    canvas.MouseLeftButtonUp += (s, args) =>
    {
        var increment = 0d;
        if(orangeLeft.IsMouseOver)
        {
            increment = -redtangle.GetLeftPropertyValue();
            redtangle.AnimateLeftProperty((s2, args2) =>
            {
                redtangle.RemoveAllAnimationClocksFromLeftProperty();
                redtangle.SetLeftPropertyValue(0);
                hasBeenMoved = false;
                redtangle.GetLeftPropertyValue().Dump("completed");
            },
            increment,
            1000,
            new QuadraticEase { EasingMode = EasingMode.EaseOut });
        }
        else if(hasBeenMoved)
        {
            increment = canvas.Width - redtangle.Width - orangeLeft.Width;
            redtangle.AnimateLeftProperty((s2, args2) =>
            {
                redtangle.RemoveAllAnimationClocksFromLeftProperty();
                redtangle.SetLeftPropertyValue(increment + orangeLeft.Width);
                hasBeenMoved = false;
                redtangle.GetLeftPropertyValue().Dump("completed");
            },
            increment,
            3000,
            new QuadraticEase { EasingMode = EasingMode.EaseOut });
        }
    };

    orangeLeft.MouseMove += (s, args) =>
    {
        if(args.LeftButton != MouseButtonState.Pressed) return;
        var inputElement = s as IInputElement;
        if(inputElement == null) return;

        var point = args.GetPosition(inputElement);
        redtangle.SetLeftPropertyValue(point.X);
        hasBeenMoved = true;
    };

    canvas.Dump();
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
        var current = dependencyObject.GetLeftPropertyValue();
        dependencyObject.SetLeftPropertyValue(current + increment);
    }

    public static void SetLeftPropertyValue(this DependencyObject dependencyObject, double value)
    {
        if(dependencyObject == null) return;

        dependencyObject.SetValue(Canvas.LeftProperty, value);
    }
}

public static class UIElementExtensions
{
    public static void AnimateLeftProperty(this UIElement element, EventHandler completionEventHandler, double increment, double durationInMilliseconds = 100, IEasingFunction easingFunction = null)
    {
        if (element == null) return;

        var duration = new Duration(TimeSpan.FromMilliseconds(durationInMilliseconds));
        var current = element.GetLeftPropertyValue();
        var animation = new DoubleAnimation(current + increment, duration);
        if(easingFunction != null) animation.EasingFunction = easingFunction;
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