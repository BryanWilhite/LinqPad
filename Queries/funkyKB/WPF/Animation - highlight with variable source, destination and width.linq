<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\WPF\PresentationCore.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\WPF\PresentationFramework.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Windows.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Xaml.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\WPF\WindowsBase.dll</Reference>
  <Namespace>System.Windows</Namespace>
  <Namespace>System.Windows.Controls</Namespace>
  <Namespace>System.Windows.Controls.Primitives</Namespace>
  <Namespace>System.Windows.Input</Namespace>
  <Namespace>System.Windows.Markup</Namespace>
  <Namespace>System.Windows.Media</Namespace>
  <Namespace>System.Windows.Media.Animation</Namespace>
  <Namespace>System.Windows.Shapes</Namespace>
</Query>

void Main()
{
    var xaml = @"
    <UserControl
        xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation""
        xmlns:x=""http://schemas.microsoft.com/winfx/2006/xaml""
        xmlns:sys=""clr-namespace:System;assembly=mscorlib"">
        <UserControl.Resources>
            <Style TargetType=""{x:Type Button}"">
                <Setter Property=""Height"" Value=""32"" />
                <Setter Property=""Margin"" Value=""8"" />
                <Setter Property=""Width"" Value=""128"" />
            </Style>
        </UserControl.Resources>
        <Canvas>
            <StackPanel Name=""LeftStack"" Canvas.Left=""16"" Canvas.Top=""16"" Width=""160"">
                <Button />
                <Button  Width=""96"" />
                <Button />
            </StackPanel>
            <StackPanel Name=""RightStack"" Canvas.Left=""240"" Canvas.Top=""16"" Width=""160"">
                <Button />
                <Button Width=""96"" />
                <Button />
            </StackPanel>
            <Rectangle Name=""Tangle""
                Fill=""Red""
                IsHitTestVisible=""false"" Opacity=""0""
                Width=""128"" Height=""32""
                />
        </Canvas>
    </UserControl>
    ";

    var view = (UserControl)XamlReader.Parse(xaml);
    var leftStack = (StackPanel)view.FindName("LeftStack");
    var rightStack = (StackPanel)view.FindName("RightStack");
    var tangle = (Rectangle)view.FindName("Tangle");

    view.Loaded += (s, args) =>
    {
        Action<StackPanel, Button> setupStackButton = (stack, button) =>
        {
            var canvasTopLeft = stack.GetTopAndLeftOfChildOnCanvas(button);
            var canvasCenter = stack.GetCenterOfChildOnCanvas(button);
            var index = stack.Children.IndexOf(button);
            
            if((index == 0) && (stack.Name == "LeftStack"))
            {
                tangle.SetCenterOfChildOnCanvas(canvasCenter);
                tangle.Opacity = .4;
            }

            button.Content = $"center: ({canvasCenter.X},{canvasCenter.Y})";

            button.Click += (s1, args1) =>
            {
                var duration = 450;
                tangle.AnimateProperty(Canvas.LeftProperty,
                    canvasTopLeft.X, duration,
                    (s2, args2) =>
                    {
                        tangle.RemoveAllAnimationClocksFromProperty(Canvas.LeftProperty);
                        tangle.SetDouble(Canvas.LeftProperty, canvasTopLeft.X);
                    });

                tangle.AnimateProperty(Canvas.TopProperty,
                    canvasTopLeft.Y, duration,
                    (s2, args2) =>
                    {
                        tangle.RemoveAllAnimationClocksFromProperty(Canvas.TopProperty);
                        tangle.SetDouble(Canvas.TopProperty, canvasTopLeft.Y);
                    });

                tangle.AnimateProperty(FrameworkElement.WidthProperty,
                    button.Width, duration,
                    (s2, args2) =>
                    {
                        tangle.RemoveAllAnimationClocksFromProperty(FrameworkElement.WidthProperty);
                        tangle.SetDouble(FrameworkElement.WidthProperty, button.Width);
                    });
            };
        };

        leftStack.Children.OfType<Button>().ToList().ForEach(i => setupStackButton(leftStack, i));
        rightStack.Children.OfType<Button>().ToList().ForEach(i => setupStackButton(rightStack, i));
    };

    view.Dump();
}

public static class CanvasExtensions
{
    public static Point GetTopAndLeftOfChildOnCanvas(this StackPanel panel, FrameworkElement child)
    {
        if(panel == null) return default(Point);
        if(child == null) throw new ArgumentNullException("child", "The expected child element is not here.");

        var stackTopLeft = new Vector(Canvas.GetLeft(panel), Canvas.GetTop(panel));
        var canvasTopLeft = Point.Add(child.GetVisualOrigin(panel), stackTopLeft);
        return canvasTopLeft;
    }

    public static Point GetCenterOfChildOnCanvas(this StackPanel panel, FrameworkElement child)
    {
        if(panel == null) return default(Point);

        var canvasTopLeft = panel.GetTopAndLeftOfChildOnCanvas(child);
        var center = new Vector(child.GetCenterX(), child.GetCenterY());
        var canvasCenter = Point.Add(canvasTopLeft, center);
        return canvasCenter;
    }
    
    public static void SetCenterOfChildOnCanvas(this FrameworkElement child, Point center)
    {
        if(child == null) return;

        Canvas.SetLeft(child, center.X - child.GetCenterX());
        Canvas.SetTop(child, center.Y - child.GetCenterY());
    }
}

public static class DependencyObjectExtensions
{
    public static double? GetDouble(this DependencyObject dependencyObject, DependencyProperty property)
    {
        if (dependencyObject == null) return null;
        return (double)dependencyObject.GetValue(property);
    }

    public static void SetDouble(this DependencyObject dependencyObject, DependencyProperty property, double value)
    {
        if (dependencyObject == null) return;
        dependencyObject.SetValue(property, value);
    }
}

public static class FrameworkElementExtensions
{
    public static double GetCenterX(this FrameworkElement element)
    {
        if (element == null) return default(double);
        return (element.Width / 2);
    }

    public static double GetCenterY(this FrameworkElement element)
    {
        if (element == null) return default(double);
        return (element.Height / 2);
    }
}

public static class UIElementExtensions
{
    public static void AnimateProperty(this UIElement element, DependencyProperty property, double toValue, double durationInMilliseconds, EventHandler completionHandler = null)
    {
        if (element == null) return;

        var duration = new Duration(TimeSpan.FromMilliseconds(durationInMilliseconds));
        var animation = new DoubleAnimation(toValue, duration);
        if(completionHandler != null) animation.Completed += completionHandler;
        element.BeginAnimation(property, animation);
    }

    public static void AnimateProperty(this UIElement element, DependencyProperty property, Thickness toValue, double durationInMilliseconds, EventHandler completionHandler = null)
    {
        if (element == null) return;

        var duration = new Duration(TimeSpan.FromMilliseconds(durationInMilliseconds));
        var animation = new ThicknessAnimation(toValue, duration);
        if(completionHandler != null) animation.Completed += completionHandler;
        element.BeginAnimation(property, animation);
    }

    public static void RemoveAllAnimationClocksFromProperty(this UIElement element, DependencyProperty property)
    {
        if (element == null) return;
        element.BeginAnimation(property, null);
    }
}

public static class VisualExtensions
{
    public static Point GetVisualOrigin(this Visual visual, Visual ancestorVisual)
    {
        if (visual == null) return default(Point);
        if (ancestorVisual == null) throw new ArgumentNullException("ancestorVisual", "The expected ancestor visual is not here.");

        var transform = visual.TransformToAncestor(ancestorVisual);
        var point = transform.Transform(new Point(0, 0));
        return point;
    }
}